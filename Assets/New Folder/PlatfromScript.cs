using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlatfromScript : MonoBehaviour
{
    public float speed;
    public int startPoint;
    public Transform[] points;

    int i;
    private void Start()
    {
        transform.position = points[startPoint].position;
        i = startPoint;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.transform.SetParent(transform);
        if (collision.gameObject.CompareTag("Player")){
            Move();
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

}
