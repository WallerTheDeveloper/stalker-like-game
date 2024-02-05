using System;
using Core;
using Core.DISystem;
using UnityEngine;

namespace Input
{
    public interface IPlayerInput : IDependency
    {
        Action OnJumpTriggered { get; set; }
        Vector2 MovementInputValue { get; }
        Vector2 LookInputValue { get; }
    }
}