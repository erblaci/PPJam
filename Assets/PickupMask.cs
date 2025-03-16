using System;
using UnityEngine;

public class PickupMask : MonoBehaviour
{
    [SerializeField] public GameObject Mask;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer==LayerMask.NameToLayer("Player"))
        {
            DialougueManager.main.DisplayDialougue();
            DialougueManager.main.index++;
            Mask.SetActive(true);
            Destroy(gameObject);
        }
        
        
    }
}
