using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelResetManager : MonoBehaviour
{
    //Public array to hold objects that should be reset during time-loop
    public GameObject[] resettableObjects;
    //Array to hold the initial position of objects in resettableObjects
    public Vector2[] initialObjectState;
    private collectableTracker collectable;

    private void Start()
    {
        collectable = GameObject.Find("ExitTrigger").GetComponent<collectableTracker>();

        initialObjectState = new Vector2[resettableObjects.Length];

        //For loop iterating through all objects in the resettableObjects array
        for (int i = 0; i < resettableObjects.Length; i++)
        {
            //Every resettableObject's transforms position is put in the initialObjectState array
            initialObjectState[i] = resettableObjects[i].GetComponent<Transform>().position;
        }
    }

    //Function resetting the dynamic objects attached to the array ressettableObjects.
    public void resetObjectState()
    {
        //For loop iterating through the resettableObjects array
        for (int i = 0; i < resettableObjects.Length; i++)
        {
            //Each object in the array, has it's position set to the saved position in the array initialObjectState
            resettableObjects[i].GetComponent<Transform>().position = initialObjectState[i];

            //If the object in the array has the script component Crumble
            if (resettableObjects[i].GetComponent<Crumble>() != null)
            {
                //Executing a function in the Crumble script component
                resettableObjects[i].GetComponent<Crumble>().LifeIsNotSuffering();
            } 
            else if (resettableObjects[i].CompareTag("Collectable"))
            {
                resettableObjects[i].SetActive(true);
            }

            if (resettableObjects[i].GetComponent<Rigidbody2D>() != null)
            {
                resettableObjects[i].GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
        }
        collectable.collectedReset();
    }
}