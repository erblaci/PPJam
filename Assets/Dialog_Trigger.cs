using System;
using Unity.VisualScripting;
using UnityEngine;

public class Dialog_Trigger : MonoBehaviour
{
  [SerializeField] private BoxCollider bc;

  private void OnTriggerEnter(Collider other)
  {
       if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
    {
      DialougueManager.main.DisplayDialougue();
      DialougueManager.main.index++;
      bc.enabled = false;
    }
  }
  
  
}
