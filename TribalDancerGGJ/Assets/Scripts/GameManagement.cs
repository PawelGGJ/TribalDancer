using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour {

    public static GameManagement instance;
    private string channelName = "";

    void Awake() {
        if (instance == null) {
            DontDestroyOnLoad(transform.gameObject);
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    public void setChannelName(string theName) {
        channelName = theName.ToLower();
    }

    public string getChannelName() {
        return channelName;
    }

    public void LoadScene(string name) {
        SceneManager.LoadScene(name);
    }
    public void QuitGame() {
        Application.Quit();
    }
}
