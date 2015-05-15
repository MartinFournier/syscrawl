using UnityEngine;
using syscrawl.Utils.Lerp;
using syscrawl.Levels;

namespace syscrawl.Camera
{
    public class CameraController : MonoBehaviour
    {
        UnityEngine.Camera mainCamera;

        Lerp<Vector3> cameraPositionLerp;
        Lerp<float> cameraZoomLerp;
        Lerp<float> cameraUnzoomLerp;
       
        // Use this for initialization
        void Start()
        {
            mainCamera = UnityEngine.Camera.main;

            var lerpSettings = new LerpSettings() { Duration = 0.5f };

            cameraPositionLerp = new VectorLerp(lerpSettings);
            cameraZoomLerp = new FloatLerp(lerpSettings);
            cameraUnzoomLerp = new FloatLerp(lerpSettings);

            cameraPositionLerp.LerpActivated += () => cameraZoomLerp.Activate(25, 0);
            cameraZoomLerp.LerpCompleted += () => cameraUnzoomLerp.Activate(0, 25);

        }

        public void BindPositioning(Positioning positioning)
        {
            positioning.MovedToNode += (pos) =>
                cameraPositionLerp.Activate(mainCamera.transform.position, pos);
        }

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
