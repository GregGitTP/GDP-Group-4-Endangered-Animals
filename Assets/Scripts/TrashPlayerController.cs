using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrashPlayerController : MonoBehaviour {
    public enum TrashType { Paper, Plastic, Glass, Metal, GeneralWaste }
    public TrashType currentTrash = TrashType.GeneralWaste;
    public int currentPosition = 2;
    public float SWIPE_THRESHOLD = 20f;
    public TextMeshProUGUI trashName;
    public GameObject[] wayPoints;

    [Space(10)]
    public Sprite[] paper, plastic, glass, metal, generalWaste;
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    private TrashMinigame trashMinigame;
    void Start() {
        newTrash();
        trashMinigame = GameObject.FindGameObjectWithTag("GameController").GetComponent<TrashMinigame>();
    }

    // Update is called once per frame
    void Update() {

        foreach (Touch touch in Input.touches) {
            if (touch.phase == TouchPhase.Began) {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }
            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended) {
                fingerDown = touch.position;
                checkSwipe();
            }
        }

        /*//FOR PC TESTING, DELETE AFTER USE
        if (Input.GetKeyDown(KeyCode.A)) {
            if (currentPosition != 0) {
                currentPosition--;
            }
        } else if (Input.GetKeyDown(KeyCode.D)) {
            if (currentPosition != (wayPoints.Length - 1)) {
                currentPosition++;
            }
        } else if (Input.GetKeyDown(KeyCode.S)) {
            trashMinigame.ThrownTrash();
            if (currentPosition == (int)currentTrash) {
                trashMinigame.winScore();
            } else {
                trashMinigame.failsBeforeLose--;
            }
            newTrash();
        }
        transform.position = wayPoints[currentPosition].transform.position;*/

    }
    private void newTrash() {
        currentTrash = (TrashType)Random.Range(0, 5);
        switch (currentTrash) {
            case TrashType.Paper:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = paper[Random.Range(0, paper.Length)];
                break;
            case TrashType.Plastic:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = plastic[Random.Range(0, plastic.Length)];
                break;
            case TrashType.Glass:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = glass[Random.Range(0, glass.Length)];
                break;
            case TrashType.Metal:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = metal[Random.Range(0, metal.Length)];
                break;
            case TrashType.GeneralWaste:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = generalWaste[Random.Range(0, generalWaste.Length)];
                break;
        }
        trashName.text = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name.Replace("sprite", "").Replace("_", " ");
    }
    void checkSwipe() {
        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove()) {
            //Debug.Log("Vertical");
            if (fingerDown.y - fingerUp.y > 0) //up swipe
            {
                SwipeDirection(3);
            } else if (fingerDown.y - fingerUp.y < 0) //Down swipe
            {
                SwipeDirection(4);
            }
            fingerUp = fingerDown;
        }

        //Check if Horizontal swipe
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove()) {
            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0) //Right swipe
            {
                SwipeDirection(2);
            } else if (fingerDown.x - fingerUp.x < 0) //Left swipe
            {
                SwipeDirection(1);
            }
            fingerUp = fingerDown;
        }

        //No Movement at-all
        else {
            //Debug.Log("No Swipe!");
        }
    }

    float verticalMove() {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove() {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    void SwipeDirection(int dir) {
        if (MinigameManager.minigameManager.currentMinigameState == MinigameManager.MinigameState.InGame) {
            switch (dir) {
                case 1: //Left
                    if (currentPosition != 0) {
                        currentPosition--;
                    }
                    break;
                case 2: //Right
                    if (currentPosition != (wayPoints.Length - 1)) {
                        currentPosition++;
                    }
                    break;
                case 3: //Up
                    break;
                case 4: //Down
                    trashMinigame.ThrownTrash();
                    if (currentPosition == (int)currentTrash) {
                        trashMinigame.winScore();
                    } else {
                        trashMinigame.failsBeforeLose--;
                    }
                    newTrash();
                    break;
            }
            transform.position = wayPoints[currentPosition].transform.position;
        }
    }
}