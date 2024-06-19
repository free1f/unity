using Freelf.Character.DataTransfer;
using UnityEngine;
using Freelf.Character.Interfaces;

namespace Freelf.Character
{
    public class CharacterCamera : CharacterComponent, IAttached<CameraData>, ILateTick
    {
        public bool LockCameraPosition = false;
        public float CameraTopLimit = 90f;
        public float CameraBottomLimit = -90f;
        public float CinemachineOverride;
        public float CinemachineVerticalSpeed = 20f;
        public float CinemachineHorizontalSpeed = 20f;
        public GameObject CameraTarget;

        private float _cinemachinePitch;
        private float _cinemachineYaw;
        
        private CameraData cameraData;
        
        public override void Init()
        {
            _cinemachineYaw = CameraTarget.transform.rotation.eulerAngles.y;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void CalculateCameraRotation()
        {
            var mouseHorizontal = cameraData.HorizontalMouseInput; //Input.GetAxis("Mouse X");
            var mouseVertical = cameraData.VerticalMouseInput; //Input.GetAxis("Mouse Y");
            var mouseMagnitude = Mathf.Sqrt(mouseHorizontal*mouseHorizontal + mouseVertical*mouseVertical);
        
            if(mouseMagnitude >= 0.01f && !LockCameraPosition)
            {
                var deltaTime = Time.deltaTime;
                _cinemachinePitch += mouseVertical * CinemachineVerticalSpeed * deltaTime;
                _cinemachineYaw += mouseHorizontal * CinemachineHorizontalSpeed * deltaTime;
            }

            _cinemachinePitch = ClampCamera(_cinemachinePitch, CameraBottomLimit, CameraTopLimit);
            _cinemachineYaw = ClampCamera(_cinemachineYaw, float.MinValue, float.MaxValue);
            CameraTarget.transform.rotation = Quaternion.Euler(_cinemachinePitch + CinemachineOverride, _cinemachineYaw, 0f);
        }

        private static float ClampCamera(float angle, float min, float max)
        {
            if (angle < -360f) angle += 360f;
            if (angle > 360f) angle -= 360f;
            return Mathf.Clamp(angle, min, max);
        }

        public void Attached(ref CameraData value)
        {
            cameraData = value;
        }

        public void LateTick()
        {
            CalculateCameraRotation();
        }
    }
}