using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour
{

    public GameObject cube;

    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Update()
    {

    }



    public void Generate(LevelSettings settings)
    {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.SetParent(this.transform);

        var scale = 
            RandomUtils.RandomVectorBetweenRange(
                settings.nodeMinimumScale, 
                settings.nodeMaximumScale);

        cube.transform.localScale = scale;
    }
}
