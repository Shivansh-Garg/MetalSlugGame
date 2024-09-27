using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Actions
{
    internal class HealthCollectible:MonoBehaviour
    {
        [SerializeField]
        private float _healthValue;

        private void OnTriggerEnter2D(Collider2D collision)
        {
                Debug.Log("apple");
            if(collision.tag == "Player")
            {
                collision.GetComponent<PlayerHealth>().Heal(_healthValue);
                gameObject.SetActive(false);
            }
        }
    }

}
