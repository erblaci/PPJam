using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask_Behaviour : MonoBehaviour
{
    public Transform target;
    
    public Rigidbody rb;
    public bool IsStuck = false;
    private void Update()
    {
        transform.LookAt(target.parent);
        Vector3 dir = (target.position - transform.position).normalized;
        if (Vector3.Distance(transform.position, target.position) > 0.5f)
        {
            rb.WakeUp();
            rb.MovePosition(transform.position + dir * Time.deltaTime*10f);
        }
        else
        {
           rb.Sleep();
        }

        if (IsStuck)
        {
            StartCoroutine(Wait());
            
        }
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        if (IsStuck)
        {
            transform.position = target.position;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        IsStuck = true;
    }

    private void OnCollisionExit(Collision other)
    {
        IsStuck = false;
    }
}
