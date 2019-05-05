using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarrotPack;
using UnityEngine.UI;

namespace AstralliaProject
{
    public class UIManager : MonoBehaviour, IManager
    {
        public Slider hpSlider;
        public Text hpText;

        public Text levelText;

        private GameManager gameManager;

        public void InitializeManager()
        {
            gameManager = Toolbox.Instance.GetManager<GameManager>();
            gameManager.playerData.ChangeHPEvent += UpdateHPValue;
            gameManager.playerData.LevelUpEvent += UpdateLevelText;

            gameManager.playerData.InitializeUI();
        }

        public void UpdateLevelText(int level)
        {
            levelText.text = "LV. " + level;
        }

        public void UpdateHPValue(int currentHP, int maxHP)
        {
            hpSlider.value = (float) currentHP / (float) maxHP;
            hpText.text = currentHP + "/" + maxHP;
        }

    }
}