﻿using System;
using UnityEngine;
using System.Collections;

public class HitIndicator : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float fadeSpeed = 1f;
   private DateTime _birth;

	// Use this for initialization
	void Start ()
	{
	   _birth = DateTime.UtcNow;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0f, Time.deltaTime * 5f * moveSpeed, 0f);
	   var color = GetComponent<SpriteRenderer>().color;
       color = new Color(color.r, color.g, color.b, color.a - Time.deltaTime * 1.2f * fadeSpeed);
	   GetComponent<SpriteRenderer>().color = color;
      if(DateTime.UtcNow.Subtract(_birth).TotalSeconds > 1)
         Destroy(gameObject);
	}
}
