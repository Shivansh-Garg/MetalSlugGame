using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using UnityEngine.SceneManagement;

namespace Assets.Scripts.Player.Actions
{
    public class PlayerRespawn : MonoBehaviour
    {
        [SerializeField] private AudioClip checkpoint;
        private Transform currentCheckpoint;
        private PlayerHealth playerHealth;

        private void Awake()
        {
            playerHealth = GetComponent<PlayerHealth>();
        }

        public void DoRespawn()
        {

            //playerHealth.Respawn(); //Restore player health and reset animation
            transform.position = currentCheckpoint.position; //Move player to checkpoint location
            //show the game over screen
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        
    }
}
