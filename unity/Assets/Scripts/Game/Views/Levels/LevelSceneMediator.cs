using strange.extensions.mediation.impl;
using UnityEngine;
using syscrawl.Game.Models.Levels;
using syscrawl.Game;
using syscrawl.Common.Extensions;
using syscrawl.Game.Views.Nodes;
using System.Linq;
using syscrawl.Common.Utils.Lerp;

namespace syscrawl.Game.Views.Levels
{
    public class LevelSceneMediator : Mediator
    {
        [Inject]
        public LevelSceneView View { get; set; }

        [Inject]
        public GenerateLevelSignal GenerateLevelSceneSignal { get; set; }

        [Inject]
        public LevelGeneratedSignal LevelGeneratedSignal { get; set; }

        [Inject]
        public PlayerMovedSignal PlayerMovedSignal { get; set; }

        [Inject]
        public PositionNodesSignal PositionNodesSignal { get; set; }

        [Inject]
        public NodeWrapperClickedSignal NodeWrapperClickedSignal { get; set; }

        public override void OnRegister()
        {
            View.Init();

            rotationLerp = new QuaternionLerp(new LerpSettings());

//            LevelGeneratedSignal.AddListener(PositionNodes);
            PlayerMovedSignal.AddListener(PositionNodes);

            NodeWrapperClickedSignal.AddListener(NodeClicked);

            GenerateLevelSceneSignal.Dispatch();
        }

        QuaternionLerp rotationLerp;

        void Update()
        {
            if (rotationLerp != null && !rotationLerp.IsComplete)
            {
                var value = rotationLerp.Evaluate(Time.deltaTime);
                transform.rotation = value;
            }
        }

        void NodeClicked(NodeWrapperView wrapper)
        {
            // todo: Get view.currentNode first node transform
            var destination = new Vector3(20, 0, 0); 
            // todo: destination should not be hardcoded


            // angle based on 0,0,0 axis
            var angle = Vector3.Angle(destination, wrapper.transform.position);
            if (wrapper.transform.position.z < 0)
                angle *= -1;

            var initialRotation = gameObject.transform.rotation;
            var axisRotation = Quaternion.AngleAxis(angle, Vector3.up);
            var targetRotation = axisRotation * initialRotation;
            rotationLerp.Activate(initialRotation, targetRotation);

            Debug.Log(initialRotation);
            Debug.Log(targetRotation);
            Debug.Log(angle);
        }

        void PositionNodes()
        {
            Debug.Log("Positioning nodes");
            PositionNodesSignal.Dispatch(this);
        }

        public void RemoveNodes()
        {
            View.activeNodes.DestroyChildren();
            View.currentNodes.DestroyChildren();
            View.furtherAheadNodes.DestroyChildren();
            View.previousNodes.DestroyChildren();
        }

        public GameObject GetNodeContainerForType(SceneNodeType type)
        {
            var container = View.activeNodes;
            switch (type)
            {
                case SceneNodeType.Active:
                    container = View.activeNodes;
                    break;
                case SceneNodeType.Current:
                    container = View.currentNodes;
                    break;
                case SceneNodeType.FurtherAhead:
                    container = View.furtherAheadNodes;
                    break;
                case SceneNodeType.Previous:
                    container = View.previousNodes;
                    break;

            }
            return container;
        }
    }
}
