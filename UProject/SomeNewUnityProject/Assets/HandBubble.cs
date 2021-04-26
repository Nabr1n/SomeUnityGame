using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBubble : MonoBehaviour
{
    // Start is called before the first frame update
    public BlobObject CurrentBlob;
    private MeshRenderer myRend;
    public GameObject sphere;

    private GameObject Player;
    private ColorBubbleInventory myInv;

    public string mySide;

    private void Start() {
        myRend = sphere.GetComponent<MeshRenderer>();
        Player = GameObject.FindWithTag("Player");
        myInv = Player.GetComponent<ColorBubbleInventory>();
    }




    // Update is called once per frame
    void Update()
    {
        switch (mySide){
            case ("Left"):
            CurrentBlob = myInv.Inventory[myInv.leftArmObj].Object;
            
            break;

            case "Right":
            CurrentBlob = myInv.Inventory[myInv.RightArmObj].Object;
            break;
        }
        
        //Debug.Log(CurrentBlob.MyColorName);
        myRend.material = CurrentBlob.myMaterial;
    }
}
