using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AstralliaProject
{
    public class PlayerData : MonoBehaviour
    {
        public int CurrentHP
        {
            get { return currentHp; }
        }

        public int MaxHP
        {
            get { return maxHp; }
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

        [SerializeField] private int currentHp;
        [SerializeField] private int maxHp;
        [SerializeField] private int defensePower;
        [SerializeField] private int attackPower;
        [SerializeField] private int exp;
        [SerializeField] private int level;

        public void Damage(int rawDamage)
        {
            currentHp -= (rawDamage - defensePower);
        }

        public void Attack(Enemy enemy)
        {
            enemy.Damage(attackPower);
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
            Heal(maxHp);
            attackPower++;
            defensePower++;
        }

        public void Heal(int amount)
        {
            currentHp += amount;
            if (currentHp > maxHp) currentHp = maxHp;
        }


    }
}