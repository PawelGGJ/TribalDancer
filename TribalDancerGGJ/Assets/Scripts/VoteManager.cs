using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public enum Curse
{
   Blood,
   Flip,
   Quake
}

public class VoteManager : MonoBehaviour
{
   private bool _voting = false;
   private DateTime _beginning;
   private int _passedTrials = 0;
   private DateTime _timeOfNextTrialRequest;
   private DateTime _timeOfNextTrialResolution;

   private static IDictionary<Curse, int> _curseVotes = CreateEmptyCurseVotes();

   private static Dictionary<Curse, int> CreateEmptyCurseVotes()
   {
      return new Dictionary<Curse, int>
      {
         {Curse.Blood, 0},
         {Curse.Flip, 0},
         {Curse.Quake, 0},
      };
   }

   public bool IsVoting()
   {
      return _voting;
   }

   public void StartVoting()
   {
      var choose = Instantiate(Resources.Load(@"Prefabs/Choose")) as GameObject;
      choose.transform.position = new Vector3(-0.52f, -0.49f, 0f);

      // reset counters
      _curseVotes = CreateEmptyCurseVotes();
      
      //Shout("***TWITCH, CHOOSE HIS FATE: BLOOD, LAVA OR SPEED***", SpeechType.GodVotingRequest);
      _voting = true;
   }

   public void EndVoting()
   {
      int topVoteCount = _curseVotes.Values.Max();
      Curse winner = _curseVotes.OrderBy(kvp => Guid.NewGuid()).First(kvp => kvp.Value == topVoteCount).Key;

      var choose = Instantiate(Resources.Load(@"Prefabs/" + winner)) as GameObject;
      if(choose != null) choose.transform.position = new Vector3(-0.52f, -0.49f, 0f);

      //Shout(string.Format("***YOU CHOSE {0}***", winner.ToString().ToUpper()), SpeechType.GodVotingResolved);

      _passedTrials += 1;
      int secondsLast = (int)((DateTime.UtcNow - _beginning).TotalSeconds);
      FindObjectOfType<CurseManager>().PrepareCurse(winner, secondsLast);
      _voting = false;

      _timeOfNextTrialRequest = DateTime.UtcNow.AddSeconds(Mathf.Max((int)(20 - _passedTrials*2), 12));
      _timeOfNextTrialResolution = _timeOfNextTrialRequest.AddSeconds(15);
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
	   _beginning = DateTime.UtcNow;

      _timeOfNextTrialRequest = DateTime.UtcNow.AddSeconds(15);
      _timeOfNextTrialResolution = _timeOfNextTrialRequest.AddSeconds(10);
	}
	
	// Update is called once per frame
	void Update () {

	   if (DateTime.UtcNow > _timeOfNextTrialRequest && !_voting)
	   {
	      StartVoting();
	   }
	   if (DateTime.UtcNow > _timeOfNextTrialResolution)
	   {
	      EndVoting();
	   }

	  // if (Input.GetKeyDown(KeyCode.Space))
	  // {
	  //    Debug.Log(GetComponent<Watcher>() != null);
     //
     //    if (_voting)
     //    {
     //       EndVoting();
     //    }
     //    else
     //    {
     //       StartVoting();
     //    }
	  // }
	      
	}
}
