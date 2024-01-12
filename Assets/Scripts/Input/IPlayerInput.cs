using System;
using UnityEngine;

namespace Input
{
    public interface IPlayerInput
    {
        Action OnJumpTriggered { get; set; }
        Vector2 MovementInputValue { get; }
        Vector2 LookInputValue { get; }
    }
}