using System;
using UnityEngine;

public class Respawn_Manager : MonoBehaviour
{
   [SerializeField] public Transform[] respawnPoints;
   [SerializeField] public int respawnPointIndex;
   public static Respawn_Manager main;

   private void Awake()
   {
      if (main != null)
      {
         Destroy(gameObject);
      }
      else
      {
         main = this;
      }
   }
}
