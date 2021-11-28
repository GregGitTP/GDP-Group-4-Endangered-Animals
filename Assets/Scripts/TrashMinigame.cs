using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrashMinigame : MonoBehaviour {
    public int trashRemaining = 20, trashThrownCorrectly = 0, failsBeforeLose = 3;
    public float timeLeft = 60;
    public TextMeshProUGUI scoreTimer;

    private void Update() {
        if (MinigameManager.minigameManager.currentMinigameState == MinigameManager.MinigameState.InGame) {
            scoreTimer.gameObject.SetActive(true);
            timeLeft -= Time.deltaTime;
            scoreTimer.text = "Trash Remaining: " + trashRemaining + "\nTime Remaining: " + Mathf.FloorToInt(timeLeft) + "s";
            
            if (timeLeft < 0 && trashRemaining > 1) { //Ran out of time
                MinigameManager.minigameManager.LoseGame();
            }
        }
    }
    public void winScore() { trashThrownCorrectly++; }
    public void ThrownTrash() {
        trashRemaining--;
        if (failsBeforeLose == 0) {
            MinigameManager.minigameManager.LoseGame();
        }
        if (trashRemaining == 0) {
            if (trashThrownCorrectly == 20) {
                MinigameManager.minigameManager.WinGame();
            }
        }
    }
}