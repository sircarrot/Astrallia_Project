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

        public delegate void OnChangeHP(int currentHP, int maxHP);
        public event OnChangeHP ChangeHPEvent;

        public delegate void OnLevelUp(int level);
        public event OnLevelUp LevelUpEvent;

        public void InitializeUI()
        {
            ChangeHPEvent(currentHp, maxHp);
            LevelUpEvent(level);
        }

        public bool Damage(int rawDamage)
        {
            // Min damage of 1
            currentHp -= Mathf.Max(rawDamage - defensePower, 1);

            if (currentHp <= 0f)
            {
                currentHp = 0;
                ChangeHPEvent(currentHp, maxHp);
                return true;
            }

            ChangeHPEvent(currentHp, maxHp);
            return false;
        }

        public void GainExp(int expGain)
        {
            exp += expGain;
            if (exp > (100 + 10*(level-1)))
            {
                exp -= (100 + 10 * (level - 1));
                LevelUp();
            }
        }

        public void LevelUp()
        {
            maxHp += 5;
            attackPower += 2;
            Heal(maxHp);
            // Don't raise defense power
            level++;
            
            // Call event
            LevelUpEvent(level);
            ChangeHPEvent(currentHp, maxHp);
        }

        public void Heal(int amount, bool healMax = false)
        {
            if(healMax)
            {
                amount = maxHp;
            }

            currentHp += amount;
            if (currentHp > maxHp) currentHp = maxHp;

            ChangeHPEvent(currentHp, maxHp);
        }
    }
}