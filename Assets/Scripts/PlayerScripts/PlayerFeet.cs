using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
    private Player player;
    private IndividualMovementApplying individualMovementApplying;

    void Start()
    {
        player = gameObject.GetComponent<Player>();
        individualMovementApplying = gameObject.GetComponent<IndividualMovementApplying>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (transform.gameObject.GetComponent<Player>().enabled == true)
        {
            player.SetOnGround(true);
        }
        else
        {
            individualMovementApplying.SetOnGround(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (transform.gameObject.GetComponent<Player>().enabled == true)
        {
            player.SetOnGround(false);
        }
        else
        {
            individualMovementApplying.SetOnGround(false);
        }
    }
}
