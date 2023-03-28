using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlatfromScript : MonoBehaviour
{
    public float speed;
    public bool isActive;
    public int startPoint;
    public Transform[] points;

    int i;
    private void Start()
    {
        transform.position = points[startPoint].position;
        i = startPoint;
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }

        isActive = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.transform.SetParent(transform);
            //Move();
            isActive = true;
        }
    }
}
