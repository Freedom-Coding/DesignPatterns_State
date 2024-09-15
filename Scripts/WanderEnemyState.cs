using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class WanderEnemyState : IEnemyState
    {
        private EnemyAI enemyAI;
        private float wanderTimeMin = 2;
        private float wanderTimeMax = 5;
        private float wanderDistance = 2;
        private float followRange;

        public WanderEnemyState(float _followRange)
        {
            followRange = _followRange;
        }

        public void EnterState(EnemyAI _enemyAI)
        {
            enemyAI = _enemyAI;
        }

        public void UpdateState()
        {
            float distToPlayer = enemyAI.GetDistanceToPlayer();

            if (distToPlayer > followRange)
            {
                Wander();
            }
            else
            {
                enemyAI.SetState(enemyAI.enemyFollowState);
            }
        }

        float wanderTimer = 0;
        Vector2 randomPoint;
        private void Wander()
        {
            wanderTimer -= Time.deltaTime;
            enemyAI.MoveTowards(randomPoint, 0.5f);
            enemyAI.LookAt(randomPoint, 0.5f);

            if (wanderTimer <= 0)
            {
                GetNewWanderPosition();
            }
        }

        private void GetNewWanderPosition()
        {
            wanderTimer = Random.Range(wanderTimeMin, wanderTimeMax);
            randomPoint = Random.insideUnitCircle * wanderDistance + (Vector2)enemyAI.transform.position;
        }
    }
}