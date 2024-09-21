using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{

    public class Player : MonoBehaviour
    {
        private Rigidbody2D playerBody;

        [SerializeField]
        private float playerSpeed = 7.0f;

        //method to find the player body in the //awake method is called whenever the game is instantiated
        private void Awake()
        {
            playerBody = GetComponent<Rigidbody2D>(); // will store the player object as its the closest rigid body
        }

        // Start is called before the first frame update
        void Start()
        {

            transform.position = new Vector3(0,0,0);
            //set players Initial position
            
        }

        // Update is called once per frame
        private void Update()
        {
            
            playerBody.velocity = new Vector2(Input.GetAxis("Horizontal")*playerSpeed, playerBody.velocity.y);

            //player jump
            if (Input.GetKey(KeyCode.W))
            {
                playerBody.velocity = new Vector2(playerBody.velocity.x, Input.GetAxis("Vertical") * playerSpeed);
            }
        }
    }

}