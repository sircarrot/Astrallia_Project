using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AstralliaProject
{
    public class CameraController : MonoBehaviour
    {
        public float horRotateSpeed = 2.0f;
        public float verRotateSpeed = 0.5f;

        public float verRotateMax = 45f;
        public float verRotateMin = -40f;

        [SerializeField] private Camera cam;
        [SerializeField] private GameObject player;

        void FixedUpdate()
        {
            transform.position = player.transform.position;

            // Camera movement using Mouse/Right Analog
            float horizontalAxis = Input.GetAxis("Mouse X");
            float verticalAxis = Input.GetAxis("Mouse Y");

            float horizontalRotation = horizontalAxis * horRotateSpeed;
            float verticalRotation = verticalAxis * verRotateSpeed * -1f; // Reverse Axis

            Vector3 currentRotation = transform.localRotation.eulerAngles;
            float checkRotation = currentRotation.x + verticalRotation;

            if (checkRotation > verRotateMax && checkRotation < (360 + verRotateMin))
            {
                verticalRotation = 0;
            }

            transform.localEulerAngles += new Vector3(verticalRotation, horizontalRotation, 0);
        }
    }
}