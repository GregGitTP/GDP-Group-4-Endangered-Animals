using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrashMinigame : MonoBehaviour {
    public TextMeshProUGUI scoreTimer;
    public GameObject endScreenCanvas;
    public TextMeshProUGUI endScreenTxt;
    public TextMeshProUGUI reqScoreTxt;
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI continueTxt;
    public GameObject levelLoader;

    private float timeLeft = 60;
    private int score = 0;
    private int reqScore = 3000;

    private void Update() {
        if (MinigameManager.minigameManager.currentMinigameState == MinigameManager.MinigameState.InGame) {
            scoreTimer.gameObject.SetActive(true);
            timeLeft -= Time.deltaTime;
            scoreTimer.text = "Score: " + score + "\nTime Remaining: " + Mathf.FloorToInt(timeLeft) + "s";
            
            if (timeLeft < 0) { //Ran out of time
                MinigameManager.minigameManager.SetGameStateEnd();
                EndGame();
            }
        }
    }

    private void EndGame(){
        scoreTimer.text = "Score: " + score + "\nTime Remaining: 0s";

        endScreenCanvas.SetActive(true);

        endScreenTxt.text = score >= reqScore ? "Well Done!" : "Try Again!";

        reqScoreTxt.text = "Required Score: " + reqScore;
        scoreTxt.text = "Score: " + score;
        StartCoroutine(WaitForContinue());
    }

    IEnumerator WaitForContinue(){
        endScreenCanvas.SetActive(true);
        scoreTimer.text = "";
        continueTxt.text = "";

        yield return new WaitForSeconds(2f);

        continueTxt.text = "Tap to continue";
        StartCoroutine(PulseContinue());

        for(;;){
            if(Input.touchCount > 0){
                StartCoroutine(levelLoader.GetComponent<LevelLoader>().LoadMinigame(SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Main.unity")));
            }
            yield return null;
        }
    }

    IEnumerator PulseContinue(){
        for(;;){
            while(continueTxt.fontSize < 40){
                continueTxt.fontSize+=.01f;
                yield return null;
            }
            yield return new WaitForSeconds(.2f);
            while(continueTxt.fontSize > 35){
                continueTxt.fontSize-=.01f;
                yield return null;
            }
            yield return new WaitForSeconds(.2f);
        }
    }

    public void Reset(){
        endScreenCanvas.SetActive(false);
        continueTxt.text = "";
        score = 0;
        timeLeft = 60;
    }

    public void ScoreNormal(){
        score+=100;
    }

    public void ScoreWashed(){
        score+=150;
    }

    public void ScoreNotWashed(){
        score += 50;
    }

    public void ScoreWrong(){
        score-=100;
        if(score<0) score = 0;
    }

    public void WashWrong(){
        score-=50;
        if(score<0) score = 0;
    }
}