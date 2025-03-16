using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Plate : MonoBehaviour
{
    public int objectsOnPlate = 0;
    
    public List<Transform> points = new List<Transform>();
    public bool isInverted = false;
    public bool isPressed = false;
    public LineRenderer lineRenderer;
   public GameObject boulder = null;
    private void Awake()
    {
        
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = points.Count+1;
        lineRenderer.SetPosition(0,transform.position+Vector3.down*0.1f);
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i+1,points[i].position); 
        }
       
       
    }

   private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer==LayerMask.NameToLayer("Player")||other.gameObject.layer==LayerMask.NameToLayer("Boulder"))
        {
            if (other.gameObject.layer==LayerMask.NameToLayer("Boulder"))
            {
                boulder=other.gameObject;
            }
            objectsOnPlate++;
            if (isInverted)
            {
                isPressed = false;
            }
            else
            {
                isPressed = true;
            }
            
        }
    }

    public IEnumerator Check()
    {
        yield return new WaitForSeconds(1f);
        if (isInverted)
        {
            isPressed = true;
        }
        else
        {
            isPressed = false;
        }
    }

   /* private void OnCollisionStay(Collision other)
    {
       
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") ||
            other.gameObject.layer == LayerMask.NameToLayer("Boulder"))
        {
            if (isInverted)
            {
                isPressed = false;
            }
            else
            {
                isPressed = true;
            }
            StartCoroutine(Check());
        }
    }*/

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")||other.gameObject.layer == LayerMask.NameToLayer("Boulder"))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Boulder"))
            {
                boulder=null;
            }

            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                objectsOnPlate--;
            }
           
            
            if (objectsOnPlate <= 0)
            {
                if (isInverted)
                {
                    isPressed = true;
                }
                else
                {
                    isPressed = false;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (boulder == null&&isInverted)
        {
            isPressed = true;
        }

        if (objectsOnPlate<0)
        {
            objectsOnPlate = 0;
        }
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
