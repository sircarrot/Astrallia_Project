using CarrotPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AstralliaProject
{
    public class StartGameManager : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private bool clickable = false;

        private void Awake()
        {
            if (animator == null) animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if(Input.GetButtonDown("Submit") && clickable)
            {
                animator.SetTrigger("StartGame");
            }
        }

        public void ClickableEvent()
        {
            clickable = true;
        }

        public void StartGameEvent()
        {
            Toolbox.Instance.GetManager<ToolboxSceneManager>().ChangeScene(SceneEnum.HomeScene, SceneEnum.StartScene);
        }
    }
}