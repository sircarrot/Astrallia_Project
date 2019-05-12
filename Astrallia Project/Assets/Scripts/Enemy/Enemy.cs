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

        public bool hostile = true;

        public float aggroDistance = 6f;
        private bool chasePlayer = false;

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
            if((player.transform.position - gameObject.transform.position).magnitude <= aggroDistance && hostile)
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
                    Debug.Log(enemyData.attackDelay);
                    attackCountdown = enemyData.attackDelay;
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

            attackCountdown = enemyData.attackDelay;
        }

        #region Basic Gameplay Functions
        public void Damage(int rawDamage)
        {
            enemyData.currentHp -= rawDamage;

            gameManager.HitEnemy(enemyData.currentHp, enemyData.maxHp);

            Debug.Log(gameObject.name + " " +  enemyData.currentHp);
            if (enemyData.currentHp <= 0)
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

            gameManager.KillEnemy(enemyData.expDrop);

            StartCoroutine(DeathCoroutine());
            //Destroy(this);
        }

        private IEnumerator DeathCoroutine()
        {
            float coroutineDuration = 5f;
            while (navMeshAgent.baseOffset > 0f)
            {
                navMeshAgent.baseOffset -= (0.5f * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            while(coroutineDuration > 0f)
            {
                coroutineDuration -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            Destroy(this.gameObject);
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

        void OnGUI()
        {

            Vector2 targetPos;
            targetPos = Camera.main.WorldToScreenPoint(transform.position);

            GUI.Box(new Rect(targetPos.x, Screen.height - transform.position.y, 60, 20), enemyData.currentHp + "/" + enemyData.maxHp);

        }

    }


}