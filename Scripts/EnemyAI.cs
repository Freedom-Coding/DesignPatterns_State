using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class EnemyAI : MonoBehaviour
    {
        [HideInInspector] public WanderEnemyState enemyWanderState;
        [HideInInspector] public AttackEnemyState enemyAttackState;
        [HideInInspector] public FollowEnemyState enemyFollowState;

        [SerializeField] private float moveSpeed = 0.6f;
        [SerializeField] private float rotationSpeed = 50;
        [SerializeField] private float followRange = 7;
        [SerializeField] private float attackRange = 4;
        [SerializeField] private float attackCooldown = 2;
        [SerializeField] private Transform weaponTransform;
        [SerializeField] private GameObject bulletPrefab;

        private Transform playerTransform;
        private IEnemyState currentState;

        private void InitializeStates()
        {
            playerTransform = FindObjectOfType<ExampleShipControl>().transform;

            enemyWanderState = new WanderEnemyState(followRange);
            enemyAttackState = new AttackEnemyState(attackCooldown, attackRange, bulletPrefab, weaponTransform, playerTransform);
            enemyFollowState = new FollowEnemyState(followRange, attackRange, playerTransform);

            SetState(enemyWanderState);
        }

        private void Start()
        {
            InitializeStates();
        }

        private void Update()
        {
            currentState.UpdateState();
        }

        public void SetState(IEnemyState iEnemyState)
        {
            currentState = iEnemyState;
            iEnemyState.EnterState(this);
        }

        public float GetDistanceToPlayer()
        {
            return Vector2.Distance(transform.position, playerTransform.position);
        }

        public void MoveTowards(Vector2 destination, float speedMultiplier = 1)
        {
            Vector3 direction = (Vector3)destination - transform.position;
            transform.position += direction.normalized * moveSpeed * Time.deltaTime * speedMultiplier;
        }

        public void LookAt(Vector2 destination, float speedMultiplier = 1)
        {
            Vector3 direction = (Vector3)destination - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime * speedMultiplier);
        }
    }
}