using System;
using System.Collections;
using Input;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovementController : SerializedMonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float jumpForce = 1000f;
    [SerializeField] private float fallForce = 1000f;
    [SerializeField] private float maxAngle = 90f;
    [SerializeField] private Vector2 mouseSensitivity;
    [SerializeField] private Camera firstPersonCamera;
    [field: SerializeField] private IPlayerInput _playerInput;
    
    private Rigidbody _rigidbody;

    private Vector3 _moveDirection;

    private bool _isOnGround = false;
    private float xRotation = 0f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        _playerInput.OnJumpTriggered += Jump;

        StartCoroutine(DropPlayerOnGround());
        
        IEnumerator DropPlayerOnGround()
        { 
            _isOnGround = false;
        
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
        
            yield return new WaitUntil(() => _isOnGround);
            
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        UpdateMouseLook();
    }

    private void Move()
    {
        var verticalInput = _playerInput.MovementInputValue.y;
        var horizontalInput = _playerInput.MovementInputValue.x;
        
        if (verticalInput == 0 && horizontalInput == 0)
        {
            return;
        }
        
        _moveDirection = firstPersonCamera.transform.forward * verticalInput + firstPersonCamera.transform.right * horizontalInput;
        
        _rigidbody.MovePosition(new Vector3(_moveDirection.x, 0f, _moveDirection.z).normalized * movementSpeed * Time.fixedDeltaTime + _rigidbody.position);
        
    }
    
    private void UpdateMouseLook() 
    {
        var lookInput = _playerInput.LookInputValue * mouseSensitivity;
        
        var rot = firstPersonCamera.transform.localRotation.eulerAngles;
        float xTo = rot.y + lookInput.x;

        xRotation -= lookInput.y;
        xRotation = Mathf.Clamp(xRotation, -maxAngle, maxAngle);

        firstPersonCamera.transform.localRotation = Quaternion.Euler(xRotation, xTo, 0f);
    }
    
    private void Jump()
    {
        if (!_isOnGround)
        {
            return;
        }
        _isOnGround = false;
           
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);

        if (_rigidbody.velocity.x == 0f || _rigidbody.velocity.z == 0f)
        {
            float PredictMaxJumpHeight(float forceMagnitude, float objectMass)
            {
                // Calculate the initial velocity using the impulse-momentum theorem
                float initialVelocity = forceMagnitude / objectMass;

                // Calculate the maximum height using the formula
                float maxHeight = (initialVelocity * initialVelocity) / (2 * Physics.gravity.y) * (-1);

                return maxHeight;
            }
            
            var maxHeight = PredictMaxJumpHeight(jumpForce, _rigidbody.mass);

            StartCoroutine(FallOnGround(maxHeight));
            IEnumerator FallOnGround(float maxHeight)
            {
                float initialHeight = _rigidbody.position.y;
                float currentHeight = transform.position.y - initialHeight;

                while (currentHeight < maxHeight)
                {
                    currentHeight = transform.position.y - initialHeight;
                    _rigidbody.AddForce((fallForce / 8) * Vector2.down, ForceMode.Force);
                    yield return null;
                }
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            _isOnGround = true;
        }
    }

    private void OnDestroy()
    {
        _playerInput.OnJumpTriggered -= Jump;
    }
}