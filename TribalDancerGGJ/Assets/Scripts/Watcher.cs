using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Watcher : MonoBehaviour
{
   private IDictionary<SpeechType, Curse> _cursesToSpeechTypes = new Dictionary<SpeechType, Curse>
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

   public void Shout(string message, int limit = 30)
   {
      var dialogBubble = GetComponent<DialogBubble>();

      SpeechType speechType = DetectSpeechType(message);

      string trimmedMessage = message.Length <= limit ? message : message.Substring(0, limit) + ("...");
      dialogBubble.ShowBubble(dialogBubble, trimmedMessage, speechType);
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