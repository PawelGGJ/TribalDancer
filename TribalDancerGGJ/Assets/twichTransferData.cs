using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class twichTransferData : MonoBehaviour {

    [SerializeField]
    private GameObject inputUI;
    public string channelName;

    public void saveData() {
        channelName = inputUI.GetComponentInChildren<InputField>().text;
        if (channelName != "" && channelName != "Enter Twichchannel...") {
            GameManagement.instance.setChannelName(channelName);
            GameManagement.instance.LoadScene("GameLevel");
        }
    }
}
