using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public enum Curse
{
   Blood,
   Speed,
   Lava
}

public class VoteManager : MonoBehaviour
{
   private bool _voting = false;

   private readonly IDictionary<Curse, int> _curseVotes = new Dictionary<Curse, int>
   {
      {Curse.Blood, 0},
      {Curse.Speed, 0},
      {Curse.Lava, 0},
   };

   public bool IsVoting()
   {
      return _voting;
   }

   public void StartVoting()
   {
      // reset counters
      _curseVotes.ToList().ForEach(kvp => _curseVotes[kvp.Key] = 0);

      Shout("***TWITCH, CHOOSE HIS FATE: BLOOD, LAVA OR SPEED***", SpeechType.GodVotingRequest);
      _voting = true;
   }

   public void EndVoting()
   {
      int topVoteCount = _curseVotes.Values.Max();
      Curse winner = _curseVotes.First(kvp => kvp.Value == topVoteCount).Key;
      Shout(string.Format("***YOU CHOSE {0}***", winner.ToString().ToUpper()), SpeechType.GodVotingResolved);
      _voting = false;
   }

   public void Shout(string message, SpeechType speechType)
   {
      var dialogBubble = GetComponent<DialogBubble>();

      dialogBubble.ShowBubble(dialogBubble, message, speechType);
   }

   public void Vote(Curse curse)
   {
      _curseVotes[curse] += 1;
   }

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
	   if (Input.GetKeyDown(KeyCode.Space))
	   {
	      Debug.Log("SPACE!!!!!!!!");
	      Debug.Log(GetComponent<Watcher>() != null);

         if (_voting)
         {
            EndVoting();
         }
         else
         {
            StartVoting();
         }
	   }
	      
	}
}
