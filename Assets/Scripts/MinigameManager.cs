using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MinigameManager : MonoBehaviour {
    public enum MinigameState { Idle, InGame, EndGame };
    public MinigameState currentMinigameState = MinigameState.Idle;
    public static MinigameManager minigameManager;
    public TrashPlayerController trashPlayerController;
    public GameObject countDown;
    public TextMeshProUGUI startTxt;
    public int npcId = 0;

    private bool isStarting = false;
    private Coroutine pulseStartCor;

    void Start() {
        if (minigameManager == null) {
            minigameManager = this;
        } else {
            Destroy(gameObject);
        }

        StartCoroutine(PulseStart());
    }

    void Update() {
        if (currentMinigameState == MinigameState.Idle && Input.touchCount > 0) {
            startTxt.text = "";
            if (!isStarting) {
                StartCoroutine(StartMiniGame());
            }
            isStarting = true;
        }
    }

    IEnumerator PulseStart(){
        startTxt.text = "Tap to start";
        for(;;){
            while(startTxt.fontSize < 40){
                startTxt.fontSize+=.01f;
                yield return null;
            }
            yield return new WaitForSeconds(.2f);
            while(startTxt.fontSize > 35){
                startTxt.fontSize-=.01f;
                yield return null;
            }
            yield return new WaitForSeconds(.2f);
        }
    }

    IEnumerator StartMiniGame() {
        
        countDown.SetActive(true);
        for (int i = 3; i > 0; i--) {
            countDown.GetComponent<TextMeshProUGUI>().text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }
        countDown.SetActive(false);
        currentMinigameState = MinigameState.InGame;
        trashPlayerController.SetNewTrash();
    }

    public void SetGameStateEnd(){
        currentMinigameState = MinigameState.EndGame;
    }
}