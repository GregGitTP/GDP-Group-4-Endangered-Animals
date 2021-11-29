using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoystick : MonoBehaviour
{
    public Joystick js;
    public float movementSpeed = 5f;
    public Animator anim;

    Vector3 originalLocalScale;
    Rigidbody2D rb;

    private void Start(){
        rb=GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate(){
        float hori=0;
        float vert=0;

        if(js.Horizontal>.5f || js.Horizontal<-.5f) hori=js.Horizontal*movementSpeed;
        if(js.Vertical>.5f || js.Vertical<-.5f) vert=js.Vertical*movementSpeed;

        rb.velocity=new Vector2(hori,vert);
        UpdateAnimation();
    }

    private void UpdateAnimation(){
        if(js.Horizontal<.5f && js.Horizontal>-.5f){
            ResetAnimation();
            return;
        }else if(js.Vertical<.5f && js.Vertical>-.5f){
            ResetAnimation();
            return;
        }

        Vector2 moveVector = new Vector2(js.Horizontal, js.Vertical);

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
    }
}
