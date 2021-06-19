using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBubble : MonoBehaviour
{
    // Start is called before the first frame update
    public BlobObject CurrentBlob;
    private ColoredSphere mySphere;
    public GameObject sphere;

    private GameObject Player;
    private ColorBubbleInventory myInv;
    private Color currentColor;

    public string mySide;

    private void Start() {
        mySphere = sphere.GetComponent<ColoredSphere>();
        

        Player = GameObject.FindWithTag("Player");
        myInv = Player.GetComponent<ColorBubbleInventory>();
    }




    // Update is called once per frame
    void Update()
    {
        switch (mySide){
            case ("Left"):
            if(myInv.leftArmObj!=-1) {
                currentColor = myInv.Inventory[myInv.leftArmObj].Object.MyColor;
                sphere.SetActive(true);
            }
            else {
                sphere.SetActive(false);
            }
            
            break;

            case "Right":
             if(myInv.RightArmObj!=-1) {
                currentColor = myInv.Inventory[myInv.RightArmObj].Object.MyColor;
                sphere.SetActive(true);
             }
             else{
                 sphere.SetActive(false);
             }

            break;
        }
        
        //Debug.Log(CurrentBlob.MyColorName);
        mySphere.MyColor = new Color (currentColor.r, currentColor.g, currentColor.b, mySphere.MyColor.a);
        
    }
}
