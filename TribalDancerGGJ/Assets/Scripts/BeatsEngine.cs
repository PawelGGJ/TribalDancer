using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeatsEngine : MonoBehaviour {

    [SerializeField] static int totalBeats = 4;
    [SerializeField] List<int> comboMarks = new List<int>();
    [SerializeField] List<int> ptsGoodGained = new List<int>();
    [SerializeField] List<int> ptsPerfectGained = new List<int>();

    private int currCombo = 0;
    private int maxCombo = 0;
    private int currComboMark = 0;
    private int currPoints = 0;

    private int secondStep1 = 0;
    private int secondStep2 = 0;

    private int numbGameBtns = 8;

    private int[] beatSequence1 = new int[totalBeats];
    private int[] beatSequence2 = new int[totalBeats]; //if bS2[i] == bs2[i] set bS2[i] to 0; skip all bS2 that are 0

    void Start() {
        for (int i = 0; i < totalBeats; i++) { //initial sequence of 4 individual buttons
            beatSequence1[i] = generateRand(numbGameBtns);
        }
    }

    void Update() {
        //call 5. & 6.
    }

    private int generateRand(int numb) { //randomly chooses button   //maybe turn it into a coroutine?
        return Random.Range(0, numb * 2) % numb;
    }

    private bool compareInput(int beat, int keyPressed) { //if keyPressed is the same as the beat numb return true, else false
        return true;
    }

    private void checkHit(int newHit) { //check current combo
        if (newHit == 0) {
            if (currCombo > comboMarks[0]) {
                //call combobreak
                currComboMark = 0;
            }
            currCombo = 0;
        } else {
            currCombo += 1;
            if (currCombo > maxCombo) maxCombo = currCombo;
            if (newHit == 1) {
                currPoints += ptsGoodGained[currComboMark];
            } else if (newHit == 2) {
                currPoints += ptsPerfectGained[currComboMark];
            } else {
                Debug.Log("Points calculation error");
            }
        }
    }

    private void generateInstruction() {
        if (currComboMark > 0) {
            secondStep1 = generateRand(totalBeats);
            if (currComboMark == 2) {
                do secondStep2 = generateRand(totalBeats);
                while (secondStep2 == secondStep1);
            }
        }
        for (int i = 0; i < totalBeats; i++) {
            beatSequence1[i] = generateRand(numbGameBtns);
            if (currComboMark == 1 && i == secondStep1) {
                beatSequence2[i] = generateRand(numbGameBtns);
                if (beatSequence1[i] == beatSequence2[i]) {
                    beatSequence2[i] = numbGameBtns + 1;
                }
            } else if (currComboMark == 2 && (i == secondStep1 || i == secondStep2)) {
                beatSequence2[i] = generateRand(numbGameBtns);
                if (beatSequence1[i] == beatSequence2[i]) {
                    beatSequence2[i] = numbGameBtns + 1;
                }
            } else if (currComboMark == 3 && i != secondStep1) {
                beatSequence2[i] = generateRand(numbGameBtns);
                if (beatSequence1[i] == beatSequence2[i]) {
                    beatSequence2[i] = numbGameBtns + 1;
                }
            } else {
                beatSequence2[i] = generateRand(numbGameBtns);
                if (beatSequence1[i] == beatSequence2[i]) {
                    beatSequence2[i] = numbGameBtns + 1;
                }
            }
        }
    }
    //function
    //1.check current score to determine difficulty
    //2.generate notes sequence based on difficulty

    //function
    //3.light up a note with a switch statement

    //function
    //4.call 3. based on 2.

    //function
    //5.bool to check if a beat has passed: numb-deltatime, if < 0 reset and true; light up edge of screen on exact beat

    //function
    //6.receive input - if within frame of ??? debug log state correct, else good, else miss; call 7.; use 5. maybe

    //function
    //7.calculate points


}
