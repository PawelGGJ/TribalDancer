using System;
using UnityEngine;

namespace Assets.Scripts
{
   public class Volcano : MonoBehaviour
   {
      public GameObject Destroyee;
      public DateTime DestructionTime;

      void Update()
      {
         if (Destroyee != null && DateTime.UtcNow > DestructionTime)
         {
            Destroy(Destroyee.gameObject);
            Destroyee = null;
         }
      }
   }
}