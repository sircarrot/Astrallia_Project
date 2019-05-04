using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AstralliaProject
{
    public class EnemyHitBox : MonoBehaviour
    {
        public Enemy enemy;

        private void Start()
        {
            if (enemy == null) enemy = GetComponentInParent<Enemy>();
        }

        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log(other.name);
            enemy.AttackCollision(other);
        }

    }
}