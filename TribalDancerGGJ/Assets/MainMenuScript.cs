using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameManagement.instance.QuitGame();
        }
	}
}
