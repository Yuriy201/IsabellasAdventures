using UnityEngine;
using UnityEngine.UI;

public class MobileInputHandler : InputHandler
{
    public override Vector2 Directon => GetDirection();
    
    private MoveButton _leftWalkButton;
    private MoveButton _rightWalkButton;
    private Button _jumpButton;
    private Button _fireButton;
    
    public MobileInputHandler(MoveButton _leftWalk, MoveButton _rightWalk, Button jumpButton, Button fireButton)
    {
        _leftWalkButton = _leftWalk;
        _rightWalkButton = _rightWalk;
        _jumpButton = jumpButton;
        _fireButton = fireButton;

        _jumpButton.onClick.AddListener(InvokeJumpButtonAction);
        _fireButton.onClick.AddListener(InvokeFireButtonAction);
    }

    private Vector2 GetDirection() => new Vector2(_leftWalkButton.Direction + _rightWalkButton.Direction, 0);

    ~MobileInputHandler()
    {
        _jumpButton.onClick.RemoveListener(InvokeJumpButtonAction);
        _fireButton.onClick.RemoveListener(InvokeFireButtonAction);
    }
}


