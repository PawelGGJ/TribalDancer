using System;
using UnityEngine;
using Random = System.Random;

public class FogGenerator : MonoBehaviour
{
   void Start()
   {

   }

   public void GenerateFogFor(int seconds)
   {
      for (int i = 1; i <= 6; i++)
      {
         var fog = Instantiate(Resources.Load(@"Prefabs/Bloodmist_" + i)) as GameObject;
         fog.transform.position = new Vector3(-2.5f + (float)new Random().NextDouble() - 0.5f, 
            -6f + (float)new Random().NextDouble(), 0f);
         fog.GetComponent<SpriteRenderer>().sortingOrder = 3;
         fog.GetComponent<Fog>().DeathTime = DateTime.UtcNow.AddSeconds(seconds + new Random().Next(5));
      }
   }

   void Update()
   {
      if(Input.GetKeyDown(KeyCode.DownArrow))
         GenerateFogFor(6);
   }

}