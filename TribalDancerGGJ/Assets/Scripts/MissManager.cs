using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace Assets.Scripts
{
   public class MissManager : MonoBehaviour
   {
      private DateTime _lastMiss;

      public void Miss()
      {
         if ((DateTime.UtcNow - _lastMiss).TotalSeconds < 2)
            return;
         _lastMiss = DateTime.UtcNow;
         var watchers = FindObjectsOfType<Watcher>();
         int watchersCount = watchers.Length;

         if (watchersCount < 1)
            GameOver();

         int randomIndex = new Random().Next(watchersCount);
         Watcher randomWatcher = watchers[randomIndex];

         var volcano = Instantiate(Resources.Load(@"Prefabs/Volcano")) as GameObject;
         volcano.transform.position = randomWatcher.transform.position - new Vector3(0f, -.5f, 0f);
         volcano.GetComponent<Volcano>().Destroyee = randomWatcher.gameObject;
         volcano.GetComponent<Volcano>().DestructionTime = DateTime.UtcNow.AddMilliseconds(200);

         FindObjectOfType<TwitchManager>().Watchers.Remove(randomWatcher);
         
      }

      private void GameOver()
      {
         var volcano = Instantiate(Resources.Load(@"Prefabs/Volcano")) as GameObject;
         volcano.transform.position = new Vector3(0f, -2.0f, 0f);
         volcano.transform.localScale += new Vector3(3f, 3f, 1f );
         StartCoroutine("WaitAndReload");
      }

      public IEnumerator WaitAndReload()
      {
         yield return new WaitForSeconds(1.0f);
         GameManagement.instance.LoadScene("MainMenu");
      }
   }
}