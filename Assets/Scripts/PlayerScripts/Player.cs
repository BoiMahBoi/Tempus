using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    private Animator animator;
    public float moveForce;
    public float maxSpeed;
    public float jumpForce;
    public bool isJumping;
    public bool onGround;

    void Start()
    {
        // move this to a GameManager class
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        // ------------------------------

        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && onGround)
        {
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
        else
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
            animator.SetBool("isJumping", true);
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
        onGround = status;
        
        if (onGround)
        {
            animator.SetBool("isFalling", false);
        }
    }
}
