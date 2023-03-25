using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateScript : MonoBehaviour
{
    public float startY;
    public float endY;
    public float speed;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            movevagte();
        }
    }

    public void movevagte()
    {
        if (transform.position.y > transform.position.y - endY)
        {
            transform.position = new Vector2(transform.position.x,transform.position.y - 0.05f);
        }
    }
}
