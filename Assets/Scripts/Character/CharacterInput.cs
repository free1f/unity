using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freelf.Character
{
    public class CharacterInput : MonoBehaviour
    {
        public KeyCode JumpKey = KeyCode.Space;
        public KeyCode InteractKey = KeyCode.E;
        private PressedInput _jumpInput;
        private PressedInput _interactInput;
        private PressedInput _useItemInput;
        private AxisInput _movementInput;

        public PressedInput GetJumpInput()
        {
            _jumpInput.IsHold = Input.GetKey(JumpKey);
            _jumpInput.IsPressed = Input.GetKeyDown(JumpKey);
            _jumpInput.IsReleased = Input.GetKeyUp(JumpKey);

            return _jumpInput;
        }

        public PressedInput GetInteractInput()
        {
            _interactInput.IsHold = Input.GetKey(InteractKey);
            _interactInput.IsPressed = Input.GetKeyDown(InteractKey);
            _interactInput.IsReleased = Input.GetKeyUp(InteractKey);

            return _interactInput;
        }

        public PressedInput GetUseItemInput()
        {
            _useItemInput.IsHold = Input.GetMouseButton(0);
            _useItemInput.IsPressed = Input.GetMouseButtonDown(0);
            _useItemInput.IsReleased = Input.GetMouseButtonUp(0);

            return _useItemInput;
        }

        public AxisInput GetMovementInput()
        {
            _movementInput.Horizontal = Input.GetAxis("Horizontal");
            _movementInput.Vertical = Input.GetAxis("Vertical");

            return _movementInput;
        }
    }

    public struct PressedInput
    {
        public bool IsHold;
        public bool IsPressed;
        public bool IsReleased;
    }

    public struct AxisInput
    {
        public float Horizontal;
        public float Vertical;
    }
}
