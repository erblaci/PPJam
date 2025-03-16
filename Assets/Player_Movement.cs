using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] public Animator animator;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float rotationSpeed = 10f;
    public float gravity = 9.81f;
    
    public Transform cameraTransform;
    public Transform respawnPoint;
    private Rigidbody rb;
    private bool isGrounded;
    public bool isHoldingItem = false;
    public Transform ItemHolder;
    public GameObject HoldedItem;
    public bool isPlayingSound = false;

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
        if (transform.position.y<-4)
        {
            Dead();
        }
    }

    public void Dead()
    {
        transform.position=Respawn_Manager.main.respawnPoints[Respawn_Manager.main.respawnPointIndex].position;
    }
  /*  public IEnumerator PlaySound()
    {
        isPlayingSound = true;
       Audio_Source.main.PlaySFX(0);
       yield return new WaitForSeconds(3f);
       Audio_Source.main.source.Stop();
       isPlayingSound = false;
    }*/

    
    private void Pickup()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            if (!isHoldingItem)
            {
                RaycastHit[] hits= Physics.SphereCastAll(transform.position,2f,transform.forward, 4f);

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
            RaycastHit[] hits2= Physics.SphereCastAll(transform.position,1f,transform.forward, 3f);

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
        if (Mathf.Abs(vertical)>0||Mathf.Abs(horizontal)>0)
        {
            animator.Play("Walking");
        }
        else
        {
            animator.Play("Idle");
        }

        if (Mathf.Abs(vertical)>0||Mathf.Abs(horizontal)>0&&!isPlayingSound)
        {
          //  StartCoroutine("PlaySound");
           // isPlayingSound = false;
        }
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
