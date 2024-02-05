using System;
using Core;
using Core.DISystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class PlayerInput : IPlayerInput
    {
        private PlayerInputActions _playerActions;
        
        public Vector2 MovementInputValue => _movementInputValue;
        public Vector2 LookInputValue => _lookInputValue;
        
        private Vector2 _movementInputValue = Vector2.zero;
        private Vector2 _lookInputValue;
        
        public Action OnJumpTriggered { get; set; }
        
        private bool _isJumping = false;
        public void Initialize()
        {
            _playerActions = new PlayerInputActions();

            _playerActions.Gameplay.Jump.performed += OnJump;
            
            _playerActions.Gameplay.Control.Enable();
            _playerActions.Gameplay.Jump.Enable();
            _playerActions.Gameplay.Look.Enable();

        }

        public void Deinitialize()
        {
            _playerActions.Gameplay.Jump.performed -= OnJump;     

            _playerActions.Gameplay.Control.Disable();
            _playerActions.Gameplay.Jump.Disable();
            _playerActions.Gameplay.Look.Disable();
        }
        
        public void Tick()
        {
            _movementInputValue = _playerActions.Gameplay.Control.ReadValue<Vector2>();
            _lookInputValue = _playerActions.Gameplay.Look.ReadValue<Vector2>();
        }
        
        private void OnJump(InputAction.CallbackContext obj)
        {
            OnJumpTriggered?.Invoke();
        }
    }
}