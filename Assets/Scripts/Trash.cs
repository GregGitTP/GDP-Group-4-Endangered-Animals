using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Trash : MonoBehaviour
{
    public SpriteRenderer sinkTrash;
    public TextMeshProUGUI trashName;

    [SerializeField] [Space(10)] Sprite[] paper, plastic, glass, metal, generalWaste, wash;

    enum TrashType{Paper, Plastic, Glass, Metal, GeneralWaste, Wash}
    TrashType trashType;

    enum WashStatus{Null, NotWashed, Washed}
    WashStatus washStatus;    

    Sprite[][] spriteArrays = new Sprite[4][];

    public void Start(){
        spriteArrays[0] = paper;
        spriteArrays[1] = plastic;
        spriteArrays[2] = glass;
        spriteArrays[3] = metal;
    }

    public void NewTrash(){
        trashType = (TrashType)Random.Range(0, 6); 
        washStatus = trashType != TrashType.Wash ? WashStatus.Null : WashStatus.NotWashed;
        switch (trashType) { //Setting the sprite
            case TrashType.Paper:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = paper[Random.Range(0, paper.Length)];
                break;
            case TrashType.Plastic:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = plastic[Random.Range(0, plastic.Length)];
                break;
            case TrashType.Glass:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = glass[Random.Range(0, glass.Length)];
                break;
            case TrashType.Metal:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = metal[Random.Range(0, metal.Length)];
                break;
            case TrashType.GeneralWaste:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = generalWaste[Random.Range(0, generalWaste.Length)];
                break;
            case TrashType.Wash:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = wash[Random.Range(0, wash.Length)];
                break;
        }
    }

    public IEnumerator Wash(){
        SpriteRenderer playerTrash = transform.GetChild(0).GetComponent<SpriteRenderer>();
        string spriteName = playerTrash.sprite.name;

        sinkTrash.sprite = playerTrash.sprite;
        playerTrash.sprite = null;

        yield return new WaitForSeconds(1f);
        
        spriteName = spriteName.Replace("dirty_","");

        for(int a = 0; a < spriteArrays.Length; a++){
            for(int b = 0; b < spriteArrays[a].Length; b++){
                if(spriteArrays[a][b].name == spriteName){
                    trashType = (TrashType)a;
                }
            }
        }

        sinkTrash.sprite = null;
        playerTrash.sprite = Resources.Load<Sprite>("TrashMinigame/Trash_Sprites/" + spriteName);

        trashName.text = "washed " + spriteName.Replace("sprite","").Replace("_","");

        washStatus = WashStatus.Washed;
    }

    public bool IsWashable(){return trashType == TrashType.Wash ? true : false;}

    public int GetTypeIndex(){return (int)trashType;}

    public bool IsWashed(){return washStatus == WashStatus.Washed ? true : false;}
}
