using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace StatePattern
{
    public class AttackEnemyState : IEnemyState
    {
        private EnemyAI enemyAI;
        private float attackCooldown;
        private float attackRange;
        private GameObject bulletPrefab;
        private Transform weaponTransform;
        private Transform playerTransform;

        public AttackEnemyState(float _attackCooldown, float _attackRange, GameObject _bulletPrefab, Transform _weaponTransform, Transform _playerTransform)
        {
            attackCooldown = _attackCooldown;
            attackRange = _attackRange;
            bulletPrefab = _bulletPrefab;
            weaponTransform = _weaponTransform;
            playerTransform = _playerTransform;
        }

        public void EnterState(EnemyAI _enemyAI)
        {
            enemyAI = _enemyAI;
        }

        public void UpdateState()
        {
            float distToPlayer = enemyAI.GetDistanceToPlayer();

            if (distToPlayer < attackRange)
            {
                Attack();
            }
            else
            {
                enemyAI.SetState(enemyAI.enemyFollowState);
            }
        }

        float attackTimer = 0;
        private void Attack()
        {
            enemyAI.LookAt(playerTransform.position, 0.35f);
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                attackTimer = attackCooldown;
                GameObject bullet = GameObject.Instantiate(bulletPrefab, weaponTransform.position, weaponTransform.rotation);
                bullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 6), ForceMode2D.Impulse);
                GameObject.Destroy(bullet, 5);
            }
        }
    }
}