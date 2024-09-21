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
        public void HandleInput(Player controller)
        {
            // Check if the player starts moving
            if (Input.GetAxis("Horizontal") != 0)
            {
                controller.ChangeState(new WalkState());
            }
        }

        public void UpdateState(Player controller)
        {
            // Set the idle animation in the Animator
            controller.animator.SetTrigger("Idle");
        }
    }
}
