using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Player.States;
using System.Windows.Input;
using Assets.Scripts.Weapon;
using Assets.Scripts.Player.Actions;
using Assets.Scripts.Game;

namespace Assets.Scripts.Player
{

    public class Player : MonoBehaviour
    {

        public Animator animator;
        MySceneManager sceneManager;


        //references to script
        private KunaiAttack ThrowKunai;
        private PlayerHealth playerHealth;
        private FireballAttack ThrowFireball;

        //references to gameObjects
        public GameObject sword;
        
        
        private Rigidbody2D playerBody;
        private BoxCollider2D swordCollider;
        private BoxCollider2D boxCollider;


        [SerializeField]
        private LayerMask groundLayer;
        [SerializeField]
        public float playerSpeed = 7.0f;
        
        private float playerScale = 0.3f;
        [SerializeField]
        private float jumpForce = 5.0f;

        //private bool grounded;
        private bool isMeeleAttacking;
        private bool isKunaiAttacking;
        private bool isTakingDamage;
        private bool isTakingTrapDamage = false;
        private bool isPlayerDead = false;
        private bool isCrouching = false;

        public IPlayerState idleState;
        public IPlayerState walkState;
        public IPlayerState jumpState;
        public IPlayerState jumpAttackState;
        public IPlayerState throwState;
        public IPlayerState deadState;
        public IPlayerState takingDamageState;
        public IPlayerState attackState;
        public IPlayerState currentState;


        //method to find the player body in the //awake method is called whenever the game is instantiated
        private void Awake()
        {
            
            playerBody = GetComponent<Rigidbody2D>(); // will store the player object as its the closest rigid body
            animator = GetComponent<Animator>();
            
            
            //initializing all the colliders
            boxCollider = GetComponent<BoxCollider2D>();
            if (sword != null) { swordCollider = sword.GetComponent<BoxCollider2D>(); }

            //initializing mono behaviour scripts
            ThrowKunai = GetComponentInChildren<KunaiAttack>(); // in C# cannot instanitiate script with Mono Behaviour using the new keyword
            playerHealth = GetComponent<PlayerHealth>();
            ThrowFireball = GetComponent<FireballAttack>();

            if (ThrowKunai == null)
            {
                Debug.LogError("KunaiScript not found on the player or its children!");
            }

            //swordCommand = new SwordAttack(sword);
            //kunaiCommand = new KunaiAttack(kunai);

            if (swordCollider != null)
            {
                Debug.Log("Sword BoxCollider found: " + swordCollider);
                swordCollider.enabled = false;
            }


            // Initialize and cache all states
            idleState = IdleState.Instance;
            walkState = WalkState.Instance;
            jumpState = JumpState.Instance;
            attackState = AttackState.Instance;
            jumpAttackState = JumpAttackState.Instance;
            throwState = ThrowState.Instance;
            deadState = DeadState.Instance; 
            takingDamageState= TakingDamageState.Instance;


            //creating an instance of scene manager

            if (CheckIfGrounded() == true)
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
            AllowPlayerAttack();

            CheckPlayerStatus();
            //change the state of player
            currentState.HandleInput(this);
            currentState.UpdateState(this);


        }

        private void CheckPlayerStatus()
        {
            //means the player is dead
            if(playerHealth.currentHealth <= 0 && !isPlayerDead)
            {
                isPlayerDead = true;
                isTakingDamage = false;
                currentState.HandleInput(this);
                currentState.UpdateState(this);
                //Destroy(gameObject);
            }
            isTakingDamage = false;
        }


        private void HandlePlayerDeathRoutine()
        {
            // Destroy the player GameObject
            MySceneManager.Instance.GetCurrentScene();

            //proceed to menu scene

        }


        private void AllowPlayerMovement(Rigidbody2D playerBody, float playerSpeed,float jumpForce)
        {

            float horizontalInput = Input.GetAxis("Horizontal");
            playerBody.velocity = new Vector2(horizontalInput * playerSpeed, playerBody.velocity.y);

            if (horizontalInput > 0.01f) //the player is moving right
            {
                transform.localScale = new Vector3(1,1,1)*playerScale;
            }
            else if(horizontalInput< -0.01f)//the player is moving left
            {
                transform .localScale = new Vector3 (-1,1,1)*playerScale;
            }

            //player jump
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && CheckIfGrounded() )
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Melee();

                isCrouching = true;

            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                isCrouching = false;
               
            }
            else
            {
                isCrouching = false;
            }

            //animator.SetBool("running", horizontalInput != 0);
            //animator.SetBool("grounded", grounded);

        }
        private void AllowPlayerAttack()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Melee();

                isMeeleAttacking = true;

            }
            else if(Input.GetKeyUp(KeyCode.F))
            {
                isMeeleAttacking= false;
                if (swordCollider != null)
                {
                    // Disable the collider when 'E' is released
                    swordCollider.enabled = false;
                }
            }
            else
            {
                isMeeleAttacking = false;
            }

            //do  for throw attacks
            //creating the laser instance (shooting lasers)
            if (Input.GetKey(KeyCode.Q))
            {
                RangedAttack();
                isKunaiAttacking = true;
            }
            else if(Input.GetKeyUp(KeyCode.Q)){
                isKunaiAttacking = false;
            }
            else
            {
                isKunaiAttacking = false;
            }

            if (Input.GetKey(KeyCode.Z))
            {
                FireballAttack();
                isKunaiAttacking = true;
            }
            else if (Input.GetKeyUp(KeyCode.Z))
            {
                isKunaiAttacking = false;
            }
            else
            {
                isKunaiAttacking = false;
            }


        }

        private void Jump()
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
        }

        private void Melee()
        {
            //activating the sword collider
            if (swordCollider != null)
            {
                swordCollider.enabled = true;
            }
            
        }

        private void RangedAttack()
        {
            //max 3 ammo
            ThrowKunai.SpawnKunai();

        }

        private void FireballAttack()
        {
            ThrowFireball.SpawnFireball();
        }


        // Detect collision with damaging objects
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("EnemyProjectiles"))
            {
                isTakingDamage = true;
                playerHealth.TakeDamage(10.0f);

            }
            if (other.CompareTag("lvl2"))
            {
                MySceneManager.Instance.OpenGameScene("Scene_2");
            }
            else if (other.CompareTag("lvl3"))
            {
                MySceneManager.Instance.OpenGameScene("Scene_3");
            }

            currentState.HandleInput(this);
            currentState.UpdateState(this);

        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("spikes"))
            {
                if ( !isTakingTrapDamage)
                {
                    // Start taking damage
                    isTakingDamage = true;
                    isTakingTrapDamage = true;
                    StartCoroutine(TakeDamageOverTime());
                }

            }


            currentState.HandleInput(this);
            currentState.UpdateState(this);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("spikes"))
            {
                // Stop taking damage when leaving the spikes
                isTakingDamage = false;
                isTakingTrapDamage = false;
                StopCoroutine(TakeDamageOverTime());
            }

            currentState.HandleInput(this);
            currentState.UpdateState(this);
        }

        //co routine for taking damage
        IEnumerator TakeDamageOverTime()
        {
            while (isTakingTrapDamage)
            {
                playerHealth.TakeDamage(10.0f);

                // Wait for a specific interval before taking damage again
                yield return new WaitForSeconds(1.0f); // Change 1.0f to adjust the damage frequency
            }
        }





         
        public bool CheckIfGrounded()
        {
            RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f,groundLayer);
            return raycastHit.collider != null;
        }
        public bool checkIfAttacking()
        {
            return isMeeleAttacking;
        }
        public bool checkIfKunaiAttacking()
        {
            return isKunaiAttacking;
        }

        public bool checkIfTakingDamage()
        {
            return isTakingDamage;
        }

        public bool checkIfPlayerDead()
        {
            return isPlayerDead;
        }
        public bool checkIfCrouching()
        {
            return isCrouching;
        }

        /**
         * Changes the  state of player
         * e.g. from running to jumping 
         */
        public void ChangeState(IPlayerState newState)
        {
            currentState = newState;
        }

      

    }

}