using Control;
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
        [SerializeField] private float _roundsPerMinute = 6000f;
        [SerializeField] private int _magazineSize = 30;
        
        private GameObject _projectile;
        
        private float _initialVelocity;
        private float _timeSinceLastShot = 0f;
        private bool _isShooting;
        private float _fireDelay;
        private float _nextShoot;

        private void Start()
        {
            IPlayerInput playerInput =
                gameObject.GetComponentInParent<PlayerMovementController>()._playerInput;
            
            playerInput.OnShootStartedTriggered += OnShootStateHandle;
            playerInput.OnShootCanceledTriggered += OnShootStateHandle;
            
            
            _fireDelay = 60.0f / _roundsPerMinute;
        }
        
        private void OnShootStateHandle(bool isShooting)
        {
            _isShooting = isShooting;
        }

        private void PerformShooting()
        {
            // generate random values for dispersion
            Random random = new Random();
            float randomX = (float) random.NextDouble() * _dispersion;
            float randomY = (float) random.NextDouble() * _dispersion;
            
            _projectile = Instantiate(_bulletPrefab, _shootingPoint.transform.position, _shootingPoint.transform.rotation);
            Rigidbody projectileRigidbody = _projectile.GetComponent<Rigidbody>();

            projectileRigidbody.AddForce(new Vector3(randomX, randomY, 0) + _firstPersonCamera.transform.forward * _bulletVelocity, ForceMode.Impulse);
            _magazineSize--;
        }
        private void Update()
        {
            if (_magazineSize == 0)
            {
                Debug.Log("Out of ammo");
                return;
            }
            
            if (_isShooting && Time.time > _nextShoot)
            {
                PerformShooting();
                _nextShoot = Time.time + _fireDelay;
            }
        }
    }
}