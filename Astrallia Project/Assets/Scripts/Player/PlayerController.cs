﻿using UnityEngine;
using System.Collections;
using CarrotPack;

namespace AstralliaProject
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float forwardSpeed = 7.0f;

        public bool canMove = true;

        private CapsuleCollider col;
        private Rigidbody rb;

        private Vector3 velocity;
        private float orgColHight;

        private Vector3 orgVectColCenter;
        private Animator anim;
        private AnimatorStateInfo currentBaseState;

        private GameObject cam;
        [SerializeField] private GameObject weapon;
        private SwordScript swordScript;
        private PlayerData playerData;
        private GameManager gameManager;

        private bool detectAttack = false;

        public NPCController targetNpc = null;

        static int damageState = Animator.StringToHash("Base Layer.Damage");
        static int deathState = Animator.StringToHash("Base Layer.Death");

        void Start()
        {
            if(anim == null) anim = GetComponent<Animator>();
            if(col == null) col = GetComponent<CapsuleCollider>();
            if(rb == null) rb = GetComponent<Rigidbody>();

            cam = GameObject.FindWithTag("MainCamera");
            orgColHight = col.height;
            orgVectColCenter = col.center;

            swordScript = weapon.GetComponent<SwordScript>();
            swordScript.playerController = this;

            gameManager = Toolbox.Instance.GetManager<GameManager>();
            playerData = gameManager.playerData;
        }

        void FixedUpdate()
        {
            if (!canMove) return;

            if(Input.GetButtonDown("Interact") && targetNpc != null)
            {
                targetNpc.Interact();
                return;
            }

            // Cannot move when damaged/dead
            currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
            if(currentBaseState.fullPathHash == damageState 
                || currentBaseState.fullPathHash == deathState)
            {
                return;
            }

            // Look At Direction
            float verticalAxis = Input.GetAxis("Vertical");
            float horizontalAxis = Input.GetAxis("Horizontal");

            Vector3 moveDirection = new Vector3(horizontalAxis, 0, verticalAxis).normalized;
            Vector3 cameraDirection = cam.transform.position - transform.position;
            cameraDirection = (new Vector3(cameraDirection.x, 0, cameraDirection.z)).normalized;

            float angle = Vector3.SignedAngle(Vector3.back, cameraDirection, Vector3.up);

            // Apply camera angle to movement direction
            moveDirection = Quaternion.Euler(0, angle, 0) * moveDirection;
            transform.LookAt(transform.position + moveDirection);

            if (Input.GetButtonDown("Jump"))
            {
                anim.SetBool("Alert", !weapon.activeSelf);
                weapon.SetActive(!weapon.activeSelf);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                if (weapon.activeSelf)
                {
                    anim.SetTrigger("Attack");
                }
            }

            if (rb.isKinematic) return;

            // Movement
            float moveSpeed = Mathf.Sqrt(verticalAxis * verticalAxis + horizontalAxis * horizontalAxis);
            rb.velocity = transform.forward * moveSpeed * forwardSpeed;
            anim.SetFloat("Speed", moveSpeed);
        }

        #region Animation Events
        public void KinematicsOff()
        {
            rb.isKinematic = false;
            detectAttack = false;

            anim.ResetTrigger("Attack");
        }

        public void KinematicsOn()
        {
            rb.isKinematic = true;
        }

        // Event for attack impact
        public void BeginAttackEvent()
        {
            detectAttack = true;
        }

        public void EndAttackEvent()
        {
            detectAttack = false;
        }

        public void AttackCollision(Collider collider)
        {
            if (!detectAttack) return;

            if(collider.tag == "Enemy")
            {
                Debug.Log(collider.name + " Hit");
                Enemy enemy = collider.GetComponent<Enemy>();

                Attack(enemy);
                detectAttack = false;
            }
        }

        // Restart game with full hp
        public void DeathEvent()
        {
            Toolbox.Instance.GetManager<ToolboxSceneManager>().ChangeScene(SceneEnum.StartScene, SceneEnum.StartScene, () =>
            {
                Heal(0, true);

            });
        }
        #endregion

        #region Data Usage Functions
        public void Attack(Enemy enemy)
        {
            enemy.Damage(playerData.attackPower);
        }

        public void Damage(int rawDamage)
        {
            anim.SetTrigger("Damage");

            if(playerData.Damage(rawDamage))
            {
                anim.SetBool("Death", true);
            }
        }

        public void Heal(int amount, bool healMax = false)
        {
            playerData.Heal(amount, healMax);
        }
        #endregion
    }
}