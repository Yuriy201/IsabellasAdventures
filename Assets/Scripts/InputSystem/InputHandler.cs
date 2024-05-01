using UnityEngine;
using System;

public abstract class InputHandler
{
    public abstract Vector2 Directon { get;}

    public event Action JumpButtonDown;
    public event Action FireButtonDown; 

    protected void InvokeFireButtonAction() => FireButtonDown?.Invoke();

    protected void InvokeJumpButtonAction() => JumpButtonDown?.Invoke();
}
