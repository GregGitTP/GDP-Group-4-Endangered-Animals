using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController playerController;
    public Animator anim;
    public Camera mainCamera;

    public float movementSpeed = 5f;

    Vector3 originalLocalScale;
    bool moving;
    float targetAngle = 0;
    Vector3 moveTo;
    Vector3 moveVector;

    void Start() {
        if (PlayerController.playerController != null) {
            Destroy(gameObject);
        }
        playerController = this;

        originalLocalScale = transform.localScale;

        moveTo = transform.position;
    }

    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            moveTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moveTo.z = 0;
            moving = true;
            ResetAnimations();
            SetTargetAngle();
        }

        float distanceToDestination = Vector3.Distance(transform.position, moveTo);
        if (distanceToDestination > (Time.deltaTime * movementSpeed)) {
            transform.position += Vector3.Normalize(moveTo - transform.position) * movementSpeed * Time.deltaTime;
        } else {
            transform.position = moveTo;
            moving = false;
        }

        CameraFollow();
        UpdateAnimation();
    }

    private void CameraFollow()
    {
        mainCamera.transform.position = new Vector3(transform.position.x,transform.position.y,mainCamera.transform.position.z);
    }

    /*private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Trash>() != null) {
            GameManager.instance.collectedTrash[(int) other.GetComponent<Trash>().type]++;
            Destroy(other.gameObject);

        }
    }*/

    private void SetTargetAngle()
    {
        moveVector = new Vector3(moveTo.x - transform.position.x, moveTo.y - transform.position.y, 0);

        int quad;
        if (moveVector.x >= 0)
        {
            if (moveVector.y >= 0) quad = 1;
            else quad = 4;
        }
        else
        {
            if (moveVector.y >= 0) quad = 2;
            else quad = 3;
        }

        float _angle = Mathf.Rad2Deg*Mathf.Atan(Mathf.Abs(moveVector.y / moveVector.x));
        if (quad == 1) targetAngle = _angle;
        else if (quad == 2) targetAngle = 180f - _angle;
        else if (quad == 3) targetAngle = 180f + _angle;
        else if (quad == 4) targetAngle = 360f - _angle;
    }

    private void UpdateAnimation()
    {
        float offset = 45;
        if (moving)
        {
            if (targetAngle > 0f + offset && targetAngle <= 90f + offset)
            {
                // Character walk up
                anim.SetBool("up", true);
            }
            else if (targetAngle > 90f + offset && targetAngle <= 180f + offset)
            {
                // Character walk left
                anim.SetBool("side", true);
            }
            else if (targetAngle > 180f + offset && targetAngle <= 270f + offset)
            {
                // Character walk down
                anim.SetBool("down", true);
            }
            else
            {
                // Character walk right
                anim.SetBool("side", true);
                transform.localScale = new Vector3(originalLocalScale.x * -1, originalLocalScale.y, 0);
            }
        }
        else
        {
            // Character stand still
            ResetAnimations();
        }
    }

    private void ResetAnimations()
    {
        transform.localScale = originalLocalScale;
        anim.SetBool("up", false);
        anim.SetBool("down", false);
        anim.SetBool("side", false);
    }
}