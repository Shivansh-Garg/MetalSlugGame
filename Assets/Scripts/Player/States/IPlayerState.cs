using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Player;
using UnityEngine;
namespace Assets.Scripts.Player.States
{
    public interface IPlayerState
    {
        void HandleInput(Player controller, AnimatorStateInfo prevState = default(AnimatorStateInfo));
        void UpdateState(Player controller);
    }
}
