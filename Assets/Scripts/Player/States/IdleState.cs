using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.States
{
    internal class IdleState:IPlayerState
    {
        private static IdleState _instance;
        public static IdleState Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new IdleState();
                }
                return _instance;
            }
        }

        private IdleState() { }
        public void HandleInput(Player controller)
        {
            bool isGrounded = controller.CheckIfGrounded();

            if(isGrounded == false)
            {
                controller.ChangeState(JumpState.Instance);
            }
            // Check if the player is moving and not jumping 
            else if (Input.GetAxis("Horizontal") != 0 && isGrounded)
            {
                controller.ChangeState(WalkState.Instance);
            }

        }

        public void UpdateState(Player controller)
        {
            // Set the idle animation in the Animator
            controller.animator.SetBool("running",false);
            controller.animator.SetBool("grounded", true);

        }
    }
}
