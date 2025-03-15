using System;
using Unity.Cinemachine;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float rotationSpeed = 10f;
    public float gravity = 9.81f;
    
    public Transform cameraTransform;

    private Rigidbody rb;
    private bool isGrounded;
    public bool isHoldingItem = false;
    public Transform ItemHolder;
    public GameObject HoldedItem;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents unwanted rotation
    }

    void Update()
    {
        Pickup();
        Move();
        Jump();
    }

    private void Pickup()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            if (!isHoldingItem)
            {
                RaycastHit[] hits= Physics.SphereCastAll(transform.position,3f,transform.forward, 10f);

                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider.gameObject.layer == LayerMask.NameToLayer("Boulder"))
                    {
                    
                        HoldedItem = hits[i].collider.gameObject;
                        HoldedItem.transform.position = ItemHolder.position;
                        HoldedItem.transform.parent = ItemHolder;
                        HoldedItem.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        HoldedItem.transform.gameObject.GetComponent<Rigidbody>().Sleep();
                        isHoldingItem = true;
                        return;
                    }
                }
            }
            else if(isHoldingItem)
            {
                
                HoldedItem.transform.position = ItemHolder.position+Vector3.up*1+transform.forward*0.5f;
                HoldedItem.transform.parent = null;
                HoldedItem.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                HoldedItem.transform.gameObject.GetComponent<Rigidbody>().WakeUp();
                isHoldingItem = false;
            }
            RaycastHit[] hits2= Physics.SphereCastAll(transform.position,2f,transform.forward, 5f);

            for (int i = 0; i < hits2.Length; i++)
            {
                if (hits2[i].collider.gameObject.layer == LayerMask.NameToLayer("Button"))
                {
                    if (hits2[i].transform.GetComponent<Button_Behaviour>()!=null)
                    {
                        Button_Behaviour b = hits2[i].transform.GetComponent<Button_Behaviour>();
                        b.TurnOn();
                    }
                    
                  
                    return;
                }
            }
            
        }
       
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = cameraTransform.forward * vertical + cameraTransform.right * horizontal;
        moveDirection.y = 0; 
        moveDirection.Normalize();

        Vector3 targetVelocity = moveDirection * moveSpeed;
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);

        
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
