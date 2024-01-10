using System;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class PlayerInput : MonoBehaviour, ISystem
    {
        private PlayerInputActions _playerActions;

        public Vector2 InputValue => _inputValue;
        private Vector2 _inputValue;
        public void Register()
        { 
            _playerActions = new PlayerInputActions();
            
            _playerActions.Gameplay.Control.Enable();
        }

        public void Deregister()
        {
            _playerActions.Gameplay.Control.Disable();
        }
        
        private void FixedUpdate()
        {
            _inputValue = _playerActions.Gameplay.Control.ReadValue<Vector2>();
            Debug.Log(_inputValue);
        }
    }
}