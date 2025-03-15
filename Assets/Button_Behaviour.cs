using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Behaviour : MonoBehaviour
{
   
    
    public List<Transform> points = new List<Transform>();
    public int timer = -1;
    public bool isPressed = false;
    public LineRenderer lineRenderer;
    private void Awake()
    {
        
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = points.Count+1;
        lineRenderer.SetPosition(0,transform.position+transform.forward*-0.2f);
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i+1,points[i].position); 
        }
       
       
    }

   public void TurnOn()
    {
        if (timer==-1)
        {
            isPressed = true;
        }
        
    }

    public IEnumerator TurnOff()
    {
        isPressed = true;
        yield return new WaitForSeconds(timer);
        isPressed = false;
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
