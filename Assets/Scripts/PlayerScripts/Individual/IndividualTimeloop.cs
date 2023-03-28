using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualTimeloop : MonoBehaviour
{
    public Transform timeTravellerManager;
    public GameObject individualTimeTravellerPrefab;
    public List<GameObject> individualTimeTravellers = new List<GameObject>();

    void Start()
    {
        // find all objects in the scene with the tag tempus, and add them to the arraylist of timetravellers

        InvokeRepeating("TimeTravel", 10.0f, 10.0f);
    }

    void TimeTravel()
    {
        foreach (GameObject timeTraveller in individualTimeTravellers) // for loop instead? (last child should contain the active player script)
        {
            if (timeTraveller.transform.gameObject.GetComponent<Player>().enabled == true)
            {
                timeTraveller.transform.gameObject.GetComponent<Player>().enabled = false; // disable player movement (Since this iteration is now the past)
                timeTraveller.transform.gameObject.GetComponent<IndividualMovementStoring>().isStoring = false;
                timeTraveller.transform.gameObject.GetComponent<IndividualMovementApplying>().enabled = true;
            }

            //if (timeTraveller.transform.parent.CompareTag("WeightedPlatform"))
            //{
            //    // unparent
            //}

            timeTraveller.transform.position = timeTravellerManager.transform.position;
            timeTraveller.transform.gameObject.GetComponent<IndividualMovementApplying>().completedMovements = 0;
            timeTraveller.transform.gameObject.GetComponent<IndividualMovementApplying>().SetTimeOffset();
        }

        GameObject timeTravellerInstance = Instantiate(individualTimeTravellerPrefab, transform.position, transform.rotation);
        individualTimeTravellers.Add(timeTravellerInstance);
        //timeTravellerInstance.gameObject.GetComponent<Player>().enabled = true; // enable player movement (since this iteration is now the present)
        //timeTravellerInstance.gameObject.GetComponent<Player>().onGround = true;
    }
}
