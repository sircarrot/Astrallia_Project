using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarrotPack;

namespace AstralliaProject
{
    public class PlayerPositionManager : MonoBehaviour
    {
        public Transform playerCameraSet;

        public SceneTransformPair[] sceneTransformPairs;

        public void Awake()
        {
            SceneEnum previousScene = Toolbox.Instance.GetManager<ToolboxSceneManager>().PreviousScene;

            Debug.Log("Set Player Position: " + previousScene.ToString());

            foreach (SceneTransformPair pair in sceneTransformPairs)
            {
                Debug.Log(pair.previousScene);
                if(pair.previousScene == previousScene)
                {
                    Debug.Log("Reposition");
                    playerCameraSet.localPosition = pair.transformPosition;
                    playerCameraSet.localEulerAngles = pair.transformRotation;
                    break;
                }
            }
        }
    }

    [System.Serializable]
    public class SceneTransformPair
    {
        public SceneEnum previousScene;
        public Vector3 transformPosition;
        public Vector3 transformRotation;
    }
        
}