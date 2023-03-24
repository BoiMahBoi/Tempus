using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    //Script is a player component
    //Script requires collectables to hold the tag "Collectable"

    //Method for when "Player" Collider2D collides with other Collider2D's
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        //On collision with gameobjects tagged "Collectable"
        if (collider2D.gameObject.CompareTag("Collectable"))
        {
            //Destroy collided gameobject with tag "Collectable"
            Destroy(collider2D.gameObject);
        }
        
       
    }
}
