using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Patroling : MonoBehaviour
{
    [SerializeField] private Transform leftEndpoint;
    [SerializeField] private Transform rightEndpoint;

    [SerializeField] private Transform enemy;

    [SerializeField] private float movementSpeed;

    [SerializeField] private Animator anime;

    private Vector3 inintialScale;
    private bool inLeftDirection;
    private void Awake()
    {
        inintialScale = enemy.localScale;
    }

    private void Update()
    {
        if (enemy != null)
        {
            if (inLeftDirection)
            {
                if (enemy.position.x >= leftEndpoint.position.x)
                {
                    EnemyMove(1);
                }
                else
                {
                    changeDirection();
                }
            }
            else
            {
                if (enemy.position.x <= rightEndpoint.position.x)
                {
                    EnemyMove(-1);
                }
                else
                {
                    changeDirection();
                }

            }
        }

    }

    private void EnemyMove(int inDirection)
    {
        anime.SetBool("isMoving", true);
        enemy.localScale = new Vector3(Mathf.Abs(inintialScale.x) * inDirection, inintialScale.y, inintialScale.z);

        // will make the enemy move
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * inDirection * movementSpeed,
            enemy.position.y, enemy.position.z);
    }

    private void changeDirection()
    {
        anime.SetBool("isMoving", false);
        inLeftDirection = !inLeftDirection;

    }

    private void OnDisable()
    {
        anime.SetBool("isMoving", false);
    }
}
