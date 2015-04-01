using UnityEngine;
using System.Collections;

public class Filesystem : Node {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public new void Generate(LevelSettings settings) {
		cube = gameObject;
		var cube1 = CreateCube (settings);
		var cube2 = CreateCube (settings);
		var margin = 0.25f;

		cube2.transform.Translate (cube2.transform.localScale.x + margin, 0, 0, cube1.transform);

		var cube3 = CreateCube (settings);
		cube3.transform.Translate (0, 0, cube3.transform.localScale.z + margin, cube2.transform);

		var cube4 = CreateCube (settings);
		cube4.transform.localPosition = cube3.transform.localPosition;
		cube4.transform.Translate (cube4.transform.localScale.x + margin, 0, 0, cube3.transform);



	}

	private GameObject CreateCube(LevelSettings settings) {
		var cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		cube.transform.SetParent(this.transform);
		var material = Resources.Load<Material> ("Materials/Filecube");
		var renderer = cube.GetComponent<Renderer> ();
		renderer.material = material;
		return cube;
	}
}
