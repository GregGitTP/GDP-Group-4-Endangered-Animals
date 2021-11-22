using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Interaction : MonoBehaviour {
    // public variables
    public float interactionRange = 3f;
    public TextMeshProUGUI dialogueUI;
    public GameObject dialogueDisplay, dialoguePortrait;
    public bool interacting = false;
    // private variables
    private int dialogueLine = 0;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            StartInteraction();
        }
    }

    // Function to handle interaction event
    private void StartInteraction() {
        GameObject interactee = FindClosesInteractable();

        if (interactee != null) {
            Debug.Log("You have interacted!");
            interactee.GetComponent<NPC>().interacting = true;
            interacting = true;
            if (interactee.GetComponent<NPCDialogue>().dialogue.Count == dialogueLine) { //If the dialogue has ended
                dialogueLine = 0; //Reset the dialogue counter
                interacting = false; //Set interacting back to false
                dialogueDisplay.SetActive(false); //Hide dialogue display
                interactee.GetComponent<NPC>().enabled = true; //Allow NPC to move again

            } else {
                StartCoroutine(StartDialogue(interactee.GetComponent<NPCDialogue>().dialogue[dialogueLine])); //Display the dialogue character by chracter
                dialogueLine++; //Increase to the next line
                dialoguePortrait.GetComponent<Image>().sprite = interactee.GetComponent<NPCDialogue>().portraits[interactee.GetComponent<NPCDialogue>().currentMode]; //Set the portrait base on mode
                dialogueDisplay.SetActive(true); //Show the dialogue UI display
                interactee.GetComponent<NPC>().enabled = false; //Stop the NPC from moving away
            }
        } else {
            Debug.Log("No interactables nearby!");
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

    IEnumerator StartDialogue(string dialogue) {
        dialogueUI.text = "";
        for (int j = 0; j < dialogue.Length; j++) {
            dialogueUI.text += dialogue[j];
            yield return new WaitForSeconds(0.1f);
        }
    }
}