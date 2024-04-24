using System;
using UnityEngine;

public interface IInput
{
    public event Action<Vector2> Move;
    public event Action<bool> Jump;
}