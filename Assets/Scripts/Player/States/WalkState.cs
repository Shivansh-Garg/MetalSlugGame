using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.States
{
    internal class WalkState:IPlayerState
    {
        public void HandleInput(Player controller)
        {
            // Handle transition from walk to idle
            if (Input.GetAxis("Horizontal") == 0)
            {
                controller.ChangeState(new IdleState());
            }

            //// Handle jumping
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    controller.ChangeState(new JumpState());
            //}
        }

        public void UpdateState(Player controller)
        {
            // Update movement logic
            float moveX = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(moveX, 0, 0) * controller.playerSpeed * Time.deltaTime;
            controller.transform.position += movement;

            // Set walking animation in the Animator
            controller.animator.SetTrigger("Walk");
        }
    }
}
