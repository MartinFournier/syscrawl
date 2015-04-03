using UnityEngine;
using System.Collections;

namespace syscrawl.Levels.Nodes
{
    public class FilesystemNode : Node
    {
        public static FilesystemNode Create(Level level, string nodeName)
        {
            var nodeObject = new GameObject(nodeName);
            nodeObject.transform.parent = level.transform;
            var node = nodeObject.AddComponent<FilesystemNode>();

            var cube1 = node.CreateCube();
            var cube2 = node.CreateCube();

            var margin = 0.25f;

            cube2.transform.Translate(
                cube2.transform.localScale.x + margin, 0, 0, cube1.transform);

            var cube3 = node.CreateCube();
            cube3.transform.Translate(
                0, 0, cube3.transform.localScale.z + margin, cube2.transform);

            var cube4 = node.CreateCube();
            cube4.transform.localPosition = cube3.transform.localPosition;
            cube4.transform.Translate(
                cube4.transform.localScale.x + margin, 0, 0, cube3.transform);

            node.Wrapper = nodeObject;
            return node;
        }


        GameObject CreateCube()
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(this.transform);
            var material = Resources.Load<Material>("Materials/Nodes/Filesystem");
            var cubeRenderer = cube.GetComponent<Renderer>();
            cubeRenderer.material = material;
            return cube;
        }
    }
}