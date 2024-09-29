using Assets.Scripts.Enemy;
using Assets.Scripts.Game;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMage : MonoBehaviour
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
    private EnemyChase enemyPatrolling;
    private PlayerHealth playerHealth;


    private EnemyHealth health;
    private bool _isDead = false;


    private void Awake()
    {
        anime = GetComponent<Animator>();
        enemyPatrolling = GetComponentInParent<EnemyChase>();

        health = GetComponent<EnemyHealth>();
        if (health != null)
        {
            health.SetHealth(100.0f);
        }
        else
        {
            Debug.Log("Health no found in enemy");
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
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider2D.bounds.center + transform.right * rangeOfAttack * transform.localScale.x * colliderPositionObject,
            new Vector3(boxCollider2D.bounds.size.x * rangeOfAttack, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            // Check if the hit object has the "Player" tag
            if (hit.collider.CompareTag("Player"))
            {
                // Check if playerHealth is already set, if not, initialize it
                if (playerHealth == null)
                {
                    playerHealth = hit.transform.GetComponent<PlayerHealth>();

                    if (playerHealth == null)
                    {
                        Debug.LogError("PlayerHealth component not found on the player!");
                    }
                }

                return true; // The player is in sight
            }
        }

        return false; // Player is not in sight or not hit
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider2D.bounds.center + transform.right * rangeOfAttack * transform.localScale.x * colliderPositionObject,
            new Vector3(boxCollider2D.bounds.size.x * rangeOfAttack, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z));
    }

    private void PlayerDamaged()
    {
        currentTime = 0; // attack performed
        anime.SetTrigger("attack");

        if (CanSeePlayer() && playerHealth != null)
        {
            // Apply damage only if the player is still in range
            playerHealth.TakeDamage(attackPower);
        }
    }
    public void HandleDeadCondition()
    {
        gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerWeapon"))
        {
            Destroy(other);
            anime.SetTrigger("hurt");
            Debug.Log("isTakingDamage from proj");
            health.TakeDamage(20.0f);
            if (health.GeCurrentHealth() == 0)
            {
                anime.SetTrigger("died");
                UIManager.Instance.UpdateScoreUI(400);


                _isDead = true;
               //Destroy(gameObject);
            }

        }
        else if (other.CompareTag("PlayerMeeleWeapon"))
        {
            Debug.Log("isTakingDamage from proj");
            health.TakeDamage(20.0f);
            if (health.GeCurrentHealth() == 0)
            {
                anime.SetTrigger("died");
                UIManager.Instance.UpdateScoreUI(400);

                Destroy(gameObject);
                _isDead = true;
            }

        }
        else if (other.CompareTag("PlayerFireball"))
        {
            Debug.Log("isTakingDamage from proj");
            health.TakeDamage(75.0f);
            if (health.GeCurrentHealth() == 0)
            {
                UIManager.Instance.UpdateScoreUI(75);
                anime.SetTrigger("died");


                Destroy(gameObject);
                _isDead = true;
            }
            //isTakingDamage = false;
        }
        else
        {
            //isTakingDamage = false;
        }


    }
}
