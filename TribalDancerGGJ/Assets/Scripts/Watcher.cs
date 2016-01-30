using System;
using UnityEngine;
using System.Collections;

public class Watcher : MonoBehaviour
{

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

   public void Shout(string message)
   {
      var dialogBubble = GetComponent<DialogBubble>();
      string modifiedMessage = message.Length <= 30 ? message : message.Substring(0, 30) + ("...");
      dialogBubble.ShowBubble(dialogBubble, modifiedMessage);
   }
}
