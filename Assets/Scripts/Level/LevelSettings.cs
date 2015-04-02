using UnityEngine;
using System.Collections;

public class LevelSettings : MonoBehaviour {

	public int minimumNodes = 20;
	public int maximumNodes = 50;

	public Vector3 nodeMinimumScale = new Vector3(1, 1, 1);
	public Vector3 nodeMaximumScale = new Vector3(10, 30, 30);

	public float minimumNodeDistance = 8;
	public float maximumNodeDistance = 20;
	
	public int nodeExtraEdges = 10;
	public int nodeExtraEdgesAttempts = 100; 
}
