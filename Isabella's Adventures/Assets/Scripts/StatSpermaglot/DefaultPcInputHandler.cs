using UnityEngine;

public class DefaultPcInputHandler: InputHandler
{
    private InputSettings _playerInput;
    
    public void Awake()
    {
        _playerInput = new InputSettings();
        _playerInput.Enable();
    }

    private void Update()
    {
        if (_playerInput.Gameplay.Jump.IsPressed())
        {
            InvokeJumpAction();
        }
        
        GetDirection();
    }
    
    protected override void GetDirection()
    {
        Directon = _playerInput.Gameplay.Movement.ReadValue<Vector2>().normalized;
    }
}