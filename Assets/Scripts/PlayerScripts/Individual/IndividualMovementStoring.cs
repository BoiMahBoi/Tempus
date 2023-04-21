using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class IndividualMovementStoring : MonoBehaviour
{
    public bool isStoring = true;
    public List<List<string>> movements = new List<List<string>>();

    void Update() // switch the boolean playerController replication system with a Tempus relational direction system
    {
        if (isStoring)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                List<string> s = new List<string>() {"down", "A", "" + Time.time};
                movements.Add(s);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                List<string> s = new List<string>() {"up", "A", "" + Time.time};
                movements.Add(s);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                List<string> s = new List<string>() {"down", "D", "" + Time.time};
                movements.Add(s);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                List<string> s = new List<string>() {"up", "D", "" + Time.time};
                movements.Add(s);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                List<string> s = new List<string>() {"down", "Space", "" + Time.time};
                movements.Add(s);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                List<string> s = new List<string>() {"up", "Space", "" + Time.time};
                movements.Add(s);
            }
        }
    }
}
