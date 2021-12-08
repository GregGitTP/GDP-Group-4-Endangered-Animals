using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class NPCDialogue : MonoBehaviour {
    [HideInInspector]
    public List<string> dialogue = new List<string>();

    public List<string> sadDialogue = new List<string>();
    public List<string> neutralDialogue = new List<string>();
    public List<string> happyDialogue = new List<string>();
    public Sprite[] portraits;
    public int currentMode = 1;
    public int npcId = 0;
    public bool loadLevel;
    public int sceneID;

    private void Start() {
        if (PlayerPrefs.HasKey("moodOf" + npcId)) {
            currentMode = PlayerPrefs.GetInt("moodOf" + npcId);
        }

        switch (currentMode) {
            case 0:
                dialogue = sadDialogue;
                break;
            case 1:
                dialogue = neutralDialogue;
                break;
            case 2:
                dialogue = happyDialogue;
                break;
        }
    }
}
