using UnityEngine;
using UnityEngine.UI;

public class MobileInputHandler : InputHandler
{
    public override Vector2 Directon => _direction;

    private Vector2 _direction = Vector2.zero;
    
    private Button _leftWalkButton;
    private Button _rightWalkButton;
    
    public MobileInputHandler(Button _leftWalk, Button _rightWalk)
    {
        _leftWalkButton = _leftWalk;
        _rightWalkButton = _rightWalk;
        _leftWalkButton.onClick.AddListener(() => ButtonCallBackContext(_leftWalkButton));
        _rightWalkButton.onClick.AddListener(() => ButtonCallBackContext(_rightWalkButton));
    }

    private void ButtonCallBackContext(Button buttonPressed)
    {
        if (buttonPressed == _leftWalkButton)
        {
            _direction.x += -1f;
        }
        else if (buttonPressed == _rightWalkButton)
        {
            _direction.x += 1f;
        }
        else
        {
            _direction = Vector2.zero;
            Debug.Log("стас далбаеб");
        }
    }

    private void OnDisable()
    {
        _leftWalkButton.onClick.RemoveAllListeners();
        _rightWalkButton.onClick.RemoveAllListeners();
    }
}


