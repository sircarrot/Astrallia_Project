using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarrotPack;

namespace AstralliaProject
{
    public class ChangeSceneCollider : MonoBehaviour
    {
        // Default set to home scene
        [SerializeField] public SceneEnum nextScene = SceneEnum.HomeScene;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("OnTrigger: " +  other.tag);
            if (other.tag == "Player")
            {
                Toolbox.Instance.GetComponent<ToolboxSceneManager>().ChangeScene(nextScene);
            }
        }
    }
}