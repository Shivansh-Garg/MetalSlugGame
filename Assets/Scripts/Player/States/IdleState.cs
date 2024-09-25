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
        public void HandleInput(Player controller, AnimatorStateInfo prevState)
        {
            bool isGrounded = controller.CheckIfGrounded();
            bool MeeleKeyPressed = controller.checkIfAttacking();
            bool throwKeyPressed = controller.checkIfKunaiAttacking();


            if (isGrounded == false)
            {
                controller.ChangeState(JumpState.Instance);
            }
            else if(Input.GetKeyDown(KeyCode.F) && isGrounded)
            {
                controller.ChangeState(AttackState.Instance);
            }
            else if(throwKeyPressed && !MeeleKeyPressed && isGrounded)
            {
                controller.ChangeState(ThrowState.Instance);
            }
            // Check if the player is moving and not jumping 
            else if (Input.GetAxis("Horizontal") != 0 && isGrounded && !(Input.GetKeyDown(KeyCode.F)) &&!throwKeyPressed)
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
