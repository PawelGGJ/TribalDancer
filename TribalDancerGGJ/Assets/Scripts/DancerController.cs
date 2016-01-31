using System;
using Assets.Scripts;
using UnityEngine;
using System.Collections;

public class DancerController : MonoBehaviour
{

   private Vector2 _leftUpPosition;
   private Vector2 _upPosition;
   private Vector2 _rightUpPosition;
   private Vector2 _rightPosition;
   private Vector2 _rightDownPosition;
   private Vector2 _downPosition;
   private Vector2 _leftDownPosition;
   private Vector2 _leftPosition;
   private Vector2 _centerPosition;

   private Vector2 _lastDancerTilePosition;

   private GameObject _lastAnimation;

	// Use this for initialization
	void Start ()
	{
	   _leftUpPosition = GameObject.Find("LeftUp").transform.position;
      _upPosition = GameObject.Find("Up").transform.position;
      _rightUpPosition = GameObject.Find("RightUp").transform.position;
      _rightPosition = GameObject.Find("Right").transform.position;
      _rightDownPosition = GameObject.Find("RightDown").transform.position;
      _downPosition = GameObject.Find("Down").transform.position;
      _leftDownPosition = GameObject.Find("LeftDown").transform.position;
      _leftPosition = GameObject.Find("Left").transform.position;
      _centerPosition = GameObject.Find("Center").transform.position;

	   _lastDancerTilePosition = _centerPosition;
	}
	
	// Update is called once per frame
	void Update ()
	{
	   if (Input.GetKeyDown(KeyCode.DownArrow))
	      MoveTo(_downPosition);
	   if (Input.GetKeyDown(KeyCode.LeftArrow))
	      MoveTo(_leftPosition);
	   if (Input.GetKeyDown(KeyCode.RightArrow))
	      MoveTo(_rightPosition);
	   if (Input.GetKeyDown(KeyCode.UpArrow))
         MoveTo(_upPosition);
      if (Input.GetKeyDown(KeyCode.Q))
         MoveTo(_leftUpPosition);
      if (Input.GetKeyDown(KeyCode.E))
         MoveTo(_rightUpPosition);
      if (Input.GetKeyDown(KeyCode.Z))
         MoveTo(_leftDownPosition);
      if (Input.GetKeyDown(KeyCode.C))
         MoveTo(_rightDownPosition);
	}

   private void MoveTo(Vector2 targetPosition)
   {
      Debug.Log("source" + _lastDancerTilePosition);
      Debug.Log("target" + targetPosition);
      string animationType = GetAnimation(_lastDancerTilePosition, targetPosition);
      Debug.Log(animationType);
      var animation = Instantiate(Resources.Load(@"Prefabs/DancerMoves/" + animationType)) as GameObject;
      animation.transform.position = _lastDancerTilePosition;
      animation.GetComponent<DancerMoveInstance>().InitialPosition = _lastDancerTilePosition;
      animation.GetComponent<DancerMoveInstance>().TargetPosition = targetPosition;
      if (_lastAnimation != null)
      {
         Destroy(_lastAnimation.gameObject);
      }
      _lastAnimation = animation;
      _lastDancerTilePosition = targetPosition;
   }

   private string GetAnimation(Vector2 sourcePosition, Vector2 targetPosition)
   {
      Vector2 direction = (targetPosition - sourcePosition).normalized;
      if (targetPosition == sourcePosition)
         direction = (_centerPosition - targetPosition).normalized;
      double degrees = Math.Acos(-direction.x) * (-direction.y > 0 ? 1 : -1) + Mathf.PI;

      //return "RightDown";
      Debug.Log((degrees + " degress.\n"));
      if (22.5/ 180f * Mathf.PI <= degrees && degrees <= 67.5/ 180f * Mathf.PI)
         return "RightUp";
      if (67.5/ 180f * Mathf.PI <= degrees && degrees <= 112.5/ 180f * Mathf.PI)
         return "Up";
      if (112.5/ 180f * Mathf.PI <= degrees && degrees <= 157.5/ 180f * Mathf.PI)
         return "LeftUp";
      if (157.5/ 180f * Mathf.PI <= degrees && degrees <= 202.5/ 180f * Mathf.PI)
         return "Left";
      if (202.5/ 180f * Mathf.PI <= degrees && degrees <= 247.5/ 180f * Mathf.PI)
         return "LeftDown";
      if (247.5/ 180f * Mathf.PI <= degrees && degrees <= 292.5/ 180f * Mathf.PI)
         return "Down";
      if (292.5/ 180f * Mathf.PI <= degrees && degrees <= 337.5/ 180f * Mathf.PI)
         return "RightDown";
      if (337.5/ 360f * Mathf.PI <= degrees && degrees <= 22.5 * Mathf.PI)
         return "Right";
      return "Right";
   }
}
