using UnityEngine;
using UnityEngine.UI;

public class MobileInputContainer : MonoBehaviour
{
    public Joystick Joystick => _joystick;
    public MoveButton MoveButtonLeft => _moveButtonLeft;
    public MoveButton MoveButtonRight => _moveButtonRight;
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
    [SerializeField] private MoveButton _moveButtonLeft;
    [SerializeField] private MoveButton _moveButtonRight;

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

public enum ControlType
{
    Joystick,
    Buttons
}
