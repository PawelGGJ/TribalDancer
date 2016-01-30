using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour {

    public static GameManagement instance;
    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }
    public void LoadScene(string name) {
        SceneManager.LoadScene(name);
    }
    public void QuitGame() {
        Application.Quit();
    }
}
