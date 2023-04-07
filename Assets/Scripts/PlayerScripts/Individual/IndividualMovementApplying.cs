using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualMovementApplying : MonoBehaviour
{
    public float timeOffset;
    public int completedMovements = 0;
    private bool a;
    private bool d;
    private bool space;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    private Animator animator;
    public float moveForce;
    public float maxSpeed;
    public float jumpForce;
    public bool onGround;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    public void SetTimeOffset()
    {
        timeOffset = Time.time - float.Parse(transform.gameObject.GetComponent<IndividualMovementStoring>().movements[0][2]);
    }

    void Update()
    {
        var moveStorage = transform.gameObject.GetComponent<IndividualMovementStoring>();
        
        if (moveStorage != null)
        {
            if (completedMovements < moveStorage.movements.Count)
            {
                if (Time.time > float.Parse(moveStorage.movements[completedMovements][2]) + timeOffset)
                {
                    if (moveStorage.movements[completedMovements][0] == "down")
                    {
                        if (moveStorage.movements[completedMovements][1] == "A")
                        {
                            a = true;
                        }
                        if (moveStorage.movements[completedMovements][1] == "D")
                        {
                            d = true;
                        }
                        if (moveStorage.movements[completedMovements][1] == "Space")
                        {
                            space = true;
                        }
                    }
                    if (moveStorage.movements[completedMovements][0] == "up")
                    {
                        if (moveStorage.movements[completedMovements][1] == "A")
                        {
                            a = false;
                        }
                        if (moveStorage.movements[completedMovements][1] == "D")
                        {
                            d = false;
                        }
                        if (moveStorage.movements[completedMovements][1] == "Space")
                        {
                            space = false;
                        }
                    }

                    completedMovements++;
                }
            }
        }
    }

    void FixedUpdate()
    {

        float direction = 0;
        if (a || d)
        {
            animator.SetBool("isRunning", true);

            if (a)
            {
                direction = -1.0f;
                sr.flipX = true;
            }
            if (d)
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

        if (space && onGround)
        {
            animator.SetBool("isJumping", true);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (rb.velocity.y < 0.5)
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
