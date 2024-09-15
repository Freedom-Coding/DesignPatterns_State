using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class FollowEnemyState : IEnemyState
    {
        private float followRange;
        private float attackRange;
        private Transform playerTransform;
        private EnemyAI enemyAI;

        public FollowEnemyState(float _followRange, float _attackRange, Transform _playerTransform)
        {
            followRange = _followRange;
            attackRange = _attackRange;
            playerTransform = _playerTransform;
        }

        public void EnterState(EnemyAI _enemyAI)
        {
            enemyAI = _enemyAI;
        }

        public void UpdateState()
        {
            float distToPlayer = enemyAI.GetDistanceToPlayer();

            if (distToPlayer < followRange && distToPlayer > attackRange)
            {
                Follow();
            }
            else if (distToPlayer > followRange)
            {
                enemyAI.SetState(enemyAI.enemyWanderState);
            }
            else
            {
                enemyAI.SetState(enemyAI.enemyAttackState);
            }
        }

        private void Follow()
        {
            enemyAI.MoveTowards(playerTransform.position);
            enemyAI.LookAt(playerTransform.position);
        }
    }
}