using CarrotPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AstralliaProject
{
    public class GameManager : MonoBehaviour, IManager
    {
        public PlayerController PlayerController
        {
            get
            {
                if(playerController == null)
                {
                    playerController = FindObjectOfType<PlayerController>();
                }

                return playerController;
            }
        }

        [SerializeField] private PlayerController playerController;
        public PlayerData playerData;

        public UIManager uiManager;

        private bool gamePaused = false;
        private ToolboxSceneManager toolboxSceneManager;

        public void InitializeManager()
        {
            uiManager = Toolbox.Instance.GetManager<UIManager>();
            toolboxSceneManager = Toolbox.Instance.GetManager<ToolboxSceneManager>();
        }

        #region PlayerFunctions
        public void KillEnemy(int exp)
        {
            playerData.GainExp(exp);
        }

        public void HitEnemy(int currentHp, int maxHp)
        {
            uiManager.UpdateEnemyHP(currentHp, maxHp);
        }
        #endregion

        public void PauseGame()
        {
            if(toolboxSceneManager.CurrentScene != SceneEnum.StartScene)
            {
                Time.timeScale = 0;
                gamePaused = true;
                uiManager.PauseGame();
            }
        }

        public void ResumeGame()
        {
            if (toolboxSceneManager.CurrentScene != SceneEnum.StartScene)
            {
                Time.timeScale = 1;
                gamePaused = false;
                uiManager.ResumeGame();
            }
        }

        private void Update()
        {
            if (Input.GetButtonDown("Pause"))
            {
                if (gamePaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }
}