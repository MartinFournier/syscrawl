using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NGenerics.DataStructures.General;

[ExecuteInEditMode()]
public class Level : MonoBehaviour {

	private Graph<Node> nodesGraph = null;
	private Dictionary<Node, Vector3> nodesPositions = null;

	private Vector3 GetActualNodePosition(Node node) {
		if (nodesPositions == null) return new Vector3(1,0,1);
		var position = nodesPositions[node];
		var scaleFactor = 4f;
		return new Vector3(position.x * scaleFactor, position.y * scaleFactor, position.z * scaleFactor);
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (nodesGraph == null || !nodesGraph.Edges.Any())
			return;
		foreach (var edge in nodesGraph.Edges) {
			var v1 = edge.FromVertex;
			var v2 = edge.ToVertex;

			var pos1 = GetActualNodePosition(v1.Data);
			var pos2 = GetActualNodePosition(v2.Data);

			Debug.DrawLine(pos1, pos2);
		}
	}



	public void Generate(LevelSettings settings) {
		Debug.ClearDeveloperConsole ();

		Debug.Log ("Start Generation");

		nodesGraph = new Graph<Node> (false);
		nodesPositions = new Dictionary<Node, Vector3>();

		var test = new GameObject ("FS");
		test.transform.parent = transform;
		var fs = test.AddComponent<Filesystem> ();
		fs.Generate (settings);
		var vertex = nodesGraph.AddVertex (fs);


		InstanciateNodes (settings, vertex);
		AddRedundantEdges (settings);

		var index = 0;
		foreach (var node in nodesGraph.Vertices.Select (v => v.Data)) {
			nodesPositions.Add (node, new Vector3(index + 1,0,1));
			index++;
		}

		var positionCoroutine = PositionNodes (settings);
		var graphCoroutine = ForceDirectGraph<Node> (nodesGraph, nodesPositions, positionCoroutine);

		StartCoroutine (graphCoroutine);
	}

	private void InstanciateNodes(LevelSettings settings, Vertex<Node> startVertex) {
		var nbOfNodes = Random.Range (settings.minimumNodes, settings.maximumNodes + 1);
		var previousVertex = startVertex;
		for (var x = 0; x < nbOfNodes; x++) {
			var nodeObject = new GameObject("Node " + x);
			nodeObject.transform.parent = transform;
			var node = nodeObject.AddComponent<Node>();

			node.Generate(settings);

			var newVertex = nodesGraph.AddVertex(node);
			var edge = new Edge<Node>(previousVertex, newVertex, false);
			nodesGraph.AddEdge(edge);
			previousVertex = newVertex;
		};
	}

	private void AddRedundantEdges(LevelSettings settings) {
		var vertices = nodesGraph.Vertices.ToList ();
		var nbVertices = vertices.Count;
		var attempt = 0;
		var maximumEdges = settings.nodeExtraEdges + nbVertices;


		while (
			attempt < settings.nodeExtraEdgesAttempts && 
			nodesGraph.Edges.Count < maximumEdges) {

			var vertexFrom = vertices[Random.Range (0, nbVertices)];
			var vertexTo = vertices[Random.Range (0, nbVertices)];
			if (vertexFrom.Equals(vertexTo)) continue;
			var existingEdge = nodesGraph.GetEdge(vertexFrom, vertexTo);
			if (existingEdge != null) continue;
			var edge = new Edge<Node>(vertexFrom, vertexTo, false);
			if (nodesGraph.ContainsEdge(edge)) continue;

			nodesGraph.AddEdge(edge);
		}
	}

	 IEnumerator PositionNodes(LevelSettings settings) {
		Debug.Log ("Position nodes start");
		yield return 0;
		foreach (var key in nodesPositions.Keys) {
			var position = GetActualNodePosition(key);
			key.transform.localPosition = position;
			Debug.Log ("Positioned a node");
			yield return 0;
		}
		Debug.Log ("Done positioning");
	}

	private bool RandomBool() {
		return Random.Range (0, 2) == 0;
	}

			//from https://gist.github.com/radiatoryang/5682034
	IEnumerator ForceDirectGraph<T>( Graph<T> graph, Dictionary<T, Vector3> graphPositions, IEnumerator positionCoroutine) {
		Debug.Log ("Graph");
		// settings
		float attractToCenter = 15f;
		float repulsion = 10f;
		float spacing = 0.4f;
		float stiffness = 10f;
		float damping = 0.7f;
		
		// initialize velocities and positions
		Dictionary<Vertex<T>, Vector2> velocity = new Dictionary<Vertex<T>, Vector2>();
		Dictionary<Vertex<T>, Vector2> position = new Dictionary<Vertex<T>, Vector2>();
		
		foreach ( Vertex<T> vert in graph.Vertices ) {
			velocity.Add( vert, Vector2.zero );
			
			Vector3 bestGuess = Random.onUnitSphere * spacing * 0.5f;
			if ( graphPositions.ContainsKey( vert.Data ) ) {
				bestGuess += graphPositions[vert.Data];
			} else {
				bestGuess += graphPositions[vert.IncidentEdges[0].GetPartnerVertex( vert ).Data];
			}
			position.Add( vert, new Vector2( bestGuess.x, bestGuess.z ) );
		}


		float totalEnergy = 10f; // initial
		while ( totalEnergy > 1f ) {
			totalEnergy = 0f;
			foreach ( Vertex<T> thisVert in graph.Vertices ) {
				Vector2 netForce = Vector2.zero; // running total of kinetic energy for thisVert
				
				// Coulomb repulsion
				foreach ( Vertex<T> otherVert in graph.Vertices ) {
					if ( otherVert == thisVert )
						continue;
					Vector2 direction = position[thisVert] - position[otherVert];
					netForce += ( direction.normalized * repulsion ) / ( Mathf.Pow( direction.magnitude + 0.1f, 2f ) * 0.5f );
				}
				
				// Hooke attraction
				foreach ( Edge<T> neighbor in thisVert.EmanatingEdges ) {
					Vector2 direction = position[neighbor.ToVertex] - position[thisVert];
					float displacement = spacing - direction.magnitude;
					netForce += direction.normalized * ( stiffness * displacement * -0.5f );
				}
				
				// attract to center
				netForce += -position[thisVert].normalized * attractToCenter;
				
				// apply velocity to position
				velocity[thisVert] = ( velocity[thisVert] + ( netForce * Time.deltaTime ) ) * damping;
				position[thisVert] += velocity[thisVert] * Time.deltaTime;
				// update running totals too, in case we want to use them outside of this coroutine
				graphPositions[thisVert.Data] = new Vector3( position[thisVert].x, 0f, position[thisVert].y );
				
				// add thisVert's energy to the running total of all kinetic energy
				totalEnergy += velocity[thisVert].sqrMagnitude;
			}
			Debug.Log( "TOTAL ENERGY: " + totalEnergy.ToString() );
			yield return 0;
		}
		Debug.Log ("Done");
		StartCoroutine (positionCoroutine);
	}

}
