using CarrotPack;
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
        private GameObject player;

        public bool chasePlayer = false;

        public static float attackDelay = 4f;
        public float attackCountdown;

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
            // Update chase player
            attackCountdown -= Time.deltaTime;

            if(chasePlayer)
            {
                gameObject.GetComponent<NavMeshAgent>().destination = player.transform.position;
            }

            if(attackCountdown <= 0f)
            {
                Attack();
                attackCountdown = attackDelay;
            }
        }

        private void InitializeEnemy()
        {
            if (animator == null) animator = GetComponent<Animator>();

            enemyData = new EnemyData(scriptableObject.enemyData);
            attackCountdown = attackDelay;
        }

        public void Damage(int rawDamage)
        {
            enemyData.hp -= rawDamage;

            Debug.Log(gameObject.name + " " +  enemyData.hp);
            if (enemyData.hp <= 0)
            {
                Death();
            }
            else
            {
                animator.SetTrigger("Damage");
            }
        }

        public void Death()
        {
            Debug.Log("Death");
            animator.SetBool("Death", true);
            // Exp
        }

        public void Attack()
        {
            animator.SetTrigger("Attack");
        }
    }


}