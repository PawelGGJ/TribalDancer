using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
   public class DancerMoveInstance : MonoBehaviour
   {
      private DateTime _birth;
      public Vector2 InitialPosition;
      public Vector2 TargetPosition;
      public float TravelTimeMillis = 200f;


      void Start()
      {
         _birth = DateTime.UtcNow;
      }

      void Update()
      {
         double timePassed = (DateTime.UtcNow - _birth).TotalMilliseconds;
         if (timePassed > 300)
         {
          //  Destroy(gameObject);
            return;
         }
         double progressPercent = (DateTime.UtcNow - _birth).TotalMilliseconds / TravelTimeMillis;
         Vector2 position = Vector2.Lerp(InitialPosition, TargetPosition, (float)progressPercent);
         transform.position = position;
      }
   }
}