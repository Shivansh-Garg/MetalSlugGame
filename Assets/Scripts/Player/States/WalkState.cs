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
        private static WalkState _instance;
        public static WalkState Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WalkState();
                }
                return _instance;
            }
        }

        private WalkState() { }

        public void HandleInput(Player controller,AnimatorStateInfo prevState)
        {
            // Handle transition from walk to idle
            bool isGrounded = controller.CheckIfGrounded();
            bool MeeleKeyPressed = controller.checkIfAttacking();
            bool throwKeyPressed = controller.checkIfKunaiAttacking();
            bool isPlayerDead = controller.checkIfPlayerDead();
            bool isTakingDamage = controller.checkIfTakingDamage();

            if (isPlayerDead)
            {
                controller.ChangeState(DeadState.Instance);
            }
            else if(isTakingDamage && !isPlayerDead && !isPlayerDead)
            {
                controller.ChangeState(TakingDamageState.Instance);
            }
            //******this is not required the control is automatically  coming back to idle state
            if (isGrounded == false && !isTakingDamage && !isTakingDamage && !isPlayerDead)
            {
                controller.ChangeState(JumpState.Instance);
            }
            else if (MeeleKeyPressed && isGrounded && !isTakingDamage && !isTakingDamage && !isPlayerDead)
            {
                controller.ChangeState(AttackState.Instance);
            }
            else if(throwKeyPressed && !MeeleKeyPressed && isGrounded && !isTakingDamage  && !isPlayerDead)
            {
                controller.ChangeState(ThrowState.Instance);
            }
            else if (Input.GetAxis("Horizontal") == 0 && isGrounded && !MeeleKeyPressed && !throwKeyPressed && !isTakingDamage)
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
            controller.animator.SetBool("running",true);
            controller.animator.SetBool("grounded",true);
        }
    }
}
