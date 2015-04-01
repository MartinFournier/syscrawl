﻿using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	GameObject levelObject = null;
	
	public string seed;

	public LevelSettings settings;
	

	void Start() {
		GenerateLevel ();
	}

	public void GenerateLevel() {
		if (levelObject != null) {
			Object.DestroyImmediate(levelObject);
		}

		if (!string.IsNullOrEmpty(seed)) {
			Random.seed = seed.GetHashCode ();
		} else {
			Random.seed = Random.Range(1, int.MaxValue);
		}

		levelObject = new GameObject ("Level");
		var level = levelObject.gameObject.AddComponent<Level> ();
		level.Generate (settings);
	}
}
