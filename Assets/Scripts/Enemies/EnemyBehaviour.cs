using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] private EnemySettings _enemySettings;
        
        private float _health;
        private float _speed;
        private float _damage;
        private float _attackRange;
        private void Start()
        {
            _health = _enemySettings.Health;
            _speed = _enemySettings.Speed;
            _damage = _enemySettings.Damage;
            _attackRange = _enemySettings.AttackRange;
        }

        public void DecreaseHealth(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}