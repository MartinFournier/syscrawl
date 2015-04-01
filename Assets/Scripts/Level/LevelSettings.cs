using UnityEngine;
using System.Collections;

public class LevelSettings : MonoBehaviour {

	public int minimumNodes = 20;
	public int maximumNodes = 50;

	public Vector3 nodeMinimumScale = new Vector3(1, 1, 1);
	public Vector3 nodeMaximumScale = new Vector3(10, 30, 30);

	public float minimumNodeDistance = 8;
	public float maximumNodeDistance = 20;
	
	public int nodeExtraConnections = 10;
	public int nodeExtraConnectionsAttempts = 100; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
