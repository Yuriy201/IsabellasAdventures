using System;
using UnityEngine;

public abstract class InputHandler : MonoBehaviour
{
    public Vector2 Directon { get; protected set; }
    public event Action JumpButtonDown;
    
    private void Update()
    {
        GetDirection();
        InvokeJumpAction();
    }

    protected abstract void GetDirection();

    protected void InvokeJumpAction()
    {
        JumpButtonDown?.Invoke();
    }
}
