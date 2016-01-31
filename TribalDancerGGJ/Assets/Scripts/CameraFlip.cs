using System;
using UnityEngine;
using System.Collections;

public class CameraFlip : MonoBehaviour
{
   private bool _flipped = false;
   public DateTime TimeToNormality = DateTime.MaxValue;

   


   void Update()
   {
      if (_flipped == true && DateTime.UtcNow > TimeToNormality)
      {
         Flip(); // flip back really
         _flipped = false;
         TimeToNormality = DateTime.MaxValue;
      }
   }

   public void Flip()
   {
      _flipped = true;
      var camera = GetComponent<Camera>();
      camera.projectionMatrix = camera.projectionMatrix * Matrix4x4.Scale(new Vector3(-1, 1, 1));
   }
}
