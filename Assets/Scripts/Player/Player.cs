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
        private float jumpForce = 5.0f;

        private bool grounded;

        public IPlayerState idleState;
        public IPlayerState walkState;
        public IPlayerState jumpState;

        private IPlayerState currentState;


        //method to find the player body in the //awake method is called whenever the game is instantiated
        private void Awake()
        {
            playerBody = GetComponent<Rigidbody2D>(); // will store the player object as its the closest rigid body
            animator = GetComponent<Animator>();


            // Initialize and cache all states
            idleState = IdleState.Instance;
            walkState = WalkState.Instance;
            jumpState = JumpState.Instance;

            if(CheckIfGrounded() == true)
                currentState = JumpState.Instance;
            else
                currentState = idleState;
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
            AllowPlayerMovement(playerBody, playerSpeed,jumpForce);

        }


        private void AllowPlayerMovement(Rigidbody2D playerBody, float playerSpeed,float jumpForce)
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
            if (Input.GetKey(KeyCode.W) && grounded )
            {
                Jump();
            }

            //change the state to running
            currentState.HandleInput(this);
            currentState.UpdateState(this);

            //animator.SetBool("running", horizontalInput != 0);
            //animator.SetBool("grounded", grounded);
            


        }




        /**
         * Changes the  state of player
         * e.g. from running to jumping 
         */
        public void ChangeState(IPlayerState newState)
        {
            currentState = newState;
        }


        private void Jump()
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
            grounded = false;
            //animator

        }

        private void attack()
        {

        }

        private void ThrowWeapon()
        {
            //check if weapon is equiped


            //max 3 tries


            //game will spawn kunai at random pos

        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "ground")
            {
                grounded = true;
            }
        }

        public bool CheckIfGrounded()
        {
            return grounded == true;
        }


        


    }

}