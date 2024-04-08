using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using Cinemachine;
using Freelf.Character.DataTransfer;
using Freelf.Character.Interfaces;
using UnityEngine;

namespace Freelf.Character
{
    public class CharacterMovement : CharacterComponent, IResultData<MovementData>, IAttached<MovementData>, ITick
    {
        private CharacterController characterController;
        public float Speed;
        public float RotationSpeed;

        public MovementData Data { get; private set; }

        private Camera playerCamera;

        private float _rotationVelocity;
        public override void Init()
        {
            characterController = gameObject.GetComponent<CharacterController>();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            playerCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        private void PerformMovement(AxisInput input)
        {
            // No optimal way: var direction = new Vector3(Input.GetAxis("Horizontal"), Vector3.zero.y, Input.GetAxis("Vertical"));
            // _direction.Set(input.Horizontal, Vector3.zero.y, input.Vertical);
            Data.direction.Set(input.Horizontal, Vector3.zero.y, input.Vertical);
        }

        private void CalculateMovement()
        {
            float movementSpeed = Speed;
            if (Data.direction == Vector3.zero) 
            {
                movementSpeed = 0f;
            }
            if (Data.direction.magnitude >= 0.1f) 
            {
                var targetRotation = CalculateRotation();

                Data.direction = Quaternion.Euler(0f, targetRotation, 0f) * Vector3.forward;
                characterController.Move(Data.direction.normalized*(movementSpeed*Time.deltaTime));
            }
        }

        private float CalculateRotation()
        {
            float targetRotation = Mathf.Atan2(Data.direction.x, Data.direction.z) * Mathf.Rad2Deg + playerCamera.transform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationVelocity, RotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, targetRotation, 0f);
            return targetRotation;
        }

        public void Tick()
        {
            PerformMovement(Data.input);
            CalculateMovement();
        }

        public void Attached(ref MovementData value)
        {
            Data = value;
        }
    }
}
