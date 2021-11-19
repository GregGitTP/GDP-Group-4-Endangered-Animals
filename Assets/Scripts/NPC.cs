using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public TMP_Text nameComponent;
    public string name;
    
    public float moveRange=5f;
    public float moveSpeed=1f;

    private float maxX;
    private float minX;
    private float maxY;
    private float minY;

    private float moveDist;
    private float distTravelled;
    private float dir;
    private float delay;

    private void Start(){
        nameComponent.text=name;

        maxX=transform.position.x+moveRange;
        minX=transform.position.x-moveRange;
        maxY=transform.position.y+moveRange;
        minY=transform.position.y-moveRange;

        SetNewMove();
        StartCoroutine(Move());
    }

    IEnumerator Move(){
        for(;;){
            if(dir==1){
                transform.position+=Vector3.up*moveSpeed*Time.deltaTime;
                dir=transform.position.y>maxY?3:1;
            }else if(dir==2){
                transform.position+=Vector3.right*moveSpeed*Time.deltaTime;
                dir=transform.position.x>maxX?4:2;
            }else if(dir==3){
                transform.position+=Vector3.down*moveSpeed*Time.deltaTime;
                dir=transform.position.y<minY?1:3;
            }else if(dir==4){
                transform.position+=Vector3.left*moveSpeed*Time.deltaTime;
                dir=transform.position.x<minX?2:4;
            }
            distTravelled+=moveSpeed*Time.deltaTime;
            if(distTravelled>=moveDist){
                SetNewMove();
                yield return new WaitForSeconds(delay);
            }
            yield return null;
        }
    }

    private void SetNewMove(){
        distTravelled=0;
        moveDist=0;
        delay=0;
        dir=Random.Range(1,4);
        while(moveDist<1)moveDist=Mathf.Round(Random.value*10*10f)/10f;
        while(delay<1.5f)delay=Mathf.Round(Random.value*3*10f)/10f;
    }
}
