using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NGenerics.DataStructures.General;

public class Level : MonoBehaviour {
	//change to graph type
	//http://www.vcskicks.com/representing-graphs.php
	private List<GameObject> nodes = new List<GameObject>();

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	
	}

	//from https://gist.github.com/radiatoryang/5682034
	IEnumerator ForceDirectGraph<T>( Graph<T> graph, Dictionary<T, Vector3> graphPositions ) {
		// settings
		float attractToCenter = 15f;
		float repulsion = 10f;
		float spacing = 0.1f;
		float stiffness = 100f;
		float damping = 0.9f;
		
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
	}


	public void Generate(LevelSettings settings) {
		var test = new GameObject ("FS");
		test.transform.parent = transform;
		var fs = test.AddComponent<Filesystem> ();
		fs.Generate (settings);
		nodes.Add (test);

		InstanciateNodes (settings,fs);

		AddRedundantConnections2 (settings);

		PositionNodes (settings);
	}

	private void InstanciateNodes(LevelSettings settings, Node startingNode) {
		var nbOfNodes = Random.Range (settings.minimumNodes, settings.maximumNodes + 1);
		Node previousNode = startingNode;
		for (var x = 0; x < nbOfNodes; x++) {
			var nodeObject = new GameObject("Node " + x);
			nodeObject.transform.parent = transform;
			var node = nodeObject.AddComponent<Node>();
			node.Generate(settings);

			if (previousNode != null) {
				node.AddConnection(previousNode);
			}
			previousNode = node;
			nodes.Add (nodeObject);
		};
	}

	
	private void AddRedundantConnections(LevelSettings settings) {
		var nbNodes = nodes.Count;
		
		for (var x = 0; x < nbNodes; x++) {
			
			if (Random.value > 0.5) { // add connection
				var nodeObject = nodes[x];
				var node = nodeObject.GetComponent<Node>();
				//random node
				var randomIndex = x;
				var attempts = 0;
				var maxAttempts = 10;
				while(randomIndex != x && attempts < maxAttempts) {
					randomIndex = Random.Range (0, nbNodes);
				}
				var randomNodeObject = nodes[randomIndex].GetComponent<Node>();
				node.AddConnection(randomNodeObject);
			}
		}
	}

	
	private void AddRedundantConnections2(LevelSettings settings) {
		var nbNodes = nodes.Count;
		var attempt = 0;
		var extraConnections = 0;

		while (
			attempt < settings.nodeExtraConnectionsAttempts && 
			extraConnections < settings.nodeExtraConnections) {
		
			var randomNode = 
				nodes[Random.Range (0, nbNodes)].GetComponent<Node>();
			var randomConnection = 
				nodes[Random.Range (0, nbNodes)].GetComponent<Node>();
			if (randomNode != randomConnection) {
				randomNode.AddConnection(randomConnection);
				extraConnections++;
			}
		}
	}

	private void PositionNodes(LevelSettings settings) {
		GameObject previousNode = null;
		foreach (var node in nodes) {
			if (previousNode == null) {
				previousNode = node;
				continue;
			}

			Vector3 translationVector;
			var xAxis = RandomBool();
			var negate = RandomBool();
			var translation = Random.Range (settings.minimumNodeDistance, settings.maximumNodeDistance + 1);
			if (negate) translation = -translation;
			if (xAxis) {
				translationVector = new Vector3(translation, 0, 0);
			}else{
				translationVector = new Vector3(0, 0, translation);
			}

			node.transform.localPosition = previousNode.transform.position + translationVector;
			//node.transform.Translate(translationVector);

			previousNode = node;
		}
	}

	private bool RandomBool() {
		return Random.Range (0, 2) == 0;
	}
}
