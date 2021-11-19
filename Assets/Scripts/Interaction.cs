using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class Interaction : MonoBehaviour {
    // public variables
    public float interactionRange = 3f;

    // private variables
    private bool interacting = false;
    //public TextMeshProUGUI dialogueUI;


    private void Update() {
        if (!interacting) {
            if (Input.GetKeyDown(KeyCode.F)) {
                StartInteraction();
            }
        }
    }

    // Function to handle interaction event
    private void StartInteraction() {
        GameObject interactee = FindClosesInteractable();

        if (interactee != null) {
            Debug.Log("You have interacted!");
            /* call the function for that specific NPC to start the event */
            /* interacting boolean will be set back to false once the event has finished */
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
}
