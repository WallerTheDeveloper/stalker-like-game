using System;
using Enemies;
using UnityEngine;

namespace Shooting
{
    public class BulletBehaviour : MonoBehaviour
    {
        private void Start()
        {
            // Bullet will be destroyed after 5 seconds if it doesn't hit anything
            Destroy(gameObject, 3f);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out EnemyBehaviour enemyBehaviour))
            {
                print("Enemy hit " + enemyBehaviour.gameObject.name);
                enemyBehaviour.DecreaseHealth(10);
                Destroy(gameObject);
            }
        }
    }
}