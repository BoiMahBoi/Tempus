using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private collectableTracker collectable;
	

	private void Start()
	{
		collectable = GameObject.Find("ExitTrigger").GetComponent<collectableTracker>();
	}
    //Method for when "Player" Collider2D collides with other Collider2D's
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        //On collision with gameobjects tagged "Collectable"
        if (collider2D.gameObject.CompareTag("Collectable"))
        {
			//Calls function collectedIncrementer of collectableTracker script
			collectable.collectedIncrementer();
            //Destroy collided gameobject with tag "Collectable"
            Destroy(collider2D.gameObject);
        }
        
       
    }
}
