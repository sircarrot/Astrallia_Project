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
        private PlayerController player;
        private NavMeshAgent navMeshAgent;

        private GameManager gameManager;

        private static float aggroDistance = 6f;
        private bool chasePlayer = false;

        public static float attackDelay = 4f;
        public float attackCountdown;
        private bool detectAttack = false;

        private AnimatorStateInfo currentBaseState;
        static int deathState = Animator.StringToHash("Base Layer.Death");
        static int attackingState = Animator.StringToHash("Base Layer.Death");

        // Use this for initialization
        void Start()
        {
            InitializeEnemy();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            currentBaseState = animator.GetCurrentAnimatorStateInfo(0);
            if (currentBaseState.fullPathHash == deathState
                || currentBaseState.fullPathHash == attackingState)
            {
                return;
            }

            float velocity = navMeshAgent.velocity.magnitude;
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

            // Setting States
            //Debug.Log("Distance: " + (player.transform.position - gameObject.transform.position).magnitude);
            if((player.transform.position - gameObject.transform.position).magnitude <= aggroDistance)
            {
                chasePlayer = true;
            }
            else
            {
                chasePlayer = false;
            }

            // Wandering Around State
            


            // Chase Player State
            if(chasePlayer)
            {
                navMeshAgent.destination = player.transform.position;
                Vector3 lookAtVector = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                transform.LookAt(lookAtVector);

                if (velocity > 0.5) return;

                attackCountdown -= Time.deltaTime;

                if (attackCountdown <= 0f)
                {
                    Attack();
                    attackCountdown = attackDelay;
                }
            }
        }

        private void InitializeEnemy()
        {
            if (animator == null) animator = GetComponent<Animator>();
            if (navMeshAgent == null) navMeshAgent = GetComponent<NavMeshAgent>();

            enemyData = new EnemyData(scriptableObject.enemyData);
            gameManager = Toolbox.Instance.GetManager<GameManager>();
            player = gameManager.PlayerController;

            attackCountdown = attackDelay;
        }

        #region Basic Gameplay Functions
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
        #endregion

        #region Animation Event
        public void BeginAttackEvent()
        {
            detectAttack = true;
        }

        public void EndAttackEvent()
        {
            detectAttack = false;
            animator.ResetTrigger("Damage");
        }


        public void AttackCollision(Collider collider)
        {
            if (!detectAttack) return;

            if (collider.tag == "Player")
            {
                detectAttack = false;
                player.Damage(enemyData.attack);
                Debug.Log("Player Hit");
            }
        }
        #endregion
    }


}