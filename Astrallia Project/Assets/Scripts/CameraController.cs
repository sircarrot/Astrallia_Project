using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AstralliaProject
{
    public class CameraController : MonoBehaviour
    {
        void Start()
        {
            

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float horizontalAxis = Input.GetAxis("Mouse X");
            float verticalAxis = Input.GetAxis("Mouse Y");


        }
    }
}