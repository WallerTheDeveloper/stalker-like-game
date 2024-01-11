using System;
using System.Collections;
using Input;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerMovementController : SerializedMonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;

    [field: SerializeField] private IPlayerInput _playerInput;
    
    private Rigidbody _rigidbody;

    private Vector3 _moveDirection;

    private bool _isOnGround = false;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        _playerInput.OnJumpTriggered += Jump;
    }
    
    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Debug.Log("Collided with ground");
            _isOnGround = true;
        }
    }

    private void Move()
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

    private void Jump()
    {
        _isOnGround = false;
        
        _rigidbody.useGravity = true;
        _rigidbody.AddForce(Vector3.up * 10f, ForceMode.Impulse);
        
        StartCoroutine(UseGravityWhileInAir());
        
        IEnumerator UseGravityWhileInAir()
        {
            yield return new WaitUntil(() => _isOnGround);
            _rigidbody.useGravity = false;
        }
    }

    private void OnDestroy()
    {
        _playerInput.OnJumpTriggered -= Jump;
    }
}