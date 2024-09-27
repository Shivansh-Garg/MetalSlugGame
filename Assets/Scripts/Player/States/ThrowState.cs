using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.UI;
using UnityEngine;

namespace Assets.Scripts.Player.States
{
    internal class ThrowState : IPlayerState
    {
        private static ThrowState _instance;
        public static ThrowState Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ThrowState();
                }
                return _instance;
            }
        }

        private ThrowState() { }

        public void HandleInput(Player controller, AnimatorStateInfo prevState)
        {
            bool isPlayerDead = controller.checkIfPlayerDead();
            bool isTakingDamage = controller.checkIfTakingDamage();
            bool isAttacking = controller.checkIfAttacking();
            bool isGrounded = controller.CheckIfGrounded();
            bool isThrowing = controller.checkIfKunaiAttacking();

            if (isPlayerDead)
            {
                controller.ChangeState(DeadState.Instance);
            }
            else if (isTakingDamage && !isPlayerDead)
            {
                controller.ChangeState(TakingDamageState.Instance);
            }

            else if (!isThrowing && isGrounded && !isTakingDamage && !isPlayerDead )
            {
                controller.ChangeState(IdleState.Instance);
            }
            else if (!isAttacking && !isGrounded && !isTakingDamage && !isPlayerDead)
            {
                controller.ChangeState(JumpState.Instance);
            }


        }

        public void UpdateState(Player controller)
        {

            // Set walking animation in the Animator
            controller.animator.SetBool("running", false);
            //controller.animator.SetBool("grounded", true);
            controller.animator.SetTrigger("throwing");
        }
    }
}
