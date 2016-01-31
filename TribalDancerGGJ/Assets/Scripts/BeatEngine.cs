using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

public class BeatEngine : MonoBehaviour
{

    public static BeatEngine instance;
   [SerializeField]
   AudioSource theSong;
   [SerializeField]
   Image theFrame;
   [Range(1f, 1999f)]
   [SerializeField]
   float offsetMillsec = 400f; //number must be between 0 and 2000
   [SerializeField]
   Image preparationImage;
   [SerializeField]
   Sprite pic1;
   [SerializeField]
   Sprite pic2;
   [SerializeField]
   Sprite pic3;
   [SerializeField]
   Sprite pic4;
   [SerializeField]
   GameObject symbol0;
   [SerializeField]
   GameObject symbol1;
   [SerializeField]
   GameObject symbol2;
   [SerializeField]
   GameObject symbol3;
   [SerializeField]
   GameObject symbol4;
   [SerializeField]
   GameObject symbol5;
   [SerializeField]
   GameObject symbol6;
   [SerializeField]
   GameObject symbol7;

   private GameObject currentlyVisible;

   bool startDancing;
   int realTimer;
   float microTimer;
   bool resetMicro;
   bool microStep1;
   bool microStep2;
   bool microStep3;

   DateTime startTime;
   DateTime microStartTime;
   TimeSpan timeSpan;

   float timePerBeat;
   float goodHitx2;
   float perfectHit;
   float badHit;

   bool hasMatched;

   private int[] beatSequence1 = new int[4];//###TO be edited
   private int beatCounter;

   [SerializeField]
   Text theScore;
   private int currPoints;
   public bool instructMode;
   private Vector3 hmTop;
   private Vector3 hmBot;

   private int currDifficulty;
   [SerializeField]
   List<int> symbolsPerDifficulty = new List<int>();
   private int seqPlayed;

   void Awake() {
       if (instance == null) {
           instance = this;
       } else if (instance != this) {
           Destroy(gameObject);
       }
   }

   void Start()
   {
      currDifficulty = 0;
      seqPlayed = 0;
      realTimer = 0;
      microTimer = 0;
      symbol0.SetActive(false);
      symbol1.SetActive(false);
      symbol2.SetActive(false);
      symbol3.SetActive(false);
      symbol4.SetActive(false);
      symbol5.SetActive(false);
      symbol6.SetActive(false);
      symbol7.SetActive(false);
      for (int i = 0; i < 4; i++)
      { //initial sequence of 4 individual buttons
         beatSequence1[i] = 0;
      }
      beatCounter = 0;
      theSong.Play();
      setAccDifficulty(1);
      startDancing = false;
      hasMatched = false;
      resetMicro = false;
      microStep1 = false;
      microStep2 = false;
      microStep3 = false;
      currPoints = 0;
      preparationImage.sprite = pic1;
      startTime = DateTime.Now;
   }

   void Update()
   {
      if (!theSong.isPlaying)
      { //if the song is finished - start it again, reset realTimer, and prepare to reset microTimer
         theSong.Play();
         if (startDancing) startTime = DateTime.Now; //realTimer will not be reset the first time the song finishes
         resetMicro = true;
      }
      realTimer = (DateTime.Now - startTime).Seconds * 1000 + (DateTime.Now - startTime).Milliseconds; //calculate realTimer

      if (!startDancing)
      { //before the game starts

         if (realTimer > offsetMillsec + 2000f)
         { //if the song has played once and the time of the offset has passed begin playing
            startDancing = true;
            danceRequest(beatSequence1[beatCounter]);
            instructMode = true;
            preparationImage.enabled = false;
            microStartTime = DateTime.Now;
            resetMicro = false;
         }
         else if (realTimer > offsetMillsec + 1400f)
         { //change sign
             preparationImage.sprite = pic4;
         }
         else if (realTimer > offsetMillsec + 800f)
         { //change sign
             preparationImage.sprite = pic3;
         }
         else if (realTimer > offsetMillsec + 200f)
         { //change sign
            preparationImage.sprite = pic2;
         }

      }
      else
      {

         if (resetMicro && realTimer > offsetMillsec)
         { //if the song has reached the point for the new button check - reset the microTimer,
            //check if no matched on the fourth part to display miss, reset all steps
            //and change to next request
            microStartTime = DateTime.Now;
            microStep1 = false;
            microStep2 = false;
            microStep3 = false;
            resetMicro = false;
            if (!instructMode && !hasMatched)
            {
               SpawnIndicator("Miss");
               //currPoints -= 1;
               if (currPoints < 0) currPoints = 0;
               theScore.text = "Score : " + currPoints;
            }
            hasMatched = false;
            instructMode = !instructMode;
            if (instructMode)
            {
               seqPlayed++;
               currDifficulty = seqPlayed / 4;
               nextSeq();
               danceRequest(beatSequence1[beatCounter]);
               Debug.Log("seqPlayed " + seqPlayed + " Curr Diff: " + currDifficulty);
            } else {
                if (currentlyVisible) currentlyVisible.SetActive(false);
                currentlyVisible = null;
            }
            beatCounter = 0;
         }

         microTimer = (DateTime.Now - microStartTime).Seconds * 1000 + (DateTime.Now - microStartTime).Milliseconds; //calculate microtimer

         if (microTimer % 500 >= 150 && microTimer % 500 < 250)
         { //light up the frame during the perfect time
            theFrame.color = new Color(theFrame.color.r, theFrame.color.b, theFrame.color.b, .8f);
         }
         else
         {
            theFrame.color = new Color(theFrame.color.r, theFrame.color.b, theFrame.color.b, 1f);
         }

         if (instructMode)
         {
            if (microTimer > 3 * timePerBeat && !microStep3)
            {
               microStep3 = true;
               danceRequest(beatSequence1[beatCounter]);
               beatCounter = 3;
            }
            else if (microTimer > 2 * timePerBeat && !microStep2)
            {
               microStep2 = true;
               danceRequest(beatSequence1[beatCounter]);
               beatCounter = 2;
            }
            else if (microTimer > timePerBeat && !microStep1)
            {
               microStep1 = true;
               danceRequest(beatSequence1[beatCounter]);
               beatCounter = 1;
            }

         }
         else
         {
            if (Input.GetButtonDown("DPLeft") && beatSequence1[beatCounter] == 0 && !hasMatched)
            { //check if L is pressed, is correct & not double counted
                hasMatched = true;
                checkAccuracy(microTimer);
            }
            if (Input.GetButtonDown("DPRight") && beatSequence1[beatCounter] == 1 && !hasMatched)
            { //check if R is pressed, is correct & not double counted
                hasMatched = true;
                checkAccuracy(microTimer);
            }
            if (Input.GetButtonDown("DPUp") && beatSequence1[beatCounter] == 2 && !hasMatched)
            { //check if U is pressed, is correct & not double counted
                hasMatched = true;
                checkAccuracy(microTimer);
            }
            if (Input.GetButtonDown("DPDown") && beatSequence1[beatCounter] == 3 && !hasMatched)
            { //check if D is pressed, is correct & not double counted
                hasMatched = true;
                checkAccuracy(microTimer);
            }
            if (Input.GetButtonDown("DPX") && beatSequence1[beatCounter] == 4 && !hasMatched)
            { //check if X is pressed, is correct & not double counted
                hasMatched = true;
                checkAccuracy(microTimer);
            }
            if (Input.GetButtonDown("DPO") && beatSequence1[beatCounter] == 5 && !hasMatched)
            { //check if O is pressed, is correct & not double counted
                hasMatched = true;
                checkAccuracy(microTimer);
            }
            if (Input.GetButtonDown("DPS") && beatSequence1[beatCounter] == 6 && !hasMatched)
            { //check if S is pressed, is correct & not double counted
                hasMatched = true;
                checkAccuracy(microTimer);
            }
            if (Input.GetButtonDown("DPT") && beatSequence1[beatCounter] == 7 && !hasMatched)
            { //check if T is pressed, is correct & not double counted
                hasMatched = true;
                checkAccuracy(microTimer);
            }

            if (microTimer > 3 * timePerBeat && !microStep3)
            { //if the third step has not passed and the timer has just passed 1500msc
               microStep3 = true;
               if (!hasMatched)
               {
                  SpawnIndicator("Miss");
                  //currPoints -= 1;
                  if (currPoints < 0) currPoints = 0;
                  theScore.text = "Score : " + currPoints;
               }
               beatCounter = 3;
               hasMatched = false;
            }
            else if (microTimer > 2 * timePerBeat && !microStep2)
            { //if the second step has not passed and the timer has just passed 1000msc
               microStep2 = true;
               if (!hasMatched)
               {

                  SpawnIndicator("Miss");
                  //currPoints -= 1;
                  if (currPoints < 0) currPoints = 0;
                  theScore.text = "Score : " + currPoints;
               }
               beatCounter = 2;
               hasMatched = false;
            }
            else if (microTimer > timePerBeat && !microStep1)
            { //if the first step has not passed and the timer has just passed 500msc
               microStep1 = true;
               if (!hasMatched)
               {
                  SpawnIndicator("Miss");
                  //currPoints -= 1;
                  if (currPoints < 0) currPoints = 0;
                  theScore.text = "Score : " + currPoints;
               }
               beatCounter = 1;
               hasMatched = false;
            }
         }

      }
   }

   void checkAccuracy(float milisecs)
   { //checks hit accuracy and updates score
       milisecs = microTimer % 500;
       if (milisecs < goodHitx2)
      {
         SpawnIndicator("Good");
         currPoints += 3;
         theScore.text = "Score : " + currPoints;
      } else if (milisecs < goodHitx2 + perfectHit)
      {
         SpawnIndicator("Perfect");
         currPoints += 5;
         theScore.text = "Score : " + currPoints;
      } else if (milisecs < goodHitx2 * 2 + perfectHit)
      {
         SpawnIndicator("Good");
         currPoints += 3;
         theScore.text = "Score : " + currPoints;
      } else
      {
         SpawnIndicator("Bad");
         currPoints += 1;
         theScore.text = "Score : " + currPoints;
      }
   }

   private static void SpawnIndicator(string prefabName)
   {
      var indicator = Instantiate(Resources.Load(@"Prefabs/" + prefabName)) as GameObject;
      indicator.transform.position = new Vector2(-0.44f, -3.21f);
   }

   void nextSeq()
   { //change to next request; if four beats have passed, generate 4 more
      int maxSigns = 0;
      if (currDifficulty <= symbolsPerDifficulty.Count)
      {
         maxSigns = symbolsPerDifficulty[currDifficulty];
      }
      else
      {
         currDifficulty = symbolsPerDifficulty.Count - 1;
      }
      for (int i = 0; i < 4; i++)
      { //initial sequence of 4 individual buttons
         beatSequence1[i] = UnityEngine.Random.Range(0, maxSigns);
      }
      beatCounter = 0;
   }

   private void danceRequest(int numb)
   {  switch (numb) {
           case 0: symbol0.SetActive(true); StartCoroutine(lightUpTile(symbol0)); break;
           case 1: symbol1.SetActive(true); StartCoroutine(lightUpTile(symbol1)); break;
           case 2: symbol2.SetActive(true); StartCoroutine(lightUpTile(symbol2)); break;
           case 3: symbol3.SetActive(true); StartCoroutine(lightUpTile(symbol3)); break;
           case 4: symbol4.SetActive(true); StartCoroutine(lightUpTile(symbol4)); break;
           case 5: symbol5.SetActive(true); StartCoroutine(lightUpTile(symbol5)); break;
           case 6: symbol6.SetActive(true); StartCoroutine(lightUpTile(symbol6)); break;
           case 7: symbol7.SetActive(true); StartCoroutine(lightUpTile(symbol7)); break;
           default: break;
      }
   }

   private IEnumerator lightUpTile(GameObject lightPic) {
       yield return new WaitForSeconds(0.19f);
       lightPic.SetActive(false);
   }

   void setAccDifficulty(int diff)
   { //set values based on difficulty, !NB The sequence is goodHitx2, perfectHit, goodHitx2, badHit. They all equal timePerBeat
      switch (diff)
      {
         default:
            goodHitx2 = 100f;
            perfectHit = 200f;
            timePerBeat = 500f;
            break;
      }
   }
}
