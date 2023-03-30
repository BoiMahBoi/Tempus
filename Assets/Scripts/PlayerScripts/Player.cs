using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public float moveForce;
    public float maxSpeed;
    public float jumpForce;
    public bool isJumping;
    public bool onGround;
    public GameObject footCol;
    public GameObject headCol;
    private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        rb = transform.GetComponent<Rigidbody2D>();
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && onGround)
        {
            animator.SetBool("isJumping", true);
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
        float direction = 0;
        if((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            animator.SetBool("isRunning", true);
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                direction = -1.0f;
                sr.flipX = true;
            }
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                direction = 1.0f;
                sr.flipX = false;
            }
        }

        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isRunning", false);
        }

        rb.AddForce(Vector2.right * direction * moveForce);

        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }
        if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }

        if (isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = false;
        }

        if(rb.velocity.y < 0.5)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        } 

    }

    public void SetOnGround(bool status)
    {
        if (!onGround)
        {
            animator.SetBool("isFalling", false);
        }
        onGround = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(onGround)
        {
            onGround = false;
        }

        onGround = status;
    }
}
