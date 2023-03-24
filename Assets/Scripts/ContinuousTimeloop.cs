using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousTimeloop : MonoBehaviour
{
    public GameObject continuousTimeTraveller;
    public Transform timeTravellerManager;

    void Start()
    {
        InvokeRepeating("TimeTravel", 5.0f, 5.0f);
    }

    void TimeTravel()
    {
        GameObject timeTravellerInstance = Instantiate(continuousTimeTraveller, transform.position, transform.rotation, timeTravellerManager);
        timeTravellerInstance.transform.gameObject.GetComponent<Player>().onGround = true;
    }
}
