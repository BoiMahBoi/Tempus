using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{

    //public static GameObject[] floatable;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(!Input.GetKey(KeyCode.S))
            {
                Rigidbody2D floatingObject = collision.gameObject.GetComponent<Rigidbody2D>();
                floatingObject.constraints = RigidbodyConstraints2D.FreezePositionY;
                floatingObject.constraints = RigidbodyConstraints2D.None;
                floatingObject.constraints = RigidbodyConstraints2D.FreezeRotation;
                floatingObject.AddForce(Vector2.up * 50);
            }
        }
    }
}
