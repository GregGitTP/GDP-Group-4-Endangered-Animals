using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPlayerController : MonoBehaviour {
    public enum TrashType { Paper, Plastic, Glass, Metal, GeneralWaste }
    public TrashType currentTrash = TrashType.GeneralWaste;

    public GameObject[] wayPoints;
    public Sprite[] trashAssets;
    public int currentPosition = 2;
    public float SWIPE_THRESHOLD = 20f;
    private Vector2 fingerDown;
    private Vector2 fingerUp;

    void Start() {
        newTrash();
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

        //FOR PC TESTING, DELETE AFTER USE
        if (Input.GetKeyDown(KeyCode.A)) {
            if (currentPosition != 0) {
                currentPosition--;
            }
        } else if (Input.GetKeyDown(KeyCode.D)) {
            if (currentPosition != (wayPoints.Length - 1)) {
                currentPosition++;
            }
        } else if (Input.GetKeyDown(KeyCode.S)) {
            if (currentPosition == (int) currentTrash) {
                print("correct");
            } else {
                print("WRONG");
            }
            newTrash();
        }

        transform.position = wayPoints[currentPosition].transform.position;
        //////////////////////////////////////////////////////////////////

    }
    private void newTrash() {
        currentTrash = (TrashType) Random.Range(0, 5);

        //transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = trashAssets[(int) currentTrash]; //Enable this when asset is here

        //FOR TESTING WITHOUT ART ASSET
        switch (currentTrash) {
            case TrashType.Paper:
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case TrashType.Plastic:
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.gray;
                break;
            case TrashType.Glass:
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case TrashType.Metal:
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.black;
                break;
            case TrashType.GeneralWaste:
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
        }
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
                break;
        }
        transform.position = wayPoints[currentPosition].transform.position;
    }
}