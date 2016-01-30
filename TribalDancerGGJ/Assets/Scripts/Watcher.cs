using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Watcher : MonoBehaviour
{
   private readonly IDictionary<SpeechType, Curse> _cursesToSpeechTypes = new Dictionary<SpeechType, Curse>
   {
      {SpeechType.Blood, Curse.Blood},
      {SpeechType.Speed, Curse.Speed},
      {SpeechType.Lava, Curse.Lava},
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
      if (messageLower.Contains("b"))
         return SpeechType.Blood;
      if (messageLower.Contains("s"))
         return SpeechType.Speed;
      if (messageLower.Contains("l"))
         return SpeechType.Lava;
      if (messageLower.Contains("boo"))
         return SpeechType.Stone;

      if (messageLower.Contains("nice"))
         return SpeechType.Flower;
      if (messageLower.Contains("good"))
         return SpeechType.Flower;

      return SpeechType.Undefined;
   }
}