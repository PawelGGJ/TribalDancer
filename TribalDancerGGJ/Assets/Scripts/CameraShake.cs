using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
   // Transform of the camera to shake. Grabs the gameObject's transform
   // if null.
   public Transform camTransform;

   // Amplitude of the shake. A larger value shakes the camera harder.
   public float shakeAmount = 0.7f;
   public float decreaseFactor = 1.0f;

   Vector3 originalPos;

   void Awake()
   {
      if (camTransform == null)
      {
         camTransform = GetComponent(typeof(Transform)) as Transform;
      }
   }

   void OnEnable()
   {
      originalPos = camTransform.localPosition;
   }

   void Update()
   {
      if (shakeAmount > 0.05)
      {
         if(Time.frameCount % 4 == 0)
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

         shakeAmount -= Time.deltaTime * decreaseFactor;
      }
      else
      {
         camTransform.localPosition = originalPos;
      }
   }
}
