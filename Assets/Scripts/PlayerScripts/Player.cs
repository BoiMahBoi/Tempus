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

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
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
    }

    public void SetOnGround(bool status)
    {
        onGround = status;
    }
}
