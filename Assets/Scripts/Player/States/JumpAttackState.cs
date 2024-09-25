using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.States
{
    internal class JumpAttackState : IPlayerState
    {
        private static JumpAttackState _instance;
        public static JumpAttackState Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new JumpAttackState();
                }
                return _instance;
            }
        }

        private JumpAttackState() { }

        public void HandleInput(Player controller, AnimatorStateInfo prevState)
        {
            bool isAttacking = controller.checkIfAttacking();
            // Handle transition from walk to idle
            bool isGrounded = controller.CheckIfGrounded();

            if (!isAttacking && isGrounded)
            {
                controller.ChangeState(IdleState.Instance);
            }
            else if(!isAttacking )
            {
                controller.ChangeState(JumpState.Instance);
            }


        }

        public void UpdateState(Player controller)
        {

            // Set walking animation in the Animator
            controller.animator.SetBool("running", false);
            controller.animator.SetBool("grounded", false);
            controller.animator.SetTrigger("jumpAttacking");

        }
    }
}
