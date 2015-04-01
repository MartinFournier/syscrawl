using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode()]
public class Node : MonoBehaviour {

	private IList<Node> Connections = new List<Node>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnDrawGizmos() {
		foreach (var node in Connections) {
			Gizmos.DrawLine(transform.position, node.transform.position);
		}
	}

	public void Generate(LevelSettings settings) {
		var cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		cube.transform.SetParent(this.transform);

		var scale = RandomVectorBetween (settings.nodeMinimumScale, settings.nodeMaximumScale);

		cube.transform.localScale = scale;
	}

	private Vector3 RandomVectorBetween(Vector3 minimum, Vector3 maximum) {
		var x = Random.Range (minimum.x, maximum.x + 1);
		var y = Random.Range (minimum.y, maximum.y + 1);
		var z = Random.Range (minimum.z, maximum.z + 1);
		return new Vector3(x,y,z);
	}

	public void AddConnection(Node node) {
		Connections.Add (node);
	}
}
