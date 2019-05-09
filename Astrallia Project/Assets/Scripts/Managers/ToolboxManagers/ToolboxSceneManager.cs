using CarrotPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AstralliaProject
{
    public class ToolboxSceneManager : MonoBehaviour, IManager
    {
        private UIManager uIManager;

        private SceneEnum previousScene;

        public void InitializeManager()
        {
            previousScene = SceneEnum.StartScene;
        }

        private void Start()
        {
            uIManager = Toolbox.Instance.GetManager<UIManager>();
        }

        public void ChangeScene(SceneEnum targetScene)
        {
            Debug.Log("Change Scene: " + targetScene.ToString());
            previousScene = targetScene;
            uIManager.ScreenFadeOut(() =>
            {
                switch (targetScene)
                {
                    case SceneEnum.HomeScene:
                        SceneManager.LoadScene((int)targetScene);
                        break;
                }

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