using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.States
{
    internal class CrouchState : IPlayerState
    {
        private static CrouchState _instance;
        public static CrouchState Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CrouchState();
                }
                return _instance;
            }
        }

        private CrouchState() { }

        public void HandleInput(Player controller, AnimatorStateInfo prevState)
        {
            bool isPlayerDead = controller.checkIfPlayerDead();
            bool isAttacking = controller.checkIfAttacking();
            bool isGrounded = controller.CheckIfGrounded();
            bool isCrouching = controller.checkIfCrouching();

            if (isPlayerDead)
            {
                controller.ChangeState(DeadState.Instance);
            }

           

            // Handle transition from walk to idle

            if ((!isAttacking && !isPlayerDead) || !isCrouching)
            {
                controller.ChangeState(IdleState.Instance);
            }


        }

        public void UpdateState(Player controller)
        {

            // Set walking animation in the Animator
            controller.animator.SetBool("running", false);
            controller.animator.SetBool("grounded", true);
            controller.animator.SetTrigger("crouch");
        }
    }
}
