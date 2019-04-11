﻿using UnityEngine;
using System.Collections;


namespace AstralliaProject
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]

    public class PlayerController : MonoBehaviour
    {

        public float animSpeed = 1.5f;
        public float lookSmoother = 3.0f;
        public bool useCurves = true;

        public float useCurvesHeight = 0.5f;

        public float forwardSpeed = 7.0f;
        public float backwardSpeed = 2.0f;
        //public float rotateSpeed = 2.0f;
        public float jumpPower = 3.0f;

        private CapsuleCollider col;
        private Rigidbody rb;

        private Vector3 velocity;
        private float orgColHight;

        private Vector3 orgVectColCenter;
        private Animator anim;
        private AnimatorStateInfo currentBaseState;

        private GameObject cam;
        [SerializeField] private GameObject weapon;

        static int idleState = Animator.StringToHash("Base Layer.Idle");
        static int locoState = Animator.StringToHash("Base Layer.Locomotion");
        static int jumpState = Animator.StringToHash("Base Layer.Jump");
        static int restState = Animator.StringToHash("Base Layer.Rest");

        void Start()
        {
            anim = GetComponent<Animator>();
            col = GetComponent<CapsuleCollider>();
            rb = GetComponent<Rigidbody>();
            cam = GameObject.FindWithTag("MainCamera");
            orgColHight = col.height;
            orgVectColCenter = col.center;
        }


        void FixedUpdate()
        {
            // Movement
            float verticalAxis = Input.GetAxis("Vertical");
            float horizontalAxis = Input.GetAxis("Horizontal");

            Vector3 moveDirection = new Vector3(horizontalAxis, 0, verticalAxis).normalized;
            Vector3 cameraDirection = cam.transform.position - transform.position;
            cameraDirection = (new Vector3(cameraDirection.x, 0, cameraDirection.z)).normalized;

            float angle = Vector3.SignedAngle(Vector3.back, cameraDirection, Vector3.up);

            // Apply camera angle to movement direction
            moveDirection = Quaternion.Euler(0, angle, 0) * moveDirection;

            float moveSpeed = Mathf.Sqrt(verticalAxis * verticalAxis + horizontalAxis * horizontalAxis);

            transform.LookAt(transform.position + moveDirection);
            rb.velocity = transform.forward * moveSpeed * forwardSpeed;




            anim.SetFloat("Speed", moveSpeed);
            //anim.SetFloat("Direction", horizontalAxis);
            anim.speed = animSpeed;
            currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
            rb.useGravity = true;

            if (Input.GetButtonDown("Jump"))
            {
                anim.SetBool("Alert", !weapon.activeSelf);
                weapon.SetActive(!weapon.activeSelf);

                //if (currentBaseState.nameHash == locoState)
                //{
                //    if (!anim.IsInTransition(0))
                //    {
                //        rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
                //        anim.SetBool("Jump", true);
                //    }
                //}
            }
            
            if (currentBaseState.nameHash == locoState)
            {
                if (useCurves)
                {
                    resetCollider();
                }
            }
            else if (currentBaseState.nameHash == jumpState)
            {
                cam.SendMessage("setCameraPositionJumpView");

                if (!anim.IsInTransition(0))
                {
                    if (useCurves)
                    {
                        float jumpHeight = anim.GetFloat("JumpHeight");
                        float gravityControl = anim.GetFloat("GravityControl");
                        if (gravityControl > 0)
                            rb.useGravity = false;

                        Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
                        RaycastHit hitInfo = new RaycastHit();
                        if (Physics.Raycast(ray, out hitInfo))
                        {
                            if (hitInfo.distance > useCurvesHeight)
                            {
                                col.height = orgColHight - jumpHeight;
                                float adjCenterY = orgVectColCenter.y + jumpHeight;
                                col.center = new Vector3(0, adjCenterY, 0);
                            }
                            else
                            {
                                resetCollider();
                            }
                        }
                    }
                    anim.SetBool("Jump", false);
                }
            }
            else if (currentBaseState.nameHash == idleState)
            {
                if (useCurves)
                {
                    resetCollider();
                }
                //if (Input.GetButtonDown("Jump"))
                //{
                //    anim.SetBool("Rest", true);
                //}
            }
            else if (currentBaseState.nameHash == restState)
            {
                if (!anim.IsInTransition(0))
                {
                    anim.SetBool("Rest", false);
                }
            }
        }

        void resetCollider()
        {
            col.height = orgColHight;
            col.center = orgVectColCenter;
        }
    }
}