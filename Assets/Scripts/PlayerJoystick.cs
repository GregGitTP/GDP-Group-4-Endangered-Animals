using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoystick : MonoBehaviour
{
    public Joystick js;
    public float movementSpeed = 5f;
    public Animator anim;
    public Camera mainCamera;

    Vector3 originalLocalScale;
    Rigidbody2D rb;

    private void Start(){
        rb=GetComponent<Rigidbody2D>();
        originalLocalScale=transform.localScale;
    }

    private void Update(){
        CameraFollow();
        UpdateAnimation();
    }

    private void FixedUpdate(){
        float hori=0;
        float vert=0;

        if(js.Horizontal>.5f || js.Horizontal<-.5f){
            hori=js.Horizontal*movementSpeed;
        }
        if(js.Vertical>.5f || js.Vertical<-.5f){
            vert=js.Vertical*movementSpeed;
        } 

        rb.velocity=new Vector3(hori,vert,0f);
    }

    private void UpdateAnimation(){
        ResetAnimation();
        if(!(js.Horizontal>.5f || js.Horizontal<-.5f)&&!(js.Vertical>.5f || js.Vertical<-.5f)) return;

        Vector3 moveVector = new Vector3(js.Horizontal, js.Vertical,0f);

        float targetAngle=0f;

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

        float offset = 45;

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

    private void ResetAnimation(){
        anim.SetBool("up",false);
        anim.SetBool("down",false);
        anim.SetBool("side",false);
        transform.localScale=originalLocalScale;
    }

    private void CameraFollow()
    {
        mainCamera.transform.position = new Vector3(transform.position.x,transform.position.y,mainCamera.transform.position.z);
    }
}
