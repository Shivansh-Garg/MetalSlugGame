using Assets.Scripts.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class MeleeEnemy : MonoBehaviour
    {
        [SerializeField] private float attackCoolDownTimer;
        [SerializeField] private float rangeOfAttack;
        [SerializeField] private float colliderPositionObject;
        [SerializeField] private int attackPower;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private BoxCollider2D boxCollider2D;
        private float currentTime = Mathf.Infinity;
        private BoxCollider2D enemySwordCollider;
        [SerializeField] private GameObject enemySword;

        //Making references
        private Animator anime;
        private EnemyPatrolling enemyPatrolling;


        private EnemyHealth health;
        private bool _isDead = false;


        private void Awake()
        {
            anime = GetComponent<Animator>();
            enemyPatrolling = GetComponentInParent<EnemyPatrolling>();

            health = GetComponent<EnemyHealth>();
            health = GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.SetHealth(100.0f);
            }
            else
            {
                Debug.Log("Health no found in enemy");
            }
            if (enemySword != null)
            {
                enemySwordCollider = enemySword.GetComponent<BoxCollider2D>();
            }
            if (enemySwordCollider != null)
            {
                enemySwordCollider.enabled = false;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            currentTime += Time.deltaTime;

            // attack when player is in sight and curret timer is greater than coolDown timer
            if (CanSeePlayer())
            {
                if (currentTime >= attackCoolDownTimer)
                {
                    currentTime = 0; // attack performed
                    attackCoolDownTimer = currentTime;
                    anime.SetTrigger("meleeAttack");
                    PlayerDamaged(); // Apply damage to the player
                }

            }

            if (enemyPatrolling != null)
            {
                enemyPatrolling.enabled = !CanSeePlayer(); // if can't see the player then patrol
            }
        }

        private bool CanSeePlayer()
        {
            RaycastHit2D hit = Physics2D.BoxCast(boxCollider2D.bounds.center + transform.right * rangeOfAttack * transform.localScale.x * colliderPositionObject,
                new Vector3(boxCollider2D.bounds.size.x * rangeOfAttack, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z),
                0, Vector2.left, 0, playerLayer);

            if (hit.collider != null)
            {
                // playerHealth = hit.transform.GetComponent<Health>();
            }

            return hit.collider != null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(boxCollider2D.bounds.center + transform.right * rangeOfAttack * transform.localScale.x * colliderPositionObject,
                new Vector3(boxCollider2D.bounds.size.x * rangeOfAttack, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z));
        }

        private void PlayerDamaged()
        {

        }
        public void HandleDeadCondition()
        {
            gameObject.SetActive(false);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerWeapon"))
            {
                Debug.Log("taking damage from enemy");
                Destroy(other);
                anime.SetTrigger("hurt");
                Debug.Log("isTakingDamage from proj");
                health.TakeDamage(20.0f);
                if (health.GeCurrentHealth() == 0)
                {
                    anime.SetTrigger("died");


                    //Destroy(gameObject);
                    _isDead = true;
                }

            }
            else if (other.CompareTag("PlayerMeeleWeapon"))
            {

                Debug.Log("isTakingDamage from proj");
                health.TakeDamage(20.0f);
                if (health.GeCurrentHealth() == 0)
                {
                    anime.SetTrigger("died");


                    Destroy(gameObject);
                    _isDead = true;
                }

            }
            else
            {
                //isTakingDamage = false;
            }


        }
    }
}