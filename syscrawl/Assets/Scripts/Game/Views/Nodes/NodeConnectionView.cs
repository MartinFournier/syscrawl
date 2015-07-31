using System;
using strange.extensions.mediation.impl;
using UnityEngine;
using System.Collections.Generic;

namespace syscrawl.Game.Views.Nodes
{
    public class NodeConnectionView : View
    {
        private const bool showDebugInfo = false;
        private const float width = 0.2f;
        private const float height = 0.1f;

        public void Init(Vector3 from, Vector3 to)
        {
            var meshFilter = gameObject.AddComponent<MeshFilter>();
            var meshRenderer = gameObject.AddComponent<MeshRenderer>();

            var mesh = GetConnectionMesh(from, to);
            mesh.name = "Connection Mesh";

            var material = Resources.Load<Material>("Materials/Nodes/Filesystem");
            meshRenderer.material = material;
            meshFilter.mesh = mesh;
        }

        static Mesh GetConnectionMesh(Vector3 from, Vector3 to)
        {
            var meshBuilder = new MeshBuilder();

            var length = Math.Abs(Vector3.Distance(to, from));
            var forwardDirection = (to - from).normalized;
            var forward = forwardDirection * length;
            var upDirection = Quaternion.AngleAxis(-90, forwardDirection) * Vector3.up;
            var rightDirection = Quaternion.AngleAxis(-90, upDirection) * Vector3.right;

            Vector3 up = upDirection.normalized * width;
            Vector3 right = rightDirection.normalized * height;
            Vector3 nearCorner = from;
            Vector3 farCorner = up + right + to;

            if (showDebugInfo)
            {
                Debug.Log("From: " + from + " To: " + to);
                Debug.Log(String.Format("Forward: {0}; Right: {1}; Up: {2}", forward, right, up));
                Debug.Log("NearCorner: " + nearCorner + "; FarCorner: " + farCorner);
                Debug.Log("Direction: " + forwardDirection);
                Debug.Log("Length: " + length);
            }

            meshBuilder.BuildQuad(nearCorner, right, forward);
            meshBuilder.BuildQuad(nearCorner, forward, up);
            meshBuilder.BuildQuad(farCorner, -forward, -right);
            meshBuilder.BuildQuad(farCorner, -up, -forward);
            var mesh = meshBuilder.CreateMesh();
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.Optimize();
            return mesh;
        }
    }

    //http://jayelinda.com/modelling-by-numbers-part-1a/
    class MeshBuilder
    {
        private List<Vector3> _vertices = new List<Vector3>();

        public List<Vector3> Vertices { get { return _vertices; } }

        private List<Vector3> _normals = new List<Vector3>();

        public List<Vector3> Normals { get { return _normals; } }

        private List<Vector2> _uvs = new List<Vector2>();

        public List<Vector2> UVs { get { return _uvs; } }

        private List<int> _indices = new List<int>();

        public void AddTriangle(int index0, int index1, int index2)
        {
            _indices.Add(index0);
            _indices.Add(index1);
            _indices.Add(index2);
        }

        public Mesh CreateMesh()
        {
            Mesh mesh = new Mesh();

            mesh.vertices = _vertices.ToArray();
            mesh.triangles = _indices.ToArray();

            //Normals are optional. Only use them if we have the correct amount:
            if (_normals.Count == _vertices.Count)
                mesh.normals = _normals.ToArray();

            //UVs are optional. Only use them if we have the correct amount:
            if (_uvs.Count == _vertices.Count)
                mesh.uv = _uvs.ToArray();

            mesh.RecalculateBounds();

            return mesh;
        }

        public void BuildQuad(
            Vector3 offset, 
            Vector3 widthDirection, 
            Vector3 lengthDirection)
        {
            var normal = 
                Vector3.Cross(lengthDirection, widthDirection).normalized;

            Vertices.Add(offset);
            UVs.Add(new Vector2(0.0f, 0.0f));
            Normals.Add(normal);

            Vertices.Add(offset + lengthDirection);
            UVs.Add(new Vector2(0.0f, 1.0f));
            Normals.Add(normal);

            Vertices.Add(offset + lengthDirection + widthDirection);
            UVs.Add(new Vector2(1.0f, 1.0f));
            Normals.Add(normal);

            Vertices.Add(offset + widthDirection);
            UVs.Add(new Vector2(1.0f, 0.0f));
            Normals.Add(normal);

            int baseIndex = Vertices.Count - 4;

            AddTriangle(baseIndex, baseIndex + 1, baseIndex + 2);
            AddTriangle(baseIndex, baseIndex + 2, baseIndex + 3);
        }
    }
}

