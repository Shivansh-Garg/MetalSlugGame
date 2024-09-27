using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.States
{
    internal class TakingDamageState:IPlayerState
    {
        private static TakingDamageState _instance;

        public static TakingDamageState Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TakingDamageState();
                }
                return _instance;
            }
        }

        private TakingDamageState() { }

        public void HandleInput(Player controller, AnimatorStateInfo prevState)
        {
            bool isPlayerDead = controller.checkIfPlayerDead();
            bool isAttacking = controller.checkIfAttacking();
            bool isTakingDamage =controller.checkIfTakingDamage();
            bool isGrounded = controller.CheckIfGrounded();
            if (isPlayerDead )
            {
                controller.ChangeState(DeadState.Instance);
            }
            else
            {
                controller.ChangeState(IdleState.Instance);
            }


        }

        public void UpdateState(Player controller)
        {

            // Set walking animation in the Animator
            controller.animator.SetBool("running", false);
            controller.animator.SetBool("grounded", true);
            controller.animator.SetTrigger("takingDamage");
        }
    }
}
