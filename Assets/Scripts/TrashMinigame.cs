using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrashMinigame : MonoBehaviour {
    public TextMeshProUGUI scoreTimer;

    private int score = 0;
    private float timeLeft = 60;

    private void Update() {
        if (MinigameManager.minigameManager.currentMinigameState == MinigameManager.MinigameState.InGame) {
            scoreTimer.gameObject.SetActive(true);
            timeLeft -= Time.deltaTime;
            scoreTimer.text = "Score: " + score + "\nTime Remaining: " + Mathf.FloorToInt(timeLeft) + "s";
            
            if (timeLeft < 0) { //Ran out of time
                MinigameManager.minigameManager.EndGame();
            }
        }
    }

    public void Reset(){
        score = 0;
        timeLeft = 60;
    }

    public void ScoreNormal(){score+=100;}

    public void ScoreWashed(){score+=150;}

    public void ScoreWrong(){score-=100;}
    
    public void WashWrong(){score-=50;}
}