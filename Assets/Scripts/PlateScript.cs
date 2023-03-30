using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateScript : MonoBehaviour
{
    private float startY;
    public float endY;

    [Range(0.1f, 5)]
    public float platformSpeed;

    public GameObject[] childPlatfroms;
    public float childEndY;
    private float[] childStartPunkt;

    private void Start()
    {
        startY = transform.position.y;

        childStartPunkt = new float[childPlatfroms.Length];
        for(int i = 0; i < childPlatfroms.Length; i++)
        {
            childStartPunkt[i] = childPlatfroms[i].GetComponent<Transform>().position.y;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            childMoveUp();
            movevagte();
        }

        if (collision.gameObject.CompareTag("Crate"))
        {
            collision.transform.SetParent(transform);
            childMoveUp();
            movevagte();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            moveback();
        }

        if (collision.gameObject.CompareTag("Crate"))
        {
            collision.transform.SetParent(null);
            moveback();
        }
    }

    public void movevagte()
    {
        if (transform.position.y > startY - endY)
        {
            transform.position = new Vector2(transform.position.x,transform.position.y - (0.05f * platformSpeed));
        }
    }
    public void moveback()
    {
        if (transform.position.y < startY)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + (0.05f * platformSpeed));
        }
    }

    public void childMoveUp()
    {
        int i = 0;
       foreach(GameObject childObject in childPlatfroms)
        {
            if (childObject.transform.position.y < childStartPunkt[i] + childEndY)
            {
                childObject.transform.position = new Vector2(childObject.transform.position.x, childObject.transform.position.y + (0.05f * platformSpeed));
            }
            i++;
           
        }
    }
}
