using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool collide;
    [SerializeField] private float projectileSpeed = 5f;
    private Animator anime;
    private BoxCollider2D boxCollider2D;
    private float changeDirection;
    private float livePeriod;

    private void Awake()
    {
        anime = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        livePeriod += Time.deltaTime;
        if (collide)
        {
            return;
        }
        float fireballSpeed = projectileSpeed * Time.deltaTime * changeDirection;
        transform.Translate(fireballSpeed, 0, 0);
        if (livePeriod > 5) { 
            gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collide = true;
            anime.SetTrigger("explode");
            boxCollider2D.enabled = false;
        }
    }

    private void Discard()
    {
        gameObject.SetActive(false);
    }

    public void setDirection(float inDirection)
    {
        collide = false;
        changeDirection = inDirection;
        gameObject.SetActive(true);
        boxCollider2D.enabled = true;

        float xDirection = transform.localScale.x;
        if (Mathf.Sign(xDirection) != inDirection)
        {
            xDirection = -xDirection;
        }

        transform.localScale = new Vector3(xDirection, transform.localScale.y, transform.localScale.z);
    }
}
