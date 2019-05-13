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
        public GameObject playerHpBar;
        public Slider hpSlider;
        public Text hpText;
        public Text levelText;

        [Header("Enemy HP Bar")]
        public GameObject enemyHpBar;
        public Slider enemyHPSlider;

        [Header("Transition Effects")]
        public Animator screenAnimator;
        private System.Action savedAction;

        [Header("Subtitles")]
        public Text subtitleText;
        private GameManager gameManager;

        [Header("Pause Game")]
        public GameObject pauseGame;

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

        public void StartGame()
        {
            playerHpBar.SetActive(true);
        }

        public void QuitGame()
        {
            playerHpBar.SetActive(false);
            enemyHpBar.SetActive(false);
        }

        public void ShowSubtitles(string subtitles)
        {
            screenAnimator.SetTrigger("ShowSubtitle");
            subtitleText.text = subtitles;
        }

        public void PauseGame()
        {
            pauseGame.SetActive(true);
        }

        public void ResumeGame()
        {
            pauseGame.SetActive(false);
        }
    }
}