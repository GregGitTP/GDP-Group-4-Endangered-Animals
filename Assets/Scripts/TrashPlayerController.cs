using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPlayerController : MonoBehaviour {
    public GameObject[] wayPoints;
    public int currentPosition = 2;
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (/*Swipe Left*/Input.GetKeyDown(KeyCode.A)) {
        if (currentPosition != 0) {
            currentPosition--;
        }
        } else if (/*Swipe Right*/Input.GetKeyDown(KeyCode.D)) {
        if (currentPosition != (wayPoints.Length-1)) {
            currentPosition++;
        }
        }

        transform.position = wayPoints[currentPosition].transform.position;

    }
}