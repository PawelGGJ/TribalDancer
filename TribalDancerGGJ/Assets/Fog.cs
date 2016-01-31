using System;
using JetBrains.Annotations;
using UnityEngine;
using System.Collections;
using Random = System.Random;

public class Fog : MonoBehaviour
{

   public DateTime DeathTime;
   private float _angle = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	   if (DateTime.UtcNow > DeathTime)
	   {
	      Destroy(gameObject);
	   }
	   _angle += (float)new Random().NextDouble()*.2f;
	   var direction = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle / 10)).normalized;
      transform.Translate(direction*Time.deltaTime);
	}
}
