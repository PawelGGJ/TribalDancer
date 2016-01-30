using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeatsEngine : MonoBehaviour {

    private List<int> beatSequence = new List<int>();

	void Start () {
	    //call 2.
	}
	
	void Update () {
        //call 5. & 6.
	}

    int generateRand() {
        return (Random.Range(0, 16)%8 + 1);
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
