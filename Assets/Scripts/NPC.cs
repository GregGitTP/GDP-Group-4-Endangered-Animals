using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour {
    public Transform nameTransform;
    public TMP_Text nameComponent;
    //public string name;

    public bool interacting = false;

    public float moveRange = 5f;
    public float moveSpeed = 1f;

    private Animator anim;
    private Vector3 originalLocalScale;
    private Vector3 originalNameLocalScale;

    private float maxX;
    private float minX;
    private float maxY;
    private float minY;

    private float moveDist;
    private float distTravelled;
    private float dir;
    private float delay;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void Start() {
        originalLocalScale = transform.localScale;
        originalNameLocalScale = nameTransform.localScale;

        //nameComponent.text=name;
        nameComponent.text = transform.name;
        maxX = transform.position.x + moveRange;
        minX = transform.position.x - moveRange;
        maxY = transform.position.y + moveRange;
        minY = transform.position.y - moveRange;

        SetNewMove();
        UpdateAnimation();
        StartCoroutine(Move());
    }

    IEnumerator Move() {
        for (; ; ) {
            if (!interacting) {
                if (dir == 1) {
                    transform.position += Vector3.up * moveSpeed * Time.deltaTime;
                    if (transform.position.y > maxY) {
                        dir = 3;
                        UpdateAnimation();
                    }
                } else if (dir == 2) {
                    transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                    if (transform.position.x > maxX) {
                        dir = 4;
                        UpdateAnimation();
                    }
                } else if (dir == 3) {
                    transform.position += Vector3.down * moveSpeed * Time.deltaTime;
                    if (transform.position.y < minY) {
                        dir = 1;
                        UpdateAnimation();
                    }
                } else if (dir == 4) {
                    transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                    if (transform.position.x < minX) {
                        dir = 2;
                        UpdateAnimation();
                    }
                }
                distTravelled += moveSpeed * Time.deltaTime;
                if (distTravelled >= moveDist) {
                    SetNewMove();
                    yield return new WaitForSeconds(delay);
                    UpdateAnimation();
                }
            } else {
                ResetAnimation();
            }
            yield return null;
        }
    }

    private void SetNewMove() {
        ResetAnimation();
        distTravelled = 0;
        moveDist = 0;
        delay = 0;
        dir = Random.Range(1, 4);
        while (moveDist < 1) moveDist = Mathf.Round(Random.value * 10 * 10f) / 10f;
        while (delay < 1.5f) delay = Mathf.Round(Random.value * 3 * 10f) / 10f;
    }

    private void UpdateAnimation() {
        ResetAnimation();
        if (dir == 1) {
            // npc walk up animation
            return;
        } else if (dir == 2) {
            anim.SetBool("side", true);
            transform.localScale = new Vector3(originalLocalScale.x * -1, originalLocalScale.y, 0);
            nameTransform.localScale = new Vector3(originalNameLocalScale.x * -1, originalNameLocalScale.y, 0);
        } else if (dir == 3) {
            // npc walk up animation
            return;
        } else if (dir == 4) {
            anim.SetBool("side", true);
            transform.localScale = originalLocalScale;
            nameTransform.localScale = originalNameLocalScale;
        }
    }

    private void ResetAnimation() {
        transform.localScale = originalLocalScale;
        nameTransform.localScale = originalNameLocalScale;
        anim.SetBool("side", false);
    }
}
