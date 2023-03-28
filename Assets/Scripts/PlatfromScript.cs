using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatfromScript : MonoBehaviour
{
    public float speed;
    public bool isActive;
    public Vector2 startPoint;
    public Vector2 targetPoint;

    private void Start()
    {
        startPoint = transform.position;
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            targetPoint = startPoint - (Vector2.up * 0.5f);
        }
        else
        {
            targetPoint = startPoint;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
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
