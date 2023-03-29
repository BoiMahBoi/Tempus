using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collectableTracker : MonoBehaviour
{
    //String for the tag to detect
    [SerializeField] private string tagToDetect = "Collectable";
    //Array of all the collectables in the scene
    [SerializeField] private GameObject[] allCollectables;
    //Integer holding the value of the number of collected collectables
    [SerializeField] public int numberOfCollected;

    void Start()
    {
        //Finding all gameobjects with the specified tag and adding them to the Array allCollectables
        allCollectables = GameObject.FindGameObjectsWithTag(tagToDetect);
    }

    //Function called from Collector script, when collectable have been collected
    public void collectedIncrementer()
    {
        //Increments numberOfCollected by 1
        numberOfCollected++;
    }
    
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        //On collision with gameobject tagged "Player" and all collectables have been collected.
        if (collider2D.gameObject.CompareTag("Player") && numberOfCollected == allCollectables.Length)
        {
            //Loading next scene in the Build Index
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
