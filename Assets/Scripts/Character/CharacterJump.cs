using Freelf.Character.DataTransfer;
using Freelf.Character.Interfaces;
using UnityEngine;

namespace Freelf.Character
{
    public class CharacterJump : CharacterComponent, ITick, IAttached<JumpData>
    {
        private CharacterController _characterController;

        public float JumpHeight = 2.0f;
        public float Gravity = -9.81f;
        public float MaxJumpPressTime = 0.5f;

        private bool _isJumping;
        private float _jumpPressTimeCounter;
        private Vector3 _jumpVelocity;
        private JumpData Data;
        public override void Init()
        {
            _characterController = GetComponent<CharacterController>();
            _jumpPressTimeCounter = MaxJumpPressTime;
        }

        private void CheckGround() 
        {
            if (_characterController.isGrounded && _jumpVelocity.y < 0)
            {
                _jumpVelocity.y = 0f;
            }
        }

        private void CalculateJump()
        {
            _jumpVelocity.y += Gravity * Time.deltaTime;
            _characterController.Move(_jumpVelocity * Time.deltaTime);
        }

        private void PerformJump(PressedInput input)
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

        public void Tick()
        {
            CheckGround();
            CalculateJump();
            PerformJump(Data.input);
        }

        public void Attached(ref JumpData value)
        {
            Data = value;
        }
    }
}