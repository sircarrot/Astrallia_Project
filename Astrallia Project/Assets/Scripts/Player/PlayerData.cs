using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AstralliaProject
{
    public class PlayerData : MonoBehaviour
    {
        public int HP
        {
            get { return hp; }
        }

        public int DefensePower
        {
            get
            {
                return defensePower;
            }
        }

        public int AttackPower
        {
            get
            {
                return attackPower;
            }
        }

        public int Exp
        {
            get
            {
                return exp;
            }
        }

        public int Level
        {
            get
            {
                return level;
            }
        }

        private int hp;
        private int defensePower;
        private int attackPower;
        private int exp;
        private int level;

        public void Damage(int rawDamage)
        {
            hp -= (rawDamage - defensePower);
        }

        public void Attack()
        {

        }

        public void GainEXP(int exp)
        {
            exp += exp;
            if (exp > 100)
            {
                LevelUp();
            }
        }

        public void LevelUp()
        {

        }


    }
}