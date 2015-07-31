using strange.extensions.mediation.impl;
using UnityEngine;
using syscrawl.Game.Models.Levels;
using syscrawl.Game;
using syscrawl.Common.Extensions;
using syscrawl.Game.Views.Nodes;
using System.Linq;
using syscrawl.Common.Utils.Lerp;
using syscrawl.Game.Models;

namespace syscrawl.Game.Views.Levels
{
    public class LevelSceneMediator : Mediator
    {
        [Inject]
        public Configs Configs { get; set; }

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

            rotationLerp = new QuaternionLerp(Configs.levelRotateSettings);
            translateLerp = new VectorLerp(new LerpSettings());
            unrotationLerp = new QuaternionLerp(new LerpSettings());
//            LevelGeneratedSignal.AddListener(PositionNodes);
            PlayerMovedSignal.AddListener(PositionNodes);

            NodeWrapperClickedSignal.AddListener(NodeClicked);

            GenerateLevelSceneSignal.Dispatch();
        }

        QuaternionLerp rotationLerp;
        QuaternionLerp unrotationLerp;
        VectorLerp translateLerp;

        void Update()
        {
            rotationLerp.Update(Time.deltaTime);
            translateLerp.Update(Time.deltaTime);
            unrotationLerp.Update(Time.deltaTime);
        }

        void NodeClicked(NodeWrapperView wrapper)
        {
            // todo: destination should not be hardcoded
            var destination = new Vector3(20, 0, 0); 

            RotateLevel(wrapper.transform.position, destination);
        }

        float lastAngle = 0f;

        void RotateLevel(Vector3 fromPosition, Vector3 toPosition)
        {
            // angle based on 0,0,0 axis
            var angle = Vector3.Angle(toPosition, fromPosition);
            if (fromPosition.z < 0)
                angle *= -1;
            lastAngle = angle;

            var initialRotation = transform.rotation;
            var axisRotation = Quaternion.AngleAxis(angle, Vector3.up);
            var targetRotation = axisRotation * initialRotation;
            rotationLerp.Activate(
                initialRotation, targetRotation, 
                x => transform.rotation = x);

//            rotationLerp.LerpCompleted += TranslateLevel;
        }

        void UnrotateLevel()
        {
            var initialRotation = gameObject.transform.rotation;
            var axisRotation = Quaternion.AngleAxis(-lastAngle, Vector3.up);
            var targetRotation = axisRotation * initialRotation;
            unrotationLerp.Activate(
                initialRotation, targetRotation, 
                x => transform.rotation = x);
        }

        void TranslateLevel()
        {
            translateLerp.Activate(
                gameObject.transform.position,
                new Vector3(-20, 0, 0),
                x => gameObject.transform.position = x
            );

//            translateLerp.LerpCompleted += PositionNodes;
//            translateLerp.LerpCompleted += UnrotateLevel;
        }


        void PositionNodes()
        {
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
