using UnityEngine;
using syscrawl.Common.Utils.Lerp;

namespace syscrawl.Game.Camera
{
    public class CameraController : MonoBehaviour
    {
        UnityEngine.Camera mainCamera;

        Lerp<Vector3> cameraPositionLerp;
        Lerp<float> cameraZoomLerp;
        Lerp<float> cameraUnzoomLerp;
       
        public LerpSettings positionLerpSettings;
        public LerpSettings zoomLerpSettings;
        public LerpSettings unzoomLerpSettings;

        public float cameraZoomTo = 1;
        public float cameraHeight = 25;

        // Use this for initialization
        void Start()
        {
            mainCamera = UnityEngine.Camera.main;

            cameraPositionLerp = new VectorLerp(positionLerpSettings);
            cameraZoomLerp = new FloatLerp(zoomLerpSettings);
            cameraUnzoomLerp = new FloatLerp(unzoomLerpSettings);

//            cameraPositionLerp.LerpActivated += () => 
//                cameraZoomLerp.Activate(cameraHeight, cameraZoomTo);
//
//            cameraZoomLerp.LerpCompleted += () => 
//                cameraUnzoomLerp.Activate(cameraZoomTo, cameraHeight);
        }

        //        public void BindPositioning(Positioning positioning)
        //        {
        //            positioning.MovedToNode += (pos) =>
        //                cameraPositionLerp.Activate(mainCamera.transform.position, pos);
        //        }
        //
        void Update()
        {
            if (!cameraPositionLerp.IsComplete)
            {
                var value = cameraPositionLerp.Evaluate(Time.deltaTime);
                mainCamera.transform.position = value;
            }

            if (!cameraZoomLerp.IsComplete)
            {
                var value = cameraZoomLerp.Evaluate(Time.deltaTime);
                mainCamera.orthographicSize = value;
            }

            if (!cameraUnzoomLerp.IsComplete)
            {
                var value = cameraUnzoomLerp.Evaluate(Time.deltaTime);
                mainCamera.orthographicSize = value;
            }
        }
    }
}
