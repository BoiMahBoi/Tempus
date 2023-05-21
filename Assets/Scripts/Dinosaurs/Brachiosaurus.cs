using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brachiosaurus : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = transform.gameObject.GetComponent<Animator>();
        Invoke("Move", Random.Range(1.0f, 4.0f));
    }

    void Move()
    {
        animator.SetTrigger("Move");
        Invoke("Move", Random.Range(4.0f, 8.0f));
    }
}
