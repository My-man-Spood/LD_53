using UnityEngine;

namespace EasyDragAndDrop.DemoScene.Demo3D
{
    public class CameraOrbit : MonoBehaviour
    {
        public Transform orbitTarget;
        public float distance = 15f;

        public float xMinLimit = 0f;
        public float xMaxLimit = 80f;
        public float yMinLimit = 0f;
        public float yMaxLimit = 80f;

        public float distanceMin = .5f;
        public float distanceMax = 15f;

        private float _cameraX;
        private float _cameraY;
        private bool _isDrag;

        private void Start()
        {
            var angles = transform.eulerAngles;
            _cameraX = angles.y;
            _cameraY = angles.x;
        }

        private void LateUpdate()
        {
            if (!orbitTarget) return;
            if (!Input.GetMouseButton(1)) return;

            UpdateCameraPosition();
            UpdateCameraDistance();
        }

        private void UpdateCameraPosition()
        {
            _cameraX += Input.GetAxis("Mouse X") * distance * 2f;
            _cameraY -= Input.GetAxis("Mouse Y") * 2f;

            _cameraX = ClampAngle(_cameraX, xMinLimit, xMaxLimit);
            _cameraY = ClampAngle(_cameraY, yMinLimit, yMaxLimit);
        }

        private void UpdateCameraDistance()
        {
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
            if (Physics.Linecast(orbitTarget.position, transform.position, out var hit))
            {
                distance -= hit.distance;
            }

            var cameraTransform = transform;
            cameraTransform.rotation = Quaternion.Euler(_cameraY, _cameraX, 0);
            cameraTransform.position = cameraTransform.rotation * new Vector3(0.0f, 0.0f, -distance) +
                                       orbitTarget.position;
        }

        private static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }
    }
}