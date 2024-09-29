using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] private Transform leftEndpoint;
    [SerializeField] private Transform rightEndpoint;

    [SerializeField] private Transform enemy;

    [SerializeField] private float movementSpeed;

    [SerializeField] private Animator anime;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private bool isChasing;
    [SerializeField] private float chaseRange;

    private Vector3 initialScale;
    private bool inLeftDirection;

    private void Awake()
    {
        initialScale = enemy.localScale;
    }

    private void Update()
    {
        if (enemy != null)
        {
            if (isChasing)
            {
                if (enemy.position.x > playerTransform.position.x)
                {
                    // Move left
                    anime.SetBool("isMoving", true);
                    enemy.position += Vector3.left * movementSpeed * Time.deltaTime;
                    enemy.localScale = new Vector3(-Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
                }
                else if (enemy.position.x < playerTransform.position.x)
                {
                    // Move right
                    anime.SetBool("isMoving", true);
                    enemy.position += Vector3.right * movementSpeed * Time.deltaTime;
                    enemy.localScale = new Vector3(Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
                }
            }
            else
            {
                if (Vector2.Distance(enemy.position, playerTransform.position) < chaseRange)
                {
                    isChasing = true;
                }

                if (inLeftDirection)
                {
                    if (enemy.position.x >= leftEndpoint.position.x)
                    {
                        EnemyMove(-1);
                    }
                    else
                    {
                        ChangeDirection();
                    }
                }
                else
                {
                    if (enemy.position.x <= rightEndpoint.position.x)
                    {
                        EnemyMove(1);
                    }
                    else
                    {
                        ChangeDirection();
                    }
                }
            }
        }
    }

    private void EnemyMove(int direction)
    {
        anime.SetBool("isMoving", true);
        enemy.localScale = new Vector3(Mathf.Abs(initialScale.x) * direction, initialScale.y, initialScale.z);

        // Move the enemy
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * movementSpeed,
            enemy.position.y, enemy.position.z);
    }

    private void ChangeDirection()
    {
        anime.SetBool("isMoving", false);
        inLeftDirection = !inLeftDirection;

        // Flip the enemy's scale based on the direction
        enemy.localScale = new Vector3(Mathf.Abs(initialScale.x) * (inLeftDirection ? -1 : 1), initialScale.y, initialScale.z);
    }

    private void OnDisable()
    {
        anime.SetBool("isMoving", false);
    }
}
