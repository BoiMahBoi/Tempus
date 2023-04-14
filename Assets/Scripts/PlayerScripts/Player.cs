using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    //Variable to hold the ParticleSystem dust.
    [SerializeField] private ParticleSystem dust;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    private Animator animator;
    public float moveForce;
    public float maxSpeed;
    public float jumpForce;
    public bool onGround;
    public bool isJumping;
    public bool isJumpOnCooldown;
    public bool isBounceOnCooldown;
    //private Vector3 testVector3 = Vector3.zero;


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
        if (Input.GetKey(KeyCode.Space) && onGround && !isJumpOnCooldown)
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

            //if (onGround)
            //{
            //    rb.velocity = new Vector2(0.0f, 0.0f);
            //}
        }

        rb.AddForce(Vector2.right * direction * moveForce, ForceMode2D.Force);
        //Vector3 targetVelocity = new Vector2(direction * moveForce, rb.velocity.y);
        //rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref testVector3, 0.5f);

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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.enabled == true) // these methods gets called even when the script is disabled, so its necessary to do a check
        {
            onGround = true;
            animator.SetBool("isFalling", false);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (this.enabled == true) // these methods gets called even when the script is disabled, so its necessary to do a check
        {
            onGround = false;
            animator.SetBool("isFalling", true);
        }
    }

    //Function that creates a quick burst of particles near players feet.
    void CreateDust()
    {
        dust.Play();
    }
}
