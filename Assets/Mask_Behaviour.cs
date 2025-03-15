using System;
using UnityEngine;

public class Mask_Behaviour : MonoBehaviour
{
    public Transform target;
    public Rigidbody rb;
    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        if (Vector3.Distance(transform.position, target.position) > 1f)
        {
            rb.WakeUp();
            rb.AddForce(dir.normalized * 10f, ForceMode.Force);
        }
        else
        {
           rb.Sleep();
        }
    }
}
