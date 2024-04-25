using UnityEngine;

public class PcInputHandler: InputHandler
{
    private InputSettings _playerInput;
    
    public void Awake()
    {
        _playerInput = new InputSettings();
        _playerInput.Enable();
    }

    private void Update()
    {
        GetDirection();
        if (_playerInput.Gameplay.Jump.IsPressed()) InvokeJumpAction();
        if (_playerInput.Gameplay.Fire.IsPressed()) InvokeFireAction();
    }
    
    protected override void GetDirection()
    {
        Directon = _playerInput.Gameplay.Movement.ReadValue<Vector2>().normalized;
    }
}