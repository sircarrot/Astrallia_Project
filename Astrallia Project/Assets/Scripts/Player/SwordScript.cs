using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AstralliaProject
{
    public class SwordScript : MonoBehaviour
    {
        public PlayerController playerController;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.name);
        }
    }
}