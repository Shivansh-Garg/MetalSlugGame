using Assets.Scripts.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Actions
{
    internal class FireballCollectible : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                Debug.Log("FOund fireball");
                collision.GetComponent<FireballAttack>().increaseFireballCount();
                UIManager.Instance.UpdateFireballs(2);
                gameObject.SetActive(false);
            }
        }
    }

}
