using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelResetManager : MonoBehaviour
{
    public GameObject[] resettableObjects;
    public Vector2[] initialObjectState;
    private void Start()
    {
        initialObjectState = new Vector2[resettableObjects.Length];
        for (int i = 0; i < resettableObjects.Length; i++)
        {
            initialObjectState[i] = resettableObjects[i].GetComponent<Transform>().position;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            resetObjectState();
        }
    }

    void resetObjectState()
    {
        for (int i = 0; i < resettableObjects.Length; i++)
        {
            resettableObjects[i].GetComponent<Transform>().position = initialObjectState[i];
        }
    }
}


