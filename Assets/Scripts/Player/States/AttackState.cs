using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.States
{
    internal class AttackState:IPlayerState
    {
        private static AttackState _instance;
        public static AttackState Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AttackState();
                }
                return _instance;
            }
        }

        private AttackState() { }

        public void HandleInput(Player controller,AnimatorStateInfo prevState)
        {
            bool isAttacking = controller.checkIfAttacking();
            // Handle transition from walk to idle
            bool isGrounded = controller.CheckIfGrounded();

            if (!isAttacking)
            {
                controller.ChangeState(IdleState.Instance);
            }


        }

        public void UpdateState(Player controller)
        {

            // Set walking animation in the Animator
            controller.animator.SetBool("running", false);
            controller.animator.SetBool("grounded", true);
            controller.animator.SetTrigger("attacking");
        }
    }
}
