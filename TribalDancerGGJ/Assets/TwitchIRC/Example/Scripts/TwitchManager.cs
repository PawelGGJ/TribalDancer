using System;
using System.Collections.Generic;
using System.Linq;
using Irc;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = System.Random;

public class TwitchManager : MonoBehaviour
{
   public string UsernameText;
   public string TokenText;
   public string ChannelText;

   private List<Watcher> _watchers; 

   void Start()
   {
      //Subscribe for events
      TwitchIrc.Instance.OnChannelMessage += OnChannelMessage;
      TwitchIrc.Instance.OnUserLeft += OnUserLeft;
      TwitchIrc.Instance.OnUserJoined += OnUserJoined;
      TwitchIrc.Instance.OnServerMessage += OnServerMessage;
      TwitchIrc.Instance.OnExceptionThrown += OnExceptionThrown;
      Connect();
      _watchers = FindObjectsOfType<Watcher>().ToList();
   }

   public void Connect()
   {
      TwitchIrc.Instance.Username = UsernameText;
      TwitchIrc.Instance.OauthToken = TokenText;
      TwitchIrc.Instance.Channel = ChannelText;

      TwitchIrc.Instance.Connect();
   }

   //Open URL
   public void GoUrl(string url)
   {
      Application.OpenURL(url);
   }

   //Receive message from server
   void OnServerMessage(string message)
   {
      Debug.Log(message);
   }

   //Receive username that has been left from channel 
   void OnChannelMessage(ChannelMessageEventArgs channelMessageArgs)
   {
      Debug.Log("MESSAGE: " + channelMessageArgs.From + ": " + channelMessageArgs.Message);

      var existingWatcher = _watchers.FirstOrDefault(w => w.Name == channelMessageArgs.From);
      if (existingWatcher == null)
      {
         Watcher anyFreeWatcher = _watchers.FirstOrDefault(w => String.IsNullOrEmpty(w.Name));
         if (anyFreeWatcher != null)
         {
            existingWatcher = anyFreeWatcher;
            existingWatcher.Name = channelMessageArgs.From;
         }
      }
      if (existingWatcher != null)
      {
         existingWatcher.Shout(channelMessageArgs.Message);
      }
   }

   //Get the name of the user who joined to channel 
   void OnUserJoined(UserJoinedEventArgs userJoinedArgs)
   {
      Debug.Log("USER JOINED: " + userJoinedArgs.User);
   }


   //Get the name of the user who left the channel.
   void OnUserLeft(UserLeftEventArgs userLeftArgs)
   {
      Debug.Log("USER JOINED: " + userLeftArgs.User);
   }

   //Receive exeption if something goes wrong
   private void OnExceptionThrown(Exception exeption)
   {
      Debug.Log(exeption);
   }
}