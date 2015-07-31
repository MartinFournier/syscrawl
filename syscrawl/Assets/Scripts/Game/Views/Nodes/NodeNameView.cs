using System;
using strange.extensions.mediation.impl;
using syscrawl.Common.Extensions;
using UnityEngine;
using syscrawl.Common.Utils;
using syscrawl.Game.Camera;

namespace syscrawl.Game.Views.Nodes
{
    public class NodeNameView : View
    {
        GameObject nodeName;
        TextMesh nodeNameMesh;

        //TODO: this view should not have access to the camera.
        [Inject]
        public UnityEngine.Camera cameraView { get; set; }

        internal void Init(string name)
        {
            nodeName = Prefabs.Instantiate("NodeName", gameObject);
            nodeNameMesh = nodeName.GetComponent<TextMesh>();
            nodeNameMesh.text = name;

            nodeName.transform.localPosition = new Vector3(0, -5, 0);
            nodeName.transform.localRotation = Quaternion.identity;
        }

        void Update()
        {
            var cameraTransform = cameraView.transform;
            gameObject.transform.rotation = cameraTransform.localRotation;
//            transform.rotation = Quaternion.LookRotation(nodeName.transform.position - cameraTransform.position);
        }
    }
}

