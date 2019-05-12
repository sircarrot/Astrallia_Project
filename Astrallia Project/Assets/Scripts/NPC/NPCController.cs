using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarrotPack;

namespace AstralliaProject
{
    public class NPCController : MonoBehaviour
    {
        private PlayerController player;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                player = other.GetComponent<PlayerController>();
                player.targetNpc = this;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerController>() == player)
            {
                if(player.targetNpc == this)
                {
                    player.targetNpc = null;
                    player = null;
                }
            }
        }

        public void Interact()
        {
            if(player != null)
            {
                player.Heal(0, true);
                Toolbox.Instance.GetManager<UIManager>().ShowSubtitles("I'll heal you!");
            }
        }
    }
}