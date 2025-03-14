using System;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Vector3 movedir;
   [SerializeField] private float movespeed;
   [SerializeField] private Rigidbody rb;
    
    public void Update()
    {
        GetInput();
        MovePlayer();
    }

    private void MovePlayer()
    {
        rb.AddForce(movedir * movespeed);
    }

    private void GetInput()
    {
        movedir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }
}
