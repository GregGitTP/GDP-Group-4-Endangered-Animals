using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float movementSpeed = 5f;
    Vector3 moveTo;
    void Start() {
        moveTo = transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            moveTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moveTo.z = 0;
        }

        float distanceToDestination = Vector3.Distance(transform.position, moveTo);
        print(distanceToDestination);
        if (distanceToDestination > (Time.deltaTime * movementSpeed)) {
            transform.position += Vector3.Normalize(moveTo - transform.position) * movementSpeed * Time.deltaTime;
        } else {
            transform.position = moveTo;
        }
    }
}
