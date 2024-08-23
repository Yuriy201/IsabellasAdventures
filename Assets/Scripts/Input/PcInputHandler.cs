using UnityEngine;

namespace InputSystem
{
    public class PcInputHandler : InputHandler
    {
        public override Vector2 Directon => GetDirection();

        private InputSettings _inputSettings;      

        public PcInputHandler()
        {
            _inputSettings = new InputSettings();
            _inputSettings.Enable();

            _inputSettings.Gameplay.Jump.performed += Jump_performed;
            _inputSettings.Gameplay.Jump.canceled += Jump_canceled;
            _inputSettings.Gameplay.Fire.performed += invokeEvent => FireButtonEvent();
            _inputSettings.Gameplay.AltFire.performed += invokeEvent => InvokeAltFireButtonAction();
            _inputSettings.Gameplay.Sprint.performed += Sprint_performed;
            _inputSettings.Gameplay.Sprint.canceled += Sprint_canceled;
        }

        private void Jump_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            JumpButtonUpEvent();

            Jump = false;
        }

        private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            JumpButtonEvent();

            Jump = true;
        }

        private void Sprint_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            Sprinting = obj.ReadValueAsButton();
        }

        private void Sprint_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            Sprinting = obj.ReadValueAsButton();
        }

        //get data
        private Vector2 GetDirection() => _inputSettings.Gameplay.Movement.ReadValue<Vector2>();
        private void JumpButtonEvent() => InvokeJumpButtonAction();
        private void JumpButtonUpEvent() => InvokeJumpButtonUpAction();
        private void FireButtonEvent() => InvokeFireButtonAction();
        private void AltFireButtonEvent() => InvokeAltFireButtonAction();
    }
}