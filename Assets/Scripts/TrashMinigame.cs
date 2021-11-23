using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrashMinigame : MonoBehaviour {
    public int trashRemaining = 20, trashThrownCorrectly = 0;
    public float timeLeft = 60;
    public TextMeshProUGUI scoreTimer;

    private void Update() {
        timeLeft -= Time.deltaTime;
        scoreTimer.text = "Trash Remaining: " + trashRemaining + "\nTime Remaining: " + Mathf.FloorToInt(timeLeft) + "s";
    }
    public void winScore() { trashThrownCorrectly++; }
    public void ThrownTrash() { trashRemaining--; }
}