using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualMovementApplying : MonoBehaviour
{
    public float timeOffset;
    private IndividualMovementStoring moveStorage;
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
    public bool isJumpOnCooldown;
    public bool isBounceOnCooldown;

    void OnEnable()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
        animator = gameObject.GetComponentInChildren<Animator>();
        moveStorage = gameObject.GetComponent<IndividualMovementStoring>();
    }

    public void SetTimeOffset()
    {
        if (moveStorage.movements != null)
        {
            timeOffset = Time.time - (float.Parse(gameObject.GetComponent<IndividualMovementStoring>().movements[0][2]));
        }
    }   
    void Update()
    {
        if (moveStorage.movements != null)
        {
            if (completedMovements < moveStorage.movements.Count)
            {
                if (Time.time > (float.Parse(moveStorage.movements[completedMovements][2]) + timeOffset))
                {
                    Debug.Log("Completed movement " + completedMovements + " at " + Time.time + " which was supposed to be completed at " + (float.Parse(moveStorage.movements[completedMovements][2]) + timeOffset));

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

        rb.AddForce(Vector2.right * direction * moveForce, ForceMode2D.Force);

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

            StartCoroutine("JumpCooldown");
        }

        if (rb.velocity.y < 0.5)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }
    }

    IEnumerator JumpCooldown()
    {
        isJumpOnCooldown = true;
        yield return new WaitForSeconds(0.25f);
        isJumpOnCooldown = false;
    }

    public IEnumerator BounceCooldown()
    {
        isBounceOnCooldown = true;
        yield return new WaitForSeconds(0.25f);
        isBounceOnCooldown = false;
    }

    void OnTriggerEnter2D(Collider2D col) // slightly inconsistent with detecting when on ground maybe switch to OnCollision and ray-, box- or spherecast down
    {
        if (this.enabled == true) // these methods gets called even when the script is disabled, so its necessary to do a check
        {
            onGround = true;
            animator.SetBool("isFalling", false);
        }
    }

    void OnTriggerExit2D(Collider2D col) // slightly inconsistent with detecting when on ground maybe switch to OnCollision 
    {
        if (this.enabled == true) // these methods gets called even when the script is disabled, so its necessary to do a check
        {
            onGround = false;
            animator.SetBool("isFalling", true);
        }
    }
}
