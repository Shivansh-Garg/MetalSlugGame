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
            bool isAttacking = controller.checkIfAttacking();
            // Handle transition from walk to idle
            bool isGrounded = controller.CheckIfGrounded();
            bool isThrowing = controller.checkIfKunaiAttacking();

            if (!isThrowing && isGrounded)
            {
                controller.ChangeState(IdleState.Instance);
            }
            else if (!isAttacking && !isGrounded)
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
