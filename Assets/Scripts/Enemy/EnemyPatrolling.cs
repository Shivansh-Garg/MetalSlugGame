using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolling : MonoBehaviour
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
            if (inLeftDirection) { 
                if(enemy.position.x >= leftEndpoint.position.x)
                {
                    EnemyMove(-1);
                }
                else
                {
                    changeDirection();
                }
            }
            else
            {   
                if (enemy.position.x <= rightEndpoint.position.x) {
                    EnemyMove(1);
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
        if (anime != null)
        {
            anime.SetBool("isMoving", true);
        }
        else
        {
            Debug.LogWarning("Animator 'anime' is not assigned.");
        }
        enemy.localScale = new Vector3(Mathf.Abs(inintialScale.x) * inDirection, inintialScale.y, inintialScale.z);

        // will make the enemy move
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * inDirection * movementSpeed,
            enemy.position.y, enemy.position.z);
    }

    private void changeDirection()
    {
        if (anime != null)
        {
            anime.SetBool("isMoving", false);
        }
        else
        {
            Debug.LogWarning("Animator 'anime' is not assigned.");
        }
        inLeftDirection = !inLeftDirection;

    }

    private void OnDisable()
    {
        if (anime != null)
        {
            anime.SetBool("isMoving", false);
        }
        else
        {
            Debug.LogWarning("Animator 'anime' is not assigned.");
        }
    }
}
