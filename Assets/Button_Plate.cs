using System;
using System.Collections.Generic;
using UnityEngine;

public class Button_Plate : MonoBehaviour
{
    public int objectsOnPlate = 0;
    public ButtonClass thisButton;
    public List<Transform> points = new List<Transform>();
    public bool isPressed = false;
    public LineRenderer lineRenderer;
    private void Awake()
    {
        
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = points.Count+1;
        lineRenderer.SetPosition(0,transform.position);
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i+1,points[i].position); 
        }
       
        thisButton = new ButtonClass(false,false,points);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer==LayerMask.NameToLayer("Player")||other.gameObject.layer==LayerMask.NameToLayer("Boulder"))
        {
            objectsOnPlate++;
            isPressed = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")||other.gameObject.layer == LayerMask.NameToLayer("Boulder"))
        {
            objectsOnPlate--;
            if (objectsOnPlate <= 0)
            {
                isPressed = false;
            }
        }
    }

    private void Update()
    {
        if (isPressed)
        {
            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;
        }
        else
        {
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
        }
    }
}
