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

        private GameManager gameManager;

        public bool chasePlayer = false;

        public static float attackDelay = 4f;
        public float attackCountdown;
        private bool detectAttack = false;

        // Use this for initialization
        void Start()
        {
            InitializeEnemy();
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
            gameManager = Toolbox.Instance.GetManager<GameManager>();
            player = gameManager.PlayerController;

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
        #endregion

        public void AttackCollision(Collider collider)
        {
            if (!detectAttack) return;

            if (collider.tag == "Player")
            {
                player.Damage(enemyData.attack);
                Debug.Log("Player Hit");

            }
        }
    }


}