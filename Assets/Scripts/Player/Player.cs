using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Player.States;

namespace Assets.Scripts.Player
{

    public class Player : MonoBehaviour
    {

        public Animator animator;
        private Rigidbody2D playerBody;

        [SerializeField]
        public float playerSpeed = 7.0f;
        [SerializeField]
        private float playerScale = 0.5f;
        [SerializeField]
        private float JumpForce = 0.5f;

        private IPlayerState currentState;


        //method to find the player body in the //awake method is called whenever the game is instantiated
        private void Awake()
        {
            playerBody = GetComponent<Rigidbody2D>(); // will store the player object as its the closest rigid body
            animator = GetComponent<Animator>();


            //setting the initial animation state to idle
            currentState = new IdleState();
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
            AllowPlayerMovement(playerBody, playerSpeed);

        }


        private void AllowPlayerMovement(Rigidbody2D playerBody, float playerSpeed)
        {

            float horizontalInput = Input.GetAxis("Horizontal");
            playerBody.velocity = new Vector2(horizontalInput * playerSpeed, playerBody.velocity.y);

            if (horizontalInput > 0.01f) //the player is moving right
            {
                transform.localScale = Vector3.one*playerScale;
            }
            else if(horizontalInput< -0.01f)//the player is moving left
            {
                transform .localScale = new Vector3 (-1,1,1)*playerScale;
            }

            //player jump
            if (Input.GetKey(KeyCode.W))
            {
                playerBody.velocity = new Vector2(playerBody.velocity.x, playerSpeed);
            }
        }

        /**
         * Changes the  state of player
         */
        public void ChangeState(IPlayerState newState)
        {
            currentState = newState;
        }




    }

}