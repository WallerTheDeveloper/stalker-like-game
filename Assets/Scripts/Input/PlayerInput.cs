using System;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class PlayerInput : MonoBehaviour, ISystem, IPlayerInput
    {
        private PlayerInputActions _playerActions;
        
        public Vector2 InputValue => _inputValue;
        private Vector2 _inputValue;
        
        public Action OnJumpTriggered { get; set; }
    
        public void Register()
        { 
            _playerActions = new PlayerInputActions();

            _playerActions.Gameplay.Jump.performed += OnJump;

            _playerActions.Gameplay.Control.Enable();
            _playerActions.Gameplay.Jump.Enable();
        }

        public void Deregister()
        {
            _playerActions.Gameplay.Jump.performed -= OnJump;

            _playerActions.Gameplay.Control.Disable();
            _playerActions.Gameplay.Jump.Disable();
        }
        
        private void Update()
        {
            _inputValue = _playerActions.Gameplay.Control.ReadValue<Vector2>();
        }
        
        private void OnJump(InputAction.CallbackContext obj)
        {
            OnJumpTriggered?.Invoke();
        }
    }
}