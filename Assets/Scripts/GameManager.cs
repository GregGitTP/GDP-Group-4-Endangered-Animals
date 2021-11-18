using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public enum gameStates { Idle, InGame, Pause, GameOver };
    public gameStates currentGameState = gameStates.Idle;
    public int collectedTrash = 0; 

    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void GameOver() {
        currentGameState = gameStates.GameOver;
        //Run GameOver Things
    }
}
