using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AstralliaProject
{
    public class SwordScript : MonoBehaviour
    {
        public PlayerController playerController;

        private void Start()
        {
            if (playerController == null) playerController = GetComponentInParent<PlayerController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log(other.name);
            playerController.AttackCollision(other);
        }
    }
}