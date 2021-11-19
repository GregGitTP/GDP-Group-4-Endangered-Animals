using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    public enum gameStates { Idle, InGame, Pause, GameOver };
    public gameStates currentGameState = gameStates.Idle;
    public int[] collectedTrash = new int[4]; //0 = Paper, 1 = Plastic, 2 = Metal, 3 = General Waste
    public TextMeshProUGUI[] collectedTrashUI;
    public static GameManager instance;

    void Start() {
        if (instance != null) {
            Destroy(gameObject);
        }
        instance = this;
    }

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < collectedTrash.Length; i++) {
            collectedTrashUI[i].text = collectedTrash[i].ToString();
        }
    }

    void GameOver() {
        currentGameState = gameStates.GameOver;
    }
}
