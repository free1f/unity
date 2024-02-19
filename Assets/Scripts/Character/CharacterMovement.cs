using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using Cinemachine;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController characterController;
    public float Speed;
    public float RotationSpeed;
    private Vector3 _direction = Vector3.zero;
    public Vector3 Direction => _direction;
    private Camera playerCamera;

    private float _rotationVelocity;

    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    public void CalculateInput()
    {
        // No optimal way: var direction = new Vector3(Input.GetAxis("Horizontal"), Vector3.zero.y, Input.GetAxis("Vertical"));
        _direction.Set(Input.GetAxis("Horizontal"), Vector3.zero.y, Input.GetAxis("Vertical"));
    }

    public void CalculateMovement()
    {
        float movementSpeed = Speed;
        if (_direction == Vector3.zero) 
        {
            movementSpeed = 0f;
        }
        if (_direction.magnitude >= 0.1f) 
        {
            var targetRotation = CalculateRotation();

            _direction = Quaternion.Euler(0f, targetRotation, 0f) * Vector3.forward;
            characterController.Move(_direction.normalized*(movementSpeed*Time.deltaTime));
        }
    }

    private float CalculateRotation()
    {
        float targetRotation = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + playerCamera.transform.eulerAngles.y;
        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationVelocity, RotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, targetRotation, 0f);
        return targetRotation;
    }
}
