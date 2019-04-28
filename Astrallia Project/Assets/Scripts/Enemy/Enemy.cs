using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AstralliaProject
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyDataScriptableObject scriptableObject;

        private EnemyData enemyData;

        // Use this for initialization
        void Start()
        {
            InitializeEnemy();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void InitializeEnemy()
        {
            enemyData = scriptableObject.enemyData;
        }

    }
}