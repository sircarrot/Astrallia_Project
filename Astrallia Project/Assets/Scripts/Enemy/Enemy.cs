using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AstralliaProject
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyDataScriptableObject scriptableObject;

        private Animator animator;
        private EnemyData enemyData;
        public GameObject player;

        // Use this for initialization
        void Start()
        {
            InitializeEnemy();
            //gameObject.GetComponent<NavMeshAgent>().Move(new De(50, 0, 50));
            //gameObject.GetComponent<NavMeshAgent>().SetDestination(new Vector3(50,0,50));

        }

        // Update is called once per frame
        void Update()
        {
            gameObject.GetComponent<NavMeshAgent>().destination = player.transform.position;
        }

        private void InitializeEnemy()
        {
            enemyData = scriptableObject.enemyData;
            if (animator == null) animator = GetComponent<Animator>();
        }

        public void Damage(int rawDamage)
        {
            enemyData.hp -= rawDamage;

            Debug.Log(enemyData.hp);
            if (enemyData.hp <= 0)
            {
                Death();
            }
        }

        public void Death()
        {
            // Death animation
            // Give player exp
        }

        public void Attack()
        {

        }
    }
}