using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCDialogue : MonoBehaviour {
    public int npcId; //Able to determine which NPC this is
    public List<string> dialogue = new List<string>();
    public Sprite[] portraits;
    public int currentMode = 1;
}
