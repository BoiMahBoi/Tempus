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
    public float moveForce;
    public float maxSpeed;
    public float jumpForce;
    public bool onGround;
    //public GameObject footCol;
    //public GameObject headCol;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void SetTimeOffset()
    {
        if (gameObject.GetComponent<IndividualMovementStoring>().movements.Count > 0)
        {
            timeOffset = Time.time - float.Parse(gameObject.GetComponent<IndividualMovementStoring>().movements[0][2]);
        }
    }

    void Update()
    {
        var moveStorage = gameObject.GetComponent<IndividualMovementStoring>();

        if (moveStorage.movements.Count > completedMovements)
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

    void FixedUpdate()
    {
        float direction = 0;
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
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void SetOnGround(bool status)
    {
        onGround = status;
    }
}
