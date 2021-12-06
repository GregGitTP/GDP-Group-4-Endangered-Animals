using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrashPlayerController : MonoBehaviour {
    public TextMeshProUGUI trashName;
    public GameObject[] wayPoints;
    public SpriteRenderer sink;
    public AudioClip[] soundEffects;

    public int currentPosition = 2;
    public float SWIPE_THRESHOLD = 20f;

    private TrashMinigame trashMinigame;
    private Trash trash;

    private Vector2 fingerDown;
    private Vector2 fingerUp;
    private bool washing = false;
    private bool lockWashing = false;

    void Start() {
        trashMinigame = GameObject.FindGameObjectWithTag("GameController").GetComponent<TrashMinigame>();
        trash = GetComponent<Trash>();
        
        trashMinigame.Reset();
    }

    void Update() {
        if(!washing){
            foreach (Touch touch in Input.touches) {
                if (touch.phase == TouchPhase.Began) {
                    fingerUp = touch.position;
                    fingerDown = touch.position;
                }
                //Detects swipe after finger is released
                else if (touch.phase == TouchPhase.Ended) {
                    fingerDown = touch.position;
                    checkSwipe();
                }

                else if(touch.tapCount == 2){
                    if(!lockWashing) StartCoroutine(StartWashing());
                }
            }
        }

        //PCControls();
    }

    public void SetNewTrash() {
        trash.NewTrash();
        trashName.text = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name.Replace("sprite", "").Replace("_", " "); //Using the sprite name to display the trash name
    }

    void checkSwipe() {
        //Check if Vertical swipe
        if (VerticalMove() > SWIPE_THRESHOLD && VerticalMove() > HorizontalValMove()) {
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
        else if (HorizontalValMove() > SWIPE_THRESHOLD && HorizontalValMove() > VerticalMove()) {
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

    float VerticalMove() {return Mathf.Abs(fingerDown.y - fingerUp.y);}
    
    float HorizontalValMove() {return Mathf.Abs(fingerDown.x - fingerUp.x);}

    void SwipeDirection(int dir) {
        if (MinigameManager.minigameManager.currentMinigameState == MinigameManager.MinigameState.InGame) {
            switch (dir) {
                case 1: //Left
                    if (currentPosition != 0) {
                        currentPosition--;
                        GetComponent<AudioSource>().PlayOneShot(soundEffects[0]);
                    }
                    break;
                case 2: //Right
                    if (currentPosition != (wayPoints.Length - 1)) {
                        currentPosition++;
                        GetComponent<AudioSource>().PlayOneShot(soundEffects[0]);
                    }
                    break;
                case 3: //Up
                    //if(!lockWashing) StartCoroutine(StartWashing());
                    break;
                case 4: //Down
                    if(trash.IsWashed() && currentPosition == trash.GetTypeIndex()){
                        trashMinigame.ScoreWashed();
                        StartCoroutine(PlayerFeedback(true));
                    }else if(currentPosition == trash.GetTypeIndex()){
                        trashMinigame.ScoreNormal();
                        StartCoroutine(PlayerFeedback(true));
                    }else if(currentPosition == 4 && trash.GetTypeIndex() == 5){
                        trashMinigame.ScoreNotWashed();
                        StartCoroutine(PlayerFeedback(true));
                    }else{
                        trashMinigame.ScoreWrong();
                        StartCoroutine(PlayerFeedback(false));
                    }
                    SetNewTrash();
                    break;
            }
            transform.position = wayPoints[currentPosition].transform.position;
        }
    }

    IEnumerator StartWashing(){
        if(!trash.IsWashable()){
            GetComponent<AudioSource>().PlayOneShot(soundEffects[2]);
            trashMinigame.WashWrong();
            StartCoroutine(WashingCooldown());
            SetNewTrash();
            yield break;
        }
        GetComponent<AudioSource>().PlayOneShot(soundEffects[1]);
        washing = true;
        yield return StartCoroutine(trash.Wash());
        washing = false;
    }

    IEnumerator WashingCooldown(){
        lockWashing = true;
        sink.sprite = Resources.Load<Sprite>("TrashMinigame/Sink_Sprites/sink_off");
        yield return new WaitForSeconds(5);
        sink.sprite = Resources.Load<Sprite>("TrashMinigame/Sink_Sprites/sink_on");
        lockWashing = false;
    }

    IEnumerator PlayerFeedback(bool isCorrect) {
        GameObject feedBackObj = wayPoints[currentPosition].transform.GetChild(0).gameObject;
        ParticleSystem.MainModule settings = feedBackObj.GetComponent<ParticleSystem>().main;

        if (isCorrect) {
            GetComponent<AudioSource>().PlayOneShot(soundEffects[3]);
            feedBackObj.GetComponent<SpriteRenderer>().color = new Color(100f / 255f, 200f / 255f, 100f / 255f, 125f / 255f);
            settings.startColor = new ParticleSystem.MinMaxGradient(new Color(100f / 255f, 200f / 255f, 100f / 255f));
        } else {
            GetComponent<AudioSource>().PlayOneShot(soundEffects[4]);
            feedBackObj.GetComponent<SpriteRenderer>().color = new Color(200f / 255f, 100f / 255f, 100f / 255f, 125f / 255f);
            settings.startColor = new ParticleSystem.MinMaxGradient(new Color(200f / 255f, 100f / 255f, 100f / 255f));
        }
        feedBackObj.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSecondsRealtime(1);
        feedBackObj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }


    // FOR PC TESTING
    private void PCControls(){
        // if (Input.GetKeyDown(KeyCode.A)) {
        //     if (currentPosition != 0) {
        //         currentPosition--;
        //     }
        // } else if (Input.GetKeyDown(KeyCode.D)) {
        //     if (currentPosition != (wayPoints.Length - 1)) {
        //         currentPosition++;
        //     }
        // } else if (Input.GetKeyDown(KeyCode.S)) {
        //     trashMinigame.ThrownTrash();
        //     if (currentPosition == (int)currentTrash) {
        //         trashMinigame.winScore();
        //     } else {
        //         trashMinigame.failsBeforeLose--;
        //     }
        //     newTrash();
        // }
        // transform.position = wayPoints[currentPosition].transform.position;
    }
}