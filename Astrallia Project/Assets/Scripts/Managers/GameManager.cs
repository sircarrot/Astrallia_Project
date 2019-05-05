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


        public void InitializeManager()
        {
            uiManager = Toolbox.Instance.GetManager<UIManager>();
        }

        #region PlayerFunctions
        public void KillEnemy(int exp)
        {
            playerData.GainExp(exp);
        }
        #endregion
    }
}