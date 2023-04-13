using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualTimeloop : MonoBehaviour
{
    public GameObject individualTimeTravellerPrefab;
    public Transform timeTravellerManager;
    public List<GameObject> individualTimeTravellers = new List<GameObject>();

    void Start()
    {
        InvokeRepeating("TimeTravel", 10.0f, 10.0f);
    }

    void TimeTravel()
    {
        foreach (GameObject timeTraveller in individualTimeTravellers) // for loop instead? (last child should contain the active player script)
        {
            if (timeTraveller.gameObject.GetComponent<Player>().enabled == true)
            {
                timeTraveller.gameObject.GetComponent<Player>().enabled = false; // disable player movement (Since this iteration is now the past)
                timeTraveller.gameObject.GetComponent<IndividualMovementStoring>().isStoring = false;
                timeTraveller.gameObject.GetComponent<IndividualMovementApplying>().enabled = true;
            }

            timeTraveller.gameObject.GetComponentInChildren<Animator>().SetBool("isJumping", false);
            timeTraveller.transform.position = timeTravellerManager.transform.position;
            timeTraveller.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            timeTraveller.gameObject.GetComponent<IndividualMovementApplying>().completedMovements = 0;
            timeTraveller.gameObject.GetComponent<IndividualMovementApplying>().SetTimeOffset();
        }

        GameObject timeTravellerInstance = Instantiate(individualTimeTravellerPrefab, timeTravellerManager.transform.position, timeTravellerManager.transform.rotation, timeTravellerManager);
        individualTimeTravellers.Add(timeTravellerInstance);
    }
}
