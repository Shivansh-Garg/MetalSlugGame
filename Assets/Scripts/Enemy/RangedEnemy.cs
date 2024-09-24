using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private float attackCoolDownTimer;
    [SerializeField] private float rangeOfAttack;
    [SerializeField] private Transform attackPlace;
    [SerializeField] private GameObject fireballPrefab; // Prefab for the fireball
    [SerializeField] private float colliderPositionObject;
    [SerializeField] private int attackPower;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private BoxCollider2D boxCollider2D;
    private float currentTime = Mathf.Infinity;

    private Animator anime;
    private EnemyPatrolling enemyPatrolling;

    private void Awake()
    {
        anime = GetComponent<Animator>();
        enemyPatrolling = GetComponentInParent<EnemyPatrolling>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        // Attack when the player is in sight and the cooldown timer has finished
        if (CanSeePlayer())
        {
            if (currentTime >= attackCoolDownTimer)
            {
                Attack();
            }
        }

        if (enemyPatrolling != null)
        {
            enemyPatrolling.enabled = !CanSeePlayer(); // Patrol if the player can't be seen
        }
    }

    private bool CanSeePlayer()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider2D.bounds.center + transform.right * rangeOfAttack * transform.localScale.x * colliderPositionObject,
            new Vector3(boxCollider2D.bounds.size.x * rangeOfAttack, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider2D.bounds.center + transform.right * rangeOfAttack * transform.localScale.x * colliderPositionObject,
            new Vector3(boxCollider2D.bounds.size.x * rangeOfAttack, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z));
    }

    private void Attack()
    {
        // Trigger the enemy's attack animation
        anime.SetTrigger("attack");

        // Reset the cooldown timer
        currentTime = 0;

        // Instantiate a new fireball at the attack place position
        GameObject newFireball = Instantiate(fireballPrefab, attackPlace.position, Quaternion.identity);

        // Set the direction for the fireball (based on enemy's facing direction)
        newFireball.GetComponent<Projectile>().setDirection(Mathf.Sign(transform.localScale.x));
    }
}
