using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class NPCDialogue : MonoBehaviour {
    public List<string> dialogue = new List<string>();
    public Sprite[] portraits;
    public int currentMode = 1;
}
