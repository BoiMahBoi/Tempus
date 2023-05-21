using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pterosaurus : MonoBehaviour
{
    public Animator animator;
    private Vector3 startPos;
    public float moveSpeed;

    void Start()
    {
        animator = transform.gameObject.GetComponent<Animator>(); // gets the local animator of the object
        animator.Play("Pterosaurus", -1, Random.Range(0.25f, 1.0f)); // random animation speed makes each pterosaurus flap their wings differently
        startPos = transform.position; // stores each pterosaurus' start position
        moveSpeed = Random.Range(1.0f, 1.5f); // sets a random movement speed to further establish dynamic movement
    }

    void Update()
    {
        transform.position += transform.right * moveSpeed* Time.deltaTime; // moves each pterosaurus to the right at the same speed
    }
}
