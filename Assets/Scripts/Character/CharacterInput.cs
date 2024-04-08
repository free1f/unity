using System.Collections;
using System.Collections.Generic;
using Freelf.Character.DataTransfer;
using UnityEngine;

namespace Freelf.Character
{
    public class CharacterInput : MonoBehaviour
    {
        public KeyCode JumpKey = KeyCode.Space;
        public KeyCode InteractKey = KeyCode.E;

        public void GetJumpInput(ref JumpData data)
        {
            data.input.IsHold = Input.GetKey(JumpKey);
            data.input.IsPressed = Input.GetKeyDown(JumpKey);
            data.input.IsReleased = Input.GetKeyUp(JumpKey);
        }

        public void GetInteractInput(ref InteractData data)
        {
            data.input.IsHold = Input.GetKey(InteractKey);
            data.input.IsPressed = Input.GetKeyDown(InteractKey);
            data.input.IsReleased = Input.GetKeyUp(InteractKey);
        }

        public void GetUseItemInput(ref UseItemData data)
        {
            data.input.IsHold = Input.GetMouseButton(0);
            data.input.IsPressed = Input.GetMouseButtonDown(0);
            data.input.IsReleased = Input.GetMouseButtonUp(0);
        }

        public void GetMovementInput(ref MovementData data)
        {
            data.input.Horizontal = Input.GetAxis("Horizontal");
            data.input.Vertical = Input.GetAxis("Vertical");
        }
    }
}
