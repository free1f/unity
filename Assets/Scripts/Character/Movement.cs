using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField]
    private CinemachineFreeLook freelookCamera;
    public float Speed;
    public float RotationSpeed;
    private Vector3 _direction = Vector3.zero;
    private Quaternion _rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateInput();
        CalculateMovement();
        CalculateRotation();
    }

    private void CalculateInput()
    {
        // No optimal way: var direction = new Vector3(Input.GetAxis("Horizontal"), Vector3.zero.y, Input.GetAxis("Vertical"));
        _direction.Set(Input.GetAxis("Horizontal"), Vector3.zero.y, Input.GetAxis("Vertical"));
    }

    private void CalculateMovement()
    {
        characterController.Move(_direction*(Speed*Time.deltaTime));
    }

    private void CalculateRotation()
    {
        var targetDirection = new Vector3(_direction.x, Vector3.zero.y, _direction.z).normalized;
        var forward = Quaternion.Euler(Vector3.zero.x, freelookCamera.m_XAxis.Value, Vector3.zero.z)*Vector3.forward;
        forward.y = Vector3.zero.x; // result 0f -> 0 float type
        forward.Normalize();

        Quaternion targetRotation;
        if (Mathf.Approximately(Vector3.Dot(targetDirection, Vector3.forward), Vector3.back.z))
        {
            targetRotation = Quaternion.LookRotation(-forward);
        } 
        else 
        {
            Quaternion cameraOffset = Quaternion.FromToRotation(Vector3.forward, targetDirection);
            targetRotation = Quaternion.LookRotation(cameraOffset*forward);
        }

        _rotation = targetRotation;

        _rotation = Quaternion.RotateTowards(transform.rotation, _rotation, RotationSpeed*Time.deltaTime);
        transform.rotation = _rotation;
    }
}
