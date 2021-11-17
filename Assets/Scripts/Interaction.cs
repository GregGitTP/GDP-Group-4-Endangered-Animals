using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    // public variables
    public float interactionRange=3f;

    // private variables
    private bool interacting=false;

    private void Update(){
        if(!interacting){
            if(Input.GetKeyDown(KeyCode.F)){
                Interact();
            }
        }
    }

    // Function to handle interaction event
    private void Interact(){
        interacting=true;
        GameObject interactee=FindClosesInteractable();

        if(interactee!=null){
            /* Add code for the interaction event (dialogue or start minigame or start quest) */
            /* interacting boolean will be set back to false once the event has finished */
        }
    }

    // Function to find the closes interactable NPC 
    private GameObject FindClosesInteractable(){
        GameObject closest=null;
        float closestDist=interactionRange;
        
        GameObject[] NPCs=GameObject.FindGameObjectsWithTag("InteractableNPC");

        foreach(GameObject NPC in NPCs){
            float dist=Vector3.Distance(transform.position,NPC.transform.position);
            
            if(dist<closestDist){
                closest=NPC;
                closestDist=dist;
            }
        }

        return closest;
    }
}
