using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "Enemy/EnemySettings")]
    public class EnemySettings : ScriptableObject
    {
        public float Health;
        public float Speed;
        public float Damage;
        public float AttackRange;
    }
}