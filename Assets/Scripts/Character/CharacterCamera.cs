using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    private float CinemachinePitch;
    private float CinemachineYaw;
    public bool LockCameraPosition = false;
    public float CameraTopLimit = 90f;
    public float CameraBottomLimit = -90f;
    public float CinemachineOverride;
    public float CinemachineVerticalSpeed = 20f;
    public float CinemachineHorizontalSpeed = 20f;
    public GameObject CameraTarget;

    void Start()
    {
        CinemachineYaw = CameraTarget.transform.rotation.eulerAngles.y;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void CalculateCameraRotation()
    {
        float mouseHorizontal = Input.GetAxis("Mouse X");
        float mouseVertical = Input.GetAxis("Mouse Y");
        float mouseMagnitude = Mathf.Sqrt(mouseHorizontal*mouseHorizontal + mouseVertical*mouseVertical);
    
        if(mouseMagnitude >= 0.01f && !LockCameraPosition)
        {
            float deltaTime = Time.deltaTime;
            CinemachinePitch += mouseVertical * CinemachineVerticalSpeed * deltaTime;
            CinemachineYaw += mouseHorizontal * CinemachineHorizontalSpeed * deltaTime;
        }

        CinemachinePitch = ClampCamera(CinemachinePitch, CameraBottomLimit, CameraTopLimit);
        CinemachineYaw = ClampCamera(CinemachineYaw, float.MinValue, float.MaxValue);
        CameraTarget.transform.rotation = Quaternion.Euler(CinemachinePitch + CinemachineOverride, CinemachineYaw, 0f);
    }

    private float ClampCamera(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
