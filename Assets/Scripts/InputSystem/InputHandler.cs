using UnityEngine;
using System;

public abstract class InputHandler
{
    public abstract Vector2 Directon { get;}

    public event Action JumpButtonDown;
    public event Action FireButtonDown;

    public event Action AltFireButtonDown; 

    protected void InvokeFireButtonAction() => FireButtonDown?.Invoke();
    protected void InvokeAltFireButtonAction() => AltFireButtonDown?.Invoke();
    protected void InvokeJumpButtonAction() => JumpButtonDown?.Invoke();
    
}
