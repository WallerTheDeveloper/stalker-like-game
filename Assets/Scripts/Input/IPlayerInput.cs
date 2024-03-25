using System;
using Core;
using Core.DISystem;
using UnityEngine;

namespace Input
{
    public interface IPlayerInput : IDependency
    {
        Vector2 MovementInputValue { get; }
        Vector2 LookInputValue { get; }
        Action<bool> OnCrouchPerformedTriggered { get; set; }
        Action<bool> OnSprintPerformedTriggered { get; set; }
        Action<bool> OnJumpPerformedTriggered { get; set; }
        Action<bool> OnCrouchCanceledTriggered { get; set; }
        Action<bool> OnSprintCanceledTriggered { get; set; }
        Action<bool> OnJumpCanceledTriggered { get; set; }
        Action<bool> OnShootStartedTriggered { get; set; }
        Action<bool> OnShootCanceledTriggered { get; set; }
        Action OnAimPerformedTriggered { get; set; }
        Action OnAimCanceledTriggered { get; set; }
    }
}