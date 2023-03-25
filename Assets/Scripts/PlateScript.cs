using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateScript : MonoBehaviour
{
    public float startY;
    public float endY;

    public GameObject[] childPlatfroms;
    public float childStartY;
    public float childEndY;
    private float[] childStartPunkt;

    private void Start()
    {
        startY = transform.position.y;
        childStartY = transform.position.y;

        childStartPunkt = new float[childPlatfroms.Length];
        for(int i = 0; i < childPlatfroms.Length; i++)
        {
            childStartPunkt[i] = childPlatfroms[i].GetComponent<Transform>().position.y;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.transform.SetParent(transform);
        if (collision.gameObject.CompareTag("Player"))
        {
            childMoveUp();
            movevagte();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.SetParent(null);
        if (collision.gameObject.CompareTag("Player"))
        {
            moveback();
        }
    }

    public void movevagte()
    {
        if (transform.position.y > startY - endY)
        {
            transform.position = new Vector2(transform.position.x,transform.position.y - 0.05f);
        }
    }
    public void moveback()
    {
        if (transform.position.y < startY)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.05f);
        }
    }

    public void childMoveUp()
    {
        int i = 0;
       foreach(GameObject childObject in childPlatfroms)
        {
            if (childObject.transform.position.y < childStartPunkt[i] + endY)
            {
                Debug.Log("hey");
                childObject.transform.position = new Vector2(childObject.transform.position.x, childObject.transform.position.y + 0.05f);
            }
            i++;
           
        }
    }
}
