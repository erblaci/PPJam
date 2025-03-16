using System;
using UnityEditor.Embree;
using UnityEngine;

public class UpdateRespawn : MonoBehaviour
{
  [SerializeField]  private BoxCollider bc;

  private void OnTriggerEnter(Collider other)
  {
    bc.enabled = false;
    Respawn_Manager.main.respawnPointIndex++;
  }
}
