using System;
using Assets.Scripts;
using UnityEngine;
using System.Collections;

public class DancerController : MonoBehaviour
{
    [SerializeField] GameObject luP;
    [SerializeField] GameObject uP;
    [SerializeField] GameObject ruP;
    [SerializeField] GameObject rP;
    [SerializeField] GameObject rdP;
    [SerializeField] GameObject dP;
    [SerializeField] GameObject ldP;
    [SerializeField] GameObject lP;
    [SerializeField] GameObject cP;
    [SerializeField] GameObject standingChar;

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
   private bool hasMoved;

	// Use this for initialization
	void Start ()
	{
        standingChar.SetActive(true);
        hasMoved = false;

        _leftUpPosition = luP.transform.position;
      _upPosition = uP.transform.position;
      _rightUpPosition = ruP.transform.position;
      _rightPosition = rP.transform.position;
      _rightDownPosition = rdP.transform.position;
      _downPosition = dP.transform.position;
      _leftDownPosition = ldP.transform.position;
      _leftPosition = lP.transform.position;
      _centerPosition = cP.transform.position;

	   _lastDancerTilePosition = _centerPosition;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (!BeatEngine.instance.instructMode) {
            if (Input.GetButtonDown("DPDown") || Input.GetKeyDown(KeyCode.DownArrow)) {
                MoveTo(_downPosition);
                hasMoved = true;
                if (standingChar) standingChar.SetActive(false);
            }
            if (Input.GetButtonDown("DPLeft") || Input.GetKeyDown(KeyCode.LeftArrow)) {
                MoveTo(_leftPosition);
                hasMoved = true;
                if (standingChar) standingChar.SetActive(false);
            }
            if (Input.GetButtonDown("DPRight") || Input.GetKeyDown(KeyCode.RightArrow)) { 
                MoveTo(_rightPosition);
                hasMoved = true;
                if (standingChar) standingChar.SetActive(false);
            }
            if (Input.GetButtonDown("DPUp") || Input.GetKeyDown(KeyCode.UpArrow)) {
                MoveTo(_upPosition);
                hasMoved = true;
                if (standingChar) standingChar.SetActive(false);
            }
            if (Input.GetButtonDown("DPX") || Input.GetKeyDown(KeyCode.Q)) {
                MoveTo(_leftUpPosition);
                hasMoved = true;
                if (standingChar) standingChar.SetActive(false);
            }
            if (Input.GetButtonDown("DPO") || Input.GetKeyDown(KeyCode.E)) {
                MoveTo(_rightUpPosition);
                hasMoved = true;
                if (standingChar) standingChar.SetActive(false);
            }
            if (Input.GetButtonDown("DPT") || Input.GetKeyDown(KeyCode.Z)) {
                MoveTo(_leftDownPosition);
                hasMoved = true;
                if (standingChar) standingChar.SetActive(false);
            }
            if (Input.GetButtonDown("DPS") || Input.GetKeyDown(KeyCode.C)) {
                MoveTo(_rightDownPosition);
                hasMoved = true;
                if (standingChar) standingChar.SetActive(false);
            }
        } else if (hasMoved) {
            StartCoroutine(destroyClone());
            MoveTo(_centerPosition);
            hasMoved = false;
            if (standingChar) {
                standingChar.SetActive(true);
            }
        }
	}
    private IEnumerator destroyClone() {
        yield return new WaitForSeconds(0.05f);
        Destroy(_lastAnimation.gameObject);
    }

   private void MoveTo(Vector2 targetPosition)
   {
      //Debug.Log("source" + _lastDancerTilePosition);
      //Debug.Log("target" + targetPosition);
      string animationType = GetAnimation(_lastDancerTilePosition, targetPosition);
      //Debug.Log(animationType);
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
      //Debug.Log((degrees + " degress.\n"));
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
