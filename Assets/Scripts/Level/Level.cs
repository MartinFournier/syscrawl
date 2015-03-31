using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {

	private List<GameObject> nodes = new List<GameObject>();

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void Generate(LevelSettings settings) {
		var test = new GameObject ("FS");
		test.transform.parent = transform;
		var fs = test.AddComponent<Filesystem> ();
		fs.Generate (settings);
		nodes.Add (test);

		InstanciateNodes (settings);
		PositionNodes (settings);
	}

	private void InstanciateNodes(LevelSettings settings) {
		var nbOfNodes = Random.Range (settings.minimumNodes, settings.maximumNodes + 1);
		for (var x = 0; x < nbOfNodes; x++) {
			var nodeObject = new GameObject("Node " + x);
			nodeObject.transform.parent = transform;
			var node = nodeObject.AddComponent<Node>();
			node.Generate(settings);
			
			nodes.Add (nodeObject);
		};
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
