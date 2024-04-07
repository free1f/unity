using Freelf.Character.Interfaces;
using UnityEngine;

namespace Freelf.Character
{
    public class CharacterJump : CharacterComponent
    {
        private CharacterController _characterController;

        public float JumpHeight = 2.0f;
        public float Gravity = -9.81f;
        public float MaxJumpPressTime = 0.5f;

        private bool _isJumping;
        private float _jumpPressTimeCounter;
        private Vector3 _jumpVelocity;
        public override void Init()
        {
            _characterController = GetComponent<CharacterController>();
            _jumpPressTimeCounter = MaxJumpPressTime;
        }

        public void CheckGround() 
        {
            if (_characterController.isGrounded && _jumpVelocity.y < 0)
            {
                _jumpVelocity.y = 0f;
            }
        }

        public void CalculateJump()
        {
            _jumpVelocity.y += Gravity * Time.deltaTime;
            _characterController.Move(_jumpVelocity * Time.deltaTime);
        }

        public void PerformJump(PressedInput input)
        {
            if (_characterController.isGrounded && input.IsPressed)
            {
                _isJumping = true;
                _jumpPressTimeCounter = MaxJumpPressTime;
                _jumpVelocity.y += Mathf.Sqrt(JumpHeight * -3.0f * Gravity);
            }

            if (input.IsHold && _isJumping)
            {
                if (_jumpPressTimeCounter > 0)
                {
                    _jumpVelocity.y += Mathf.Sqrt(JumpHeight * -3.0f * Gravity) * (Time.deltaTime / MaxJumpPressTime);
                    _jumpPressTimeCounter -= Time.deltaTime;
                }
                else
                {
                    _isJumping = false;
                }
            }

            if (input.IsReleased)
            {
                _isJumping = false;
            }
        }
    }
}