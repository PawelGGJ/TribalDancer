﻿using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Watcher : MonoBehaviour
{
   private readonly IDictionary<SpeechType, Curse> _cursesToSpeechTypes = new Dictionary<SpeechType, Curse>
   {
      {SpeechType.Blood, Curse.Blood},
      {SpeechType.Flip, Curse.Flip},
      {SpeechType.Quake, Curse.Quake},
   };

   public string Name;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

   public void Shout(string message, int limit = 40)
   {
      var dialogBubble = GetComponent<DialogBubble>();

      SpeechType speechType = DetectSpeechType(message);

      var voteManager = FindObjectOfType<VoteManager>();
      bool isVoting = voteManager.IsVoting();
      if (isVoting && _cursesToSpeechTypes.ContainsKey(speechType))
         voteManager.Vote(_cursesToSpeechTypes[speechType]);

      string trimmedMessage = message.Length <= limit ? message : message.Substring(0, limit) + ("...");
      dialogBubble.ShowBubble(dialogBubble, trimmedMessage, isVoting ? speechType : SpeechType.Undefined);
   }

   private SpeechType DetectSpeechType(string message)
   {
      string messageLower = message.ToLower();

      if (messageLower.Contains("blood"))
         return SpeechType.Blood;
      if (messageLower.Contains("quake"))
         return SpeechType.Quake;
      if (messageLower.Contains("chaos"))
         return SpeechType.Flip;

      return SpeechType.Undefined;
   }
}