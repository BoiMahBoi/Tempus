using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedPlatforms : MonoBehaviour
{
    private float startY;
    public float endY;

    [Range(0.1f, 5)]
    public float platformSpeed;

    public GameObject[] childPlatfroms;
    public float[] childEnd;
    public bool[] movesHorizontally;
    public float[] childMoveSpeed;
    private float[] childStart;

    private void Start()
    {
        startY = transform.position.y;
        childStart = new float[childPlatfroms.Length];

        for (int i = 0; i < childPlatfroms.Length; i++)
        {
            childStart[i] = childPlatfroms[i].GetComponent<Transform>().position.y;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
        if (collision.gameObject.CompareTag("Player"))
        {
            MoveBack();
        }

        if (collision.gameObject.CompareTag("Crate"))
        {
            collision.transform.SetParent(null);
            MoveBack();
        }
    }

    public void movevagte()
    {
        if (transform.position.y > startY - endY)
        {
            Debug.Log("Move");
            transform.position = new Vector2(transform.position.x, transform.position.y - (0.05f * platformSpeed));
        }
    }

    public void MoveBack()
    {
        if (transform.position.y < startY)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + (0.05f * platformSpeed));
        }
    }
    
    public void childMoveUp()
    {
        int i = 0;
        foreach (GameObject childObject in childPlatfroms)
        {
            if (childObject.GetComponent<WeightedPlatforms>() != null)
            {
                if (movesHorizontally[i])
                {
                    if (childObject.transform.position.x < childStart[i] + childEnd[i])
                    {
                        childObject.transform.position = new Vector2(childObject.transform.position.x + (0.05f * childMoveSpeed[i]), childObject.transform.position.y);
                    }
                } else
                {
                    if (childObject.transform.position.y < childStart[i] + childEnd[i])
                    {
                        childObject.transform.position = new Vector2(childObject.transform.position.x, childObject.transform.position.y + (0.05f * childMoveSpeed[i]));
                    }
                }
            }
            else if (childObject.GetComponent<Crumble>() != null)
            {
                childObject.GetComponent<Crumble>().LifeIsSuffering();
            }
            i++;
        }
    }
}
