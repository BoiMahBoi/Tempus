using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{

    private GameObject[] lines;

    public Material lineMaterial;

    public Transform[] startPoint;
    public Transform[] endPoint;
    public Color[] lineColor;



    // Start is called before the first frame update
    void Start()
    {
        lines = new GameObject[startPoint.Length];

        for(int i = 0; i < startPoint.Length; i++)
        {
            GameObject NewLine = new GameObject();
            NewLine.transform.SetParent(gameObject.transform);
            NewLine.name = "Line" + i;
            LineRenderer lineRenderer = NewLine.AddComponent<LineRenderer>();
            lineRenderer.SetPosition(0, startPoint[i].position);
            lineRenderer.SetPosition(1, endPoint[i].position);
            lineRenderer.startWidth = 0.15f;
            lineRenderer.endWidth = 0.15f;
            lineRenderer.startColor = lineColor[i];
            lineRenderer.endColor = lineColor[i];
            lineRenderer.material = lineMaterial;
            lineRenderer.sortingOrder = 5;
            NewLine.GetComponent<LineRenderer>().enabled = false;
            lines[i] = NewLine;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            for(int i = 0; i < lines.Length; i++)
            {
                lines[i].GetComponent<LineRenderer>().enabled = true;
            }
        } else if (Input.GetKeyUp(KeyCode.Tab))
        {
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i].GetComponent<LineRenderer>().enabled = false;
            }
        }

        UpdateLines();
    }

    void UpdateLines()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            LineRenderer line = lines[i].GetComponent<LineRenderer>();
            line.SetPosition(0, startPoint[i].position);
            line.SetPosition(1, endPoint[i].position);
        }
    }

}
