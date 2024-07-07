using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.UI;

namespace InputSystem
{
    public class MobileInputContainer : MonoBehaviour
    {
        [InputControl(layout = "Vector2")]
        public string _mobileControlPath;

        public Joystick Joystick => _joystick;
        public Transform MoveButtonLeft => _moveButtonLeft;
        public Transform MoveButtonRight => _moveButtonRight;
        public Button JumpButton => _jumpButton;
        public Button FireButton => _fireButton;
        public Button AltFireButton => _altFireButton;
        public ControlType ControlType => _controlType;

        [Space]
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private ControlType _controlType;
        [SerializeField] private GameObject _ui;

        [Space]
        [SerializeField] private Joystick _joystick;

        [Space]
        [SerializeField] private Transform _moveButtonLeft;
        [SerializeField] private Transform _moveButtonRight;

        [Space]
        [SerializeField] private Button _jumpButton;
        [SerializeField] private Button _fireButton;
        [SerializeField] private Button _altFireButton;

        private void Start()
        {
            CheckControlType();
            CheckPlatformType();
        }

        private void OnValidate()
        {
            CheckControlType();
            CheckPlatformType();

            _joystick.m_ControlPath = _mobileControlPath;
        }

        private void CheckPlatformType()
        {
            switch (_gameConfig.PlatfotmType)
            {
                case PlatfotmType.PC:
                    _ui.SetActive(false);
                    break;
                case PlatfotmType.Mobile:
                    _ui.SetActive(true);
                    break;
            }
        }

        public void CheckPlatformType(bool isMobile)
        {
            _ui.SetActive(isMobile);
        }

        private void CheckControlType()
        {
            switch (_controlType)
            {
                case ControlType.Buttons:
                    _moveButtonLeft.gameObject.SetActive(true);
                    _moveButtonRight.gameObject.SetActive(true);
                    _joystick.gameObject.SetActive(false);
                    break;

                case ControlType.Joystick:
                    _moveButtonLeft.gameObject.SetActive(false);
                    _moveButtonRight.gameObject.SetActive(false);
                    _joystick.gameObject.SetActive(true);
                    break;
            }
        }
    }
}