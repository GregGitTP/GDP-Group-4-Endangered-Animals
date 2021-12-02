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
    public int npcId = 0;
    private bool isStarting = false;
    void Start() {
        if (minigameManager == null) {
            minigameManager = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Update() {
        if (currentMinigameState == MinigameState.Idle && Input.touchCount > 0) {
            if (!isStarting) {
                StartCoroutine(StartMiniGame());
            }
            isStarting = true;
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


    public void EndGame(){
        currentMinigameState = MinigameState.EndGame;
        //Set data over to make the animal happy
        //SceneManager.LoadSceneAsync(1);
    }
}
