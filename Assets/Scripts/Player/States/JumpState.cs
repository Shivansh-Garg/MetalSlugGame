using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.Scripts.Player.States
{
    internal class JumpState:IPlayerState
    {
        private static JumpState _instance;
        public static JumpState Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new JumpState();
                }
                return _instance;
            }
        }

        private JumpState() { }

        public void HandleInput(Player controller, AnimatorStateInfo prevState)
        {
            AnimatorStateInfo currentState = controller.animator.GetCurrentAnimatorStateInfo(0);


            bool isGrounded = controller.CheckIfGrounded();
            bool MeeleKeyPressed = controller.checkIfAttacking();
            bool throwKeyPressed = controller.checkIfKunaiAttacking();

            if (MeeleKeyPressed && !isGrounded)
            {
                controller.ChangeState(JumpAttackState.Instance);
            }
            else if(throwKeyPressed && !isGrounded && !MeeleKeyPressed)
            {
                controller.ChangeState(ThrowState.Instance);
            }
            else if(controller.CheckIfGrounded() == true && !MeeleKeyPressed &&!throwKeyPressed)
            {
                controller.ChangeState(IdleState.Instance);
            }

            //// Handle jumping
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    controller.ChangeState(new JumpState());
            //}
        }

        public void UpdateState(Player controller)
        {

            // Set walking animation in the Animator
            controller.animator.SetBool("running", false);
            controller.animator.SetBool("grounded", false);
        }
    }
}
