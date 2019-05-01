using UnityEngine;
using System.Collections;


namespace AstralliaProject
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]

    public class PlayerController : MonoBehaviour
    {

        //public float animSpeed = 1.5f;
        //public float lookSmoother = 3.0f;
        //public bool useCurves = true;

        //public float useCurvesHeight = 0.5f;

        public float forwardSpeed = 7.0f;
        //public float backwardSpeed = 2.0f;
        //public float rotateSpeed = 2.0f;
        //public float jumpPower = 3.0f;

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

        private bool detectAttack = false;

        void Awake()
        {
            if(anim == null) anim = GetComponent<Animator>();
            if(col == null) col = GetComponent<CapsuleCollider>();
            if(rb == null) rb = GetComponent<Rigidbody>();

            cam = GameObject.FindWithTag("MainCamera");
            orgColHight = col.height;
            orgVectColCenter = col.center;

            swordScript = weapon.GetComponent<SwordScript>();
            swordScript.playerController = this;
        }

        void FixedUpdate()
        {
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

            //currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
            //rb.useGravity = true;


        }

        #region Animation Events
        public void KinematicsOff()
        {
            rb.isKinematic = false;
        }

        public void KinematicsOn()
        {
            rb.isKinematic = true;
        }

        // Event for impact
        public void BeginAttackEvent()
        {
            detectAttack = true;
            Debug.Log("Begin Attack");
        }

        public void EndAttackEvent()
        {
            detectAttack = false;
            Debug.Log("End Attack");
        }

        #endregion

        public void AttackCollision(Collider collider)
        {
            if (!detectAttack) return;

            if(collider.tag == "Enemy")
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                Debug.Log("Hit");
                detectAttack = false;

                // Apply damage
                // Damage animation

            }
        }
    }
}