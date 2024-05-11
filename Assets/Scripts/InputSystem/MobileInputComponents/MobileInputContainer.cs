using UnityEngine;
using UnityEngine.UI;

public class MobileInputContainer : MonoBehaviour
{
    public Joystick Joystick => _joystick;
    public MoveButton MoveButtonLeft => _moveButtonLeft;
    public MoveButton MoveButtonRight => _moveButtonRight;
    public Button JumpButton => _jumpButton;
    public Button FireButton => _fireButton;
    public ControlType ControlType => _controlType;

    [SerializeField] public ControlType _controlType;

    [Space]
    [SerializeField] private Joystick _joystick;
    [SerializeField] private MoveButton _moveButtonLeft;
    [SerializeField] private MoveButton _moveButtonRight;
    [SerializeField] private Button _jumpButton;
    [SerializeField] private Button _fireButton;

    private void OnValidate()
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
