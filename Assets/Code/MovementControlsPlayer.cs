using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControlsPlayer : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField]private float speed;
    [SerializeField]private float jumpSpeed;
    [SerializeField]private LayerMask platformLayer;
    [SerializeField] private LayerMask wallLayer;
    private BoxCollider2D boxCollider;
    private Animator anime;
    private float currentHorizontalValue;

    private void Awake()
    {   // these code grab refrences of rigidBody, animator, boxCollider from game object
        body = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentHorizontalValue = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(currentHorizontalValue*speed, body.velocity.y);

        // to flip the character while moving left and right
        if(currentHorizontalValue > 0.01f)
        {
            transform.localScale = new Vector3((float)0.5, (float)0.5, (float)0.5);
        }
        else if (currentHorizontalValue < -0.01f)
        {
            transform.localScale = new Vector3((float)-0.5, (float)0.5, (float)0.5);
        }


        if (Input.GetKey(KeyCode.UpArrow))
        {
            Playerjump();
        }

        //setting animation parameter
        anime.SetBool("Run", currentHorizontalValue != 0); // arrows key not pressed then horizontal input will be zero.
        anime.SetBool("Onground", isOnground()); //set the animation if character is not on the ground, Onground keeps check of it.
    }

    private void Playerjump()
    {
        if (isOnground())
        {
            anime.SetTrigger("Dojump");
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }else if (isOnWall() && !isOnground())
        {
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*3, 0); 
        }
        
    }

    private bool isOnground()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f,platformLayer);
        return raycastHit.collider != null;
    }

    private bool isOnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return (currentHorizontalValue == 0 && isOnground());
    }
}
