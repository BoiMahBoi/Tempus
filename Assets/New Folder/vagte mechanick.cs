using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class vagtemechanick : MonoBehaviour
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
        if (transform.localPosition.y > transform.localPosition.y-endY)
        {
            transform.localPosition = new Vector2(0, 0.3f);
        }
    }
}
