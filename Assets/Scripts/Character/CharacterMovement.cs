using Freelf.Character.DataTransfer;
using Freelf.Character.Interfaces;
using UnityEngine;

namespace Freelf.Character
{
    public class CharacterMovement : CharacterComponent, IAttached<MovementData>, ITick
    {
        private CharacterController characterController;
        public float Speed;
        public float RotationSpeed;

        private MovementData _movementData;

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
            _movementData.direction.Set(input.Horizontal, Vector3.zero.y, input.Vertical);
        }

        private void CalculateMovement()
        {
            float movementSpeed = Speed;
            if (_movementData.direction == Vector3.zero) 
            {
                movementSpeed = 0f;
            }
            if (_movementData.direction.magnitude >= 0.1f) 
            {
                var targetRotation = CalculateRotation();

                _movementData.direction = Quaternion.Euler(0f, targetRotation, 0f) * Vector3.forward;
                characterController.Move(_movementData.direction.normalized*(movementSpeed*Time.deltaTime));
            }
        }

        private float CalculateRotation()
        {
            float targetRotation = Mathf.Atan2(_movementData.direction.x, _movementData.direction.z) * Mathf.Rad2Deg + playerCamera.transform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationVelocity, RotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, targetRotation, 0f);
            return targetRotation;
        }

        public void Tick()
        {
            PerformMovement(_movementData.input);
            CalculateMovement();
        }

        public void Attached(ref MovementData value)
        {
            _movementData = value;
        }
    }
}
