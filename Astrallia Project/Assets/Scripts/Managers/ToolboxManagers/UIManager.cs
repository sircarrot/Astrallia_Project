using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarrotPack;
using UnityEngine.UI;

namespace AstralliaProject
{
    public class UIManager : MonoBehaviour, IManager
    {
        [Header("Player UI")]
        public Slider hpSlider;
        public Text hpText;
        public Text levelText;

        [Header("Enemy HP Bar")]
        public GameObject enemyHpBar;
        public Slider enemyHPSlider;

        [Header("Transition Effects")]
        public Animator screenAnimator;
        private System.Action savedAction;

        private GameManager gameManager;

        public void InitializeManager()
        {
            gameManager = Toolbox.Instance.GetManager<GameManager>();
            gameManager.playerData.ChangeHPEvent += UpdateHPValue;
            gameManager.playerData.LevelUpEvent += UpdateLevelText;

            gameManager.playerData.InitializeUI();
        }

        #region Player HP
        public void UpdateLevelText(int level)
        {
            levelText.text = "LV: " + level;
        }

        public void UpdateHPValue(int currentHP, int maxHP)
        {
            hpSlider.value = (float)currentHP / (float)maxHP;
            hpText.text = currentHP + "/" + maxHP;
        }
        #endregion

        #region EnemyHP
        public void HideEnemyHPBar()
        {
            enemyHpBar.SetActive(false);
        }

        public void UpdateEnemyHP(int currentHP, int maxHP)
        {
            if (!enemyHpBar.activeSelf) enemyHpBar.SetActive(true);
            enemyHPSlider.value = (float)currentHP / (float)maxHP;
        }
        #endregion

        #region Screen Transition
        public void ScreenFadeOut(System.Action action = null)
        {
            savedAction = action;
            screenAnimator.SetTrigger("FadeOut");
        }

        public void ScreenFadeIn(System.Action action = null)
        {
            savedAction = action;
            screenAnimator.SetTrigger("FadeIn");
        }

        public void ActionCallback()
        {
            if(savedAction != null)
            {
                Debug.Log("Saved Action");
                savedAction.Invoke();
                savedAction = null;
            }
            else
            {
                Debug.Log("No Action");
            }
        }
        #endregion
    }
}