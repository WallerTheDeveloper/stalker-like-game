using System;
using System.Collections;
using Control;
using Core.DISystem;
using Input;
using UnityEngine;
using Random = System.Random;

namespace Shooting
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private GameObject _shootingPoint;
        [SerializeField] Camera _firstPersonCamera;

        [Range(0f, 5f)]
        [SerializeField] private float _dispersion;
        [SerializeField] private float _bulletVelocity = 70f;
        
        private GameObject _projectile;
        
        private float _initialVelocity;
        
        private void Start()
        {
            IPlayerInput playerInput =
                gameObject.GetComponentInParent<PlayerMovementController>()._playerInput;
            
            playerInput.OnShootPerformedTriggered += OnShootPerformed;
        }

        private void OnShootPerformed()
        {
            _projectile = Instantiate(_bulletPrefab, _shootingPoint.transform.position, _shootingPoint.transform.rotation);
            Rigidbody projectileRigidbody = _projectile.GetComponent<Rigidbody>();
            
            // generate random values for dispersion
            Random random = new Random();
            float randomX = (float) random.NextDouble() * _dispersion;
            float randomY = (float) random.NextDouble() * _dispersion;
            
            projectileRigidbody.AddForce(new Vector3(randomX, randomY, 0) + _firstPersonCamera.transform.forward * _bulletVelocity, ForceMode.Impulse);
        }
    }
}