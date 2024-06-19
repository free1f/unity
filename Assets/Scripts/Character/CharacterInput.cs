using Freelf.Character.DataTransfer;
using Freelf.Character.Interfaces;
using UnityEngine;

namespace Freelf.Character
{
    public class CharacterInput : CharacterComponent, 
        IPreTick, 
        IAttached<JumpData>, 
        IAttached<InteractData>,
        IAttached<UseItemData>,
        IAttached<MovementData>,
        IAttached<CameraData>
    {
        public KeyCode JumpKey = KeyCode.Space;
        public KeyCode InteractKey = KeyCode.E;

        private JumpData _jumpData;
        private InteractData _interactData;
        private UseItemData _useItemData;
        private MovementData _movementData;
        private CameraData _cameraData;

        private void GetJumpInput()
        {
            _jumpData.input.IsHold = Input.GetKey(JumpKey);
            _jumpData.input.IsPressed = Input.GetKeyDown(JumpKey);
            _jumpData.input.IsReleased = Input.GetKeyUp(JumpKey);
        }

        private void GetInteractInput()
        {
            _interactData.input.IsHold = Input.GetKey(InteractKey);
            _interactData.input.IsPressed = Input.GetKeyDown(InteractKey);
            _interactData.input.IsReleased = Input.GetKeyUp(InteractKey);
        }

        private void GetUseItemInput()
        {
            _useItemData.input.IsHold = Input.GetMouseButton(0);
            _useItemData.input.IsPressed = Input.GetMouseButtonDown(0);
            _useItemData.input.IsReleased = Input.GetMouseButtonUp(0);
        }

        private void GetMovementInput()
        {
            _movementData.input.Horizontal = Input.GetAxis("Horizontal");
            _movementData.input.Vertical = Input.GetAxis("Vertical");
        }

        private void GetCameraInput()
        {
            _cameraData.HorizontalMouseInput = Input.GetAxis("Mouse X");
            _cameraData.VerticalMouseInput = Input.GetAxis("Mouse Y");
        }

        public override void Init()
        {
            // pass
        }

        public void PreTick()
        {
            GetJumpInput();
            GetInteractInput();
            GetUseItemInput();
            GetMovementInput();
            GetCameraInput();
        }

        public void Attached(ref JumpData value)
        {
            _jumpData = value;
        }

        public void Attached(ref InteractData value)
        {
            _interactData = value;
        }

        public void Attached(ref UseItemData value)
        {
            _useItemData = value;
        }

        public void Attached(ref MovementData value)
        {
            _movementData = value;
        }

        public void Attached(ref CameraData value)
        {
            _cameraData = value;
        }
    }
}
