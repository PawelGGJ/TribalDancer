using System;
using UnityEngine;
using System.Collections;

public class CurseManager : MonoBehaviour
{

   private DateTime _timeToStartCurse;
   private Action _curseAction;

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update ()
	{
	   if (_curseAction != null && DateTime.UtcNow > _timeToStartCurse)
	   {
	      _curseAction();
	      _curseAction = null;
	   }
	}

   public void PrepareCurse(Curse curse, int gameSeconds)
   {
      
      if (curse == Curse.Quake)
      {
         _timeToStartCurse = DateTime.UtcNow.AddMilliseconds(3000);
         _curseAction = () => FindObjectOfType<CameraShake>().StartShaking(gameSeconds);
      }

      if (curse == Curse.Blood)
      {
         _timeToStartCurse = DateTime.UtcNow.AddMilliseconds(3000);
         _curseAction = () => FindObjectOfType<FogGenerator>().GenerateFogFor(gameSeconds / 12);
      }
      GameObject.Find("Rivers of Lava").SetActive(true);
  
   }
}
