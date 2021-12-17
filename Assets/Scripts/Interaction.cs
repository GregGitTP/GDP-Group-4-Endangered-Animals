using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Interaction : MonoBehaviour {
    // public variables
    public float interactionRange = 3f;
    public TextMeshProUGUI dialogueUI;
    public GameObject dialogueDisplay, dialoguePortrait, dialogueName;
    public bool interacting = false;
    public GameObject levelLoader;

    // private variables
    private int dialogueLine = 0;
    private int currentMode = 0;
    private bool dAnimating = false;

    private void Update() {
        if (!interacting && Input.GetKeyDown(KeyCode.F)) {
            StartInteraction();
        }

        if (Input.touchCount > 0) { //Mobile Control
            if (Input.GetTouch(0).phase == TouchPhase.Began) {
                Collider2D hit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));
                if (hit != null && !interacting) {
                    if (hit.CompareTag("InteractableNPC")) {
                        GetComponent<PlayerController>().moveTo = transform.position;
                        StartInteraction();
                    }
                }
            }
        }
    }

    // Function to find the closes interactable NPC 
    private GameObject FindClosesInteractable() {
        GameObject closest = null;
        float closestDist = interactionRange;

        GameObject[] NPCs = GameObject.FindGameObjectsWithTag("InteractableNPC");

        foreach (GameObject NPC in NPCs) {
            float dist = Vector3.Distance(transform.position, NPC.transform.position);

            if (dist < closestDist) {
                closest = NPC;
                closestDist = dist;
            }
        }

        return closest;
    }

    // Function to handle interaction event
    private void StartInteraction() {
        GameObject interactee = FindClosesInteractable();

        if (interactee != null) {
            GetComponent<PlayerController>().interacting = true;
            interactee.GetComponent<NPC>().interacting = true;
            interacting = true;
            dialogueLine = 0;
            dialogueUI.text = "";
            dialogueDisplay.SetActive(true); //Show the dialogue UI display
            dialogueName.GetComponent<TMP_Text>().text = interactee.GetComponent<NPC>().name;
            dialoguePortrait.GetComponent<Image>().sprite = interactee.GetComponent<NPCDialogue>().portraits[interactee.GetComponent<NPCDialogue>().currentMode]; //Set the portrait base on mode
            StartCoroutine(Dialogue(interactee));
        } else {
            Debug.Log("No interactables nearby!");
        }
    }

    IEnumerator Dialogue(GameObject interactee) {
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < interactee.GetComponent<NPCDialogue>().dialogue.Count; i++) {
            yield return StartCoroutine(DialogueAnimation(interactee.GetComponent<NPCDialogue>().dialogue[dialogueLine])); //Display the dialogue character by chracter
            yield return StartCoroutine(CheckForNextDialogue());
            yield return null;
        }
        dialogueDisplay.SetActive(false);
        if (interactee.GetComponent<NPCDialogue>().loadLevel) {
            StartCoroutine(levelLoader.GetComponent<LevelLoader>().LoadMinigame(interactee.GetComponent<NPCDialogue>().sceneID));
        } else {
            GetComponent<PlayerController>().interacting = false;
            interactee.GetComponent<NPC>().interacting = false;
            interacting = false;
            dAnimating = false;
        }
    }

    IEnumerator CheckForNextDialogue() {
        for (; ; )
        {
            if (Input.GetMouseButtonDown(0)) {
                dialogueLine++;
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator CheckForSkipDialogue() {
        for (; ; )
        {
            if (dAnimating) {
                if (Input.GetMouseButtonDown(0)) {
                    dAnimating = false;
                }
                yield return null;
            } else {
                yield break;
            }
        }
    }

    IEnumerator DialogueAnimation(string dialogue) {
        dAnimating = true;
        StartCoroutine(CheckForSkipDialogue());
        dialogueUI.text = "";
        for (int j = 0; j < dialogue.Length; j++) {

            if (!dAnimating) {
                dialogueUI.text = dialogue;
                yield return null;
                yield break;
            }

            dialogueUI.text += dialogue[j];
            yield return new WaitForSeconds(.05f);
        }
        dAnimating = false;
    }
}