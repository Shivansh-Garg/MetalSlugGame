using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Player.States
{
    internal class DeadState:IPlayerState
    {
        private static DeadState _instance;
        public static DeadState Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DeadState();
                }
                return _instance;
            }
        }

        private DeadState() { }

        public void HandleInput(Player controller, AnimatorStateInfo prevState)
        {
            Debug.Log("previousstate "+ prevState.ToString());

        }

        public void UpdateState(Player controller)
        {

            // Set walking animation in the Animator
            controller.animator.SetBool("running", false);
            controller.animator.SetBool("grounded", true);
            controller.animator.SetBool("dead",true);
        }
    }
}
