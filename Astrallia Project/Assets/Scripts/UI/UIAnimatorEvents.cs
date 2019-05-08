using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarrotPack;

namespace AstralliaProject
{
    public class UIAnimatorEvents : MonoBehaviour
    {
        public void ActionCallback()
        {
            Toolbox.Instance.GetManager<UIManager>().ActionCallback();
        }
    }
}