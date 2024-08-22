using UnityEngine;

namespace InputSystem
{
    public class MobileInputHandler : InputHandler
    {
        public override Vector2 Directon => Vector2.zero;
            //GetDirection();

        private MobileInputContainer _inputContainer;

        public MobileInputHandler(MobileInputContainer inputContainer)
        {
            _inputContainer = inputContainer;

            _inputContainer.JumpButton.onClick.AddListener(InvokeJumpButtonAction);
            _inputContainer.FireButton.onClick.AddListener(InvokeFireButtonAction);
            _inputContainer.AltFireButton.onClick.AddListener(InvokeAltFireButtonAction);
        }

        /* useless for now
        private Vector2 GetDirection()
        {
            switch (_inputContainer.ControlType)
            {
                case ControlType.Joystick:
                    return _inputContainer.Joystick.Direction;
                case ControlType.Buttons:
                    return new Vector2(_inputContainer.MoveButtonLeft.Direction + _inputContainer.MoveButtonRight.Direction, 0);
                default:
                    return Vector2.zero;
            }
        }
        */

        ~MobileInputHandler()
        {
            _inputContainer.JumpButton.onClick.RemoveListener(InvokeJumpButtonAction);
            _inputContainer.FireButton.onClick.RemoveListener(InvokeFireButtonAction);
            _inputContainer.AltFireButton.onClick.RemoveListener(InvokeAltFireButtonAction);
        }
    }
}