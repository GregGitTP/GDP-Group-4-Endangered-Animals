using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float movementSpeed = 5f;
    public static PlayerController playerController;
    Vector3 moveTo;
    void Start() {
        if (PlayerController.playerController != null) {
            Destroy(gameObject);
        }
        playerController = this;
        moveTo = transform.position;
    }

    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            moveTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moveTo.z = 0;
        }

        float distanceToDestination = Vector3.Distance(transform.position, moveTo);
        if (!GetComponent<Interaction>().interacting) {
            if (distanceToDestination > (Time.deltaTime * movementSpeed)) {
                transform.position += Vector3.Normalize(moveTo - transform.position) * movementSpeed * Time.deltaTime;
            } else {
                transform.position = moveTo;
            }
        }
    }

    /*private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Trash>() != null) {
            GameManager.instance.collectedTrash[(int) other.GetComponent<Trash>().type]++;
            Destroy(other.gameObject);

        }
    }*/
}