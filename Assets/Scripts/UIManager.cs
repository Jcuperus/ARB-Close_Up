using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {
    private Button hostButton;
    private Button joinButton;
    private Button startButton;
    private GameObject winnerPanel;
    private Image IncorrectFeedbackPopupImage;
    private Image CorrectFeedbackPopupImage;
    private NetworkClientManager clientManager;
    private NetworkServerManager serverManager;

    void Start() {
        DontDestroyOnLoad(this.gameObject);

        clientManager = (NetworkClientManager)GetComponent(typeof(NetworkClientManager));
        serverManager = (NetworkServerManager)GetComponent(typeof(NetworkServerManager));

        hostButton = GameObject.Find("HostButton").GetComponent<Button>();
        hostButton.onClick.AddListener(handleHost);

        joinButton = GameObject.Find("JoinButton").GetComponent<Button>();
        joinButton.onClick.AddListener(handleJoin);
    }

    public void setStartButton(Button startButton) {
        this.startButton = startButton;
    }

    public void setCorrectFeedbackPopup(Image feedbackPopupImage) {
        this.CorrectFeedbackPopupImage = feedbackPopupImage;
    }

    public void setIncorrectFeedbackPopup(Image feedbackPopupImage) {
        this.IncorrectFeedbackPopupImage = feedbackPopupImage;
    }

    public void handleHost() {
        serverManager.SetupServer();
        clientManager.SetupLocalClient();
        clientManager.setHost(true);
    }

    public void handleJoin() {
        clientManager.SetupClient();
    }

    public void handleStart() {
        serverManager.startRound();
    }

    public void updatePlayerListUI(List<string> clientPlayerList) {
        GameObject.Find("PlayerListText").GetComponent<Text>().text = "";
        foreach(string name in clientPlayerList) {
            GameObject.Find("PlayerListText").GetComponent<Text>().text += "\n" + name;
        }
    }

    public IEnumerator triggerFeedbackPopup(bool correct) {
        showFeedbackPopup(correct, true);
        yield return new WaitForSeconds(5);
        showFeedbackPopup(correct, false);
    }

    private void showFeedbackPopup(bool correct, bool show) {
        if(correct) {
            CorrectFeedbackPopupImage.gameObject.SetActive(show);
        }
        else {
            IncorrectFeedbackPopupImage.gameObject.SetActive(show);
        }
    }

    public void setWinnerPanel(GameObject winnerPanel) {
        this.winnerPanel = winnerPanel;
    }

    public IEnumerator showWinnerPanel(string winner) {
        winnerPanel.SetActive(true);
        winnerPanel.transform.Find("PlayerNameLabel").GetComponent<Text>().text = winner;
        yield return new WaitForSeconds(5);
        GameObject.Find("Managers").GetComponent<RoundBehaviour>().updateObjectiveUI();
        winnerPanel.SetActive(false);
    }
}
