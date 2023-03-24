using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectableTracker : MonoBehaviour
{
    [SerializeField] private string tagToDetect = "Collectable";
    [SerializeField] private GameObject[] allCollectables;
    [SerializeField] private bool allCollected;
    [SerializeField] public int numberOfCollected;

    void Start()
    {
        //Finding all gameobjects with the tag "Collectable" and adding them to the Array allCollectables
        allCollectables = GameObject.FindGameObjectsWithTag(tagToDetect);
    }

    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        //On collision with gameobjects tagged "Player" and the number of collected is equal the amount.
        if (collider2D.gameObject.CompareTag("Player") && numberOfCollected == allCollectables.Length)
        {
            //Destroy collided gameobject with tag "Player"
            Destroy(collider2D.gameObject);
        }
    }
}
