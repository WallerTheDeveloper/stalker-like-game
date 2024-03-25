using System;
using Core;
using Core.DISystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class PlayerInput : IPlayerInput
    {
        private Controls controls = null;
        public Vector2 MovementInputValue => moveInput;
        public Vector2 LookInputValue => mouseInput;
        
        public Action<bool> OnCrouchPerformedTriggered { get; set; }
        public Action<bool> OnSprintPerformedTriggered { get; set; }
        public Action<bool> OnJumpPerformedTriggered { get; set; }
        public Action<bool> OnCrouchCanceledTriggered { get; set; }
        public Action<bool> OnSprintCanceledTriggered { get; set; }
        public Action<bool> OnJumpCanceledTriggered { get; set; }
        public Action<bool> OnShootStartedTriggered { get; set; }
        public Action<bool> OnShootCanceledTriggered { get; set; }
        public Action OnAimPerformedTriggered { get; set; }
        public Action OnAimCanceledTriggered { get; set; }

        private Vector2 moveInput = Vector2.zero, mouseInput = Vector2.zero;

        public void Initialize()
        {
            controls = new Controls();

            controls.Gameplay.Movement.performed += OnMovementPerformed;
            controls.Gameplay.Movement.canceled += OnMovementCanceled;

            controls.Gameplay.Mouse.performed += OnLookPerformed;
            controls.Gameplay.Mouse.canceled += OnLookCanceled;

            controls.Gameplay.Jump.performed += OnJumpPerformed;
            controls.Gameplay.Jump.canceled += OnJumpCanceled;

            controls.Gameplay.Crouch.performed += OnCrouchPerformed;
            controls.Gameplay.Crouch.canceled += OnCrouchCanceled;

            controls.Gameplay.Sprint.performed += OnSprintPerformed;
            controls.Gameplay.Sprint.canceled += OnSprintCanceled;
            
            controls.Combat.Shoot.started += OnShootStarted;
            controls.Combat.Shoot.canceled += OnShootCanceled;
            
            controls.Combat.Aim.performed += OnAimPerformed;
            controls.Combat.Aim.canceled += OnAimCanceled;
            
            controls.Enable();
        }

        private void OnMovementPerformed(InputAction.CallbackContext obj)
        {
            moveInput = obj.ReadValue<Vector2>();
        }
        
        private void OnMovementCanceled(InputAction.CallbackContext obj)
        {
            moveInput = Vector2.zero;
        }

        private void OnLookPerformed(InputAction.CallbackContext obj)
        {
            mouseInput = controls.Gameplay.Mouse.ReadValue<Vector2>();
        }
        private void OnLookCanceled(InputAction.CallbackContext obj)
        {
            mouseInput = Vector2.zero;
        }
        private void OnJumpPerformed(InputAction.CallbackContext obj)
        {
            OnJumpPerformedTriggered?.Invoke(true);
        }
        private void OnJumpCanceled(InputAction.CallbackContext obj)
        {
            OnJumpCanceledTriggered?.Invoke(false);
        }
        private void OnCrouchPerformed(InputAction.CallbackContext obj)
        {
            OnCrouchPerformedTriggered?.Invoke(true);   
        }
        private void OnCrouchCanceled(InputAction.CallbackContext obj)
        {
            OnCrouchCanceledTriggered?.Invoke(false);
        }
        private void OnSprintPerformed(InputAction.CallbackContext obj)
        {
            OnSprintPerformedTriggered?.Invoke(true);
        }
        private void OnSprintCanceled(InputAction.CallbackContext obj)
        {
            OnSprintCanceledTriggered?.Invoke(false);
        }

        private void OnShootStarted(InputAction.CallbackContext obj)
        {
            OnShootStartedTriggered?.Invoke(true);
        }
        private void OnShootCanceled(InputAction.CallbackContext obj)
        {
            OnShootCanceledTriggered?.Invoke(false);
        }
        private void OnAimPerformed(InputAction.CallbackContext obj)
        {
            OnAimPerformedTriggered?.Invoke();
        }
        private void OnAimCanceled(InputAction.CallbackContext obj)
        {
            OnAimCanceledTriggered?.Invoke();
        }
        public void Deinitialize()
        {
            controls.Disable();
        }
        public void Tick()
        {
            // _movementInputValue = _playerActions.Gameplay.Control.ReadValue<Vector2>();
            // _lookInputValue = _playerActions.Gameplay.Look.ReadValue<Vector2>();
        }
    }
}