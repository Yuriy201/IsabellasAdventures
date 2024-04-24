using System;
using UnityEngine;

public class DesctopInput: MonoBehaviour, IInput
{
    public event Action<Vector2> Move;
    public event Action<bool> Jump;

    
    private InputSettings _playerInput;
    
    public void Awake()
    {
        _playerInput = new InputSettings();
        _playerInput.Enable();
    }
    
    private void GetInputDirection()
    {
        if (_playerInput.Gameplay.Jump.IsPressed())
        {
            Jump?.Invoke(true);
        }
        else
        {
            Jump?.Invoke(false);
        }
        
        var _direction = _playerInput.Gameplay.Movement.ReadValue<Vector2>().normalized;
        Move?.Invoke(_direction);
    }
    
    public void Update()
    {
        GetInputDirection();
    }
}
