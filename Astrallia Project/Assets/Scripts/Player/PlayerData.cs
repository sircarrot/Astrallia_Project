using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AstralliaProject
{
    public class PlayerData : MonoBehaviour
    {
        public int currentHp;
        public int maxHp;
        public int attackPower;
        public int defensePower;
        public int exp;
        public int level;

        public void Damage(int rawDamage)
        {
            // Min damage of 1
            currentHp -= Mathf.Max(rawDamage - defensePower, 1);
        }

        public void GainExp(int exp)
        {
            exp += exp;
            if (exp > (100 + 10*(level-1)))
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