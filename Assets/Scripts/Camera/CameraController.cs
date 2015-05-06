using UnityEngine;

namespace syscrawl.Camera
{
    public class CameraController : MonoBehaviour
    {

        // How fast the camera moves
        public int cameraVelocity = 20;
        public int zoomVelocity = 3;
        public int rotateIncrement = 45;

        // Use this for initialization
        void Start()
        {
	
        }

        void Update()
        {
            // Left
            if ((Input.GetKey(KeyCode.A)))
            {
                transform.Translate((Vector3.left * cameraVelocity) * Time.deltaTime);
            }
            // Right
            if ((Input.GetKey(KeyCode.D)))
            {
                transform.Translate((Vector3.right * cameraVelocity) * Time.deltaTime);
            }
            // Up
            if ((Input.GetKey(KeyCode.W)))
            {
                transform.Translate((Vector3.up * cameraVelocity) * Time.deltaTime);
            }
            // Down
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate((Vector3.down * cameraVelocity) * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                transform.Rotate(0, rotateIncrement, 0, Space.World);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                transform.Rotate(0, -rotateIncrement, 0, Space.World);
            }
            var mainCamera = UnityEngine.Camera.main;
            var mouseAxis = Input.GetAxis("Mouse ScrollWheel");
            if (mouseAxis > 0)
            {
                mainCamera.orthographicSize = mainCamera.orthographicSize - zoomVelocity;
            }
            else if (mouseAxis < 0)
            {
                mainCamera.orthographicSize = mainCamera.orthographicSize + zoomVelocity;
            }

            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 3, 50);
        }
    }
}
