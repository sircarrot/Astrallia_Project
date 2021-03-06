﻿using CarrotPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AstralliaProject
{
    public class ToolboxSceneManager : MonoBehaviour, IManager
    {
        private UIManager uIManager;

        public SceneEnum PreviousScene
        {
            get { return previousScene; }
        }
        private SceneEnum previousScene;

        public SceneEnum CurrentScene
        {
            get { return currentScene; }
        }
        private SceneEnum currentScene;

        public void InitializeManager()
        {
            previousScene = SceneEnum.StartScene;
            currentScene = SceneEnum.StartScene;
        }

        private void Start()
        {
            uIManager = Toolbox.Instance.GetManager<UIManager>();
        }

        public void ChangeScene(SceneEnum targetScene, SceneEnum fromScene, System.Action action = null)
        {
            Debug.Log("Change Scene: " + targetScene.ToString());
            previousScene = fromScene;
            uIManager.ScreenFadeOut(() =>
            {
                switch (targetScene)
                {
                    case SceneEnum.StartScene:
                        SceneManager.LoadScene((int)targetScene);
                        uIManager.QuitGame();
                        break;

                    default:
                        SceneManager.LoadScene((int)targetScene);
                        uIManager.StartGame();
                        break;
                }

                currentScene = targetScene;

                if (action != null) { action.Invoke(); }

                uIManager.ScreenFadeIn();
            });
        }
    }

    public enum SceneEnum
    {
        StartScene,
        HomeScene,
        Map1Scene,
        Map2Scene
    }


}