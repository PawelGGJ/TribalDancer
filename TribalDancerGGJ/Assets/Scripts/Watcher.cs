using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Watcher : MonoBehaviour
{
   private IDictionary<KeyWord, Curse> _cursesToKeywords = new Dictionary<KeyWord, Curse>
   {
      {KeyWord.Blood, Curse.Blood},
      {KeyWord.Speed, Curse.Speed},
      {KeyWord.Lava, Curse.Lava},
   };

   public string Name;
   private int _turns = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	   if (String.IsNullOrEmpty(Name))
	      return;
	   ++_turns;
      if(_turns % 900 == 0)
         Shout(Name);
	}

   public void Shout(string message, int limit = 30)
   {
      KeyWord keyWord = DetectKeyWord(message);
      if (_cursesToKeywords.ContainsKey(keyWord))
         FindObjectOfType<VoteManager>().Vote(_cursesToKeywords[keyWord]);

      var dialogBubble = GetComponent<DialogBubble>();

      string modifiedMessage = message.Length <= limit ? message : message.Substring(0, limit) + ("...");
      dialogBubble.ShowBubble(dialogBubble, modifiedMessage, keyWord);
   }

   private KeyWord DetectKeyWord(string message)
   {
      string messageLower = message.ToLower();
      if (messageLower.Contains("b"))
         return KeyWord.Blood;
      if (messageLower.Contains("s"))
         return KeyWord.Speed;
      if (messageLower.Contains("l"))
         return KeyWord.Lava;
      if (messageLower.Contains("boo"))
         return KeyWord.Stone;

      if (messageLower.Contains("nice"))
         return KeyWord.Flower;
      if (messageLower.Contains("good"))
         return KeyWord.Flower;

      return KeyWord.Undefined;
   }
}

public enum KeyWord
{
   Undefined,
   Blood,
   Speed,
   Lava,
   Stone,
   Flower
}
