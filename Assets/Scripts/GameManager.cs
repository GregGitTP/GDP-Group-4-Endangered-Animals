using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    public enum gameStates { Idle, InGame, Pause, GameOver };
    public gameStates currentGameState = gameStates.Idle;
    public static GameManager instance;

    void Start() {
        if (instance != null) {
            Destroy(gameObject);
        }
        instance = this;
    }

    // Update is called once per frame
    void Update() {
    }

    void GameOver() {
        currentGameState = gameStates.GameOver;
    }
}
