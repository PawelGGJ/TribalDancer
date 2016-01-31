using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
   // Transform of the camera to shake. Grabs the gameObject's transform
   // if null.
   public Transform camTransform;

   // Amplitude of the shake. A larger value shakes the camera harder.
   public float InitialShakeAmount;
   public float InitialCameraField;
   public float DecreaseFactor;

   private float _currentShakeAmount;

   Vector3 originalPos;
   private float _currentCameraField;

   void Start()
   {
      _currentShakeAmount = 0f;
      _currentCameraField = 60f;
   }

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
      if (_currentShakeAmount > 0.05)
      {
         if(Time.frameCount % 4 == 0)
            camTransform.localPosition = originalPos + Random.insideUnitSphere * _currentShakeAmount;

         _currentShakeAmount -= Time.deltaTime * DecreaseFactor;
         _currentCameraField = InitialCameraField - (InitialCameraField - 50f)*_currentShakeAmount;
         FindObjectOfType<Camera>().fieldOfView = _currentCameraField;
         
      }
      else
      {
         camTransform.localPosition = originalPos;
      }
   }

   public void StartShaking(int gameSeconds)
   {
      float multiplier = 1f;
      if (gameSeconds > 60)
         multiplier = 1.3f;
      if (gameSeconds > 120)
         multiplier = 1.8f;
      if (gameSeconds > 180)
         multiplier = 2.4f;
      _currentShakeAmount = InitialShakeAmount * multiplier;
      _currentCameraField = 50f;
   }
}
