using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlatfromScript : MonoBehaviour
{
    public float speed;
    public bool isActive;
    public Vector2 startPos;
    public Vector2 activePos;

    private void Start()
    {
        startPos = transform.position;
        activePos = transform.position * Vector2.down - new Vector2(0.0f, -0.5f);
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, (isActive ? activePos : startPos), speed * Time.deltaTime);
        }

        // use isActive to trigger other objects

        isActive = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActive = true;
        }
    }
}
