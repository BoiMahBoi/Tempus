using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualTimeloop : MonoBehaviour
{
    public GameObject individualTimeTraveller;
    public Transform timeTravellerManager;

    void Start()
    {
        InvokeRepeating("TimeTravel", 10.0f, 10.0f);
    }

    void TimeTravel()
    {
        foreach (Transform child in timeTravellerManager) // for loop instead? (last child should contain the active player script)
        {
            if (child.gameObject.GetComponent<Player>().enabled == true)
            {
                child.gameObject.GetComponent<Player>().enabled = false; // disable player movement (Since this iteration is now the past)
                child.gameObject.GetComponent<IndividualMovementStoring>().isStoring = false;
                child.gameObject.GetComponent<IndividualMovementApplying>().enabled = true;
            }

            child.position = timeTravellerManager.transform.position;
            child.gameObject.GetComponent<IndividualMovementApplying>().completedMovements = 0;
            child.gameObject.GetComponent<IndividualMovementApplying>().SetTimeOffset();
        }

        GameObject timeTravellerInstance = Instantiate(individualTimeTraveller, transform.position, transform.rotation, timeTravellerManager);
        timeTravellerInstance.gameObject.GetComponent<Player>().enabled = true; // enable player movement (since this iteration is now the present)
        timeTravellerInstance.gameObject.GetComponent<Player>().onGround = true;
    }
}
