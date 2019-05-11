using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarrotPack;

namespace AstralliaProject
{
    public class BGMSceneManager : MonoBehaviour
    {
        public string bgmName;

        public void Start()
        {
            Toolbox.Instance.GetManager<AudioManager>().BGMPlayer(bgmName);
        }

    }
}