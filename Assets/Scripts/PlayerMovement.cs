using System;
using Input;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // [SerializeField] private Camera _playerCamera;

    [SerializeField] private float movementSpeed = 1f;

    [SerializeField] private PlayerInput _playerInput;
    
    private Rigidbody _rigidbody;

    private Vector3 _moveDirection;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {

        var verticalInput = _playerInput.InputValue.y;
        var horizontalInput = _playerInput.InputValue.x;
        
        if (verticalInput == 0 && horizontalInput == 0)
        {
            return;
        }
        
        _moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        
        _rigidbody.AddForce(_moveDirection.normalized * movementSpeed, ForceMode.Force);
    }
}