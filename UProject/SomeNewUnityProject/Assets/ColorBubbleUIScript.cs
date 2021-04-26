﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorBubbleUIScript : MonoBehaviour
{
    // Start is called before the first frame update
    private ColorBubbleInventory InventoryRef;

    [SerializeField] private BlobObject myBlob;
    [SerializeField] private Image myImage;


    private void Start() {
        InventoryRef = GameObject.FindWithTag("Player").GetComponent<ColorBubbleInventory>();
        Debug.Log(InventoryRef.Inventory[0].Object.MyColor);
    }

    private void CheckMyColor(){
        bool found = false;
        for (int i = 0; i < InventoryRef.Inventory.Count; i++)
        {   
            
            if(!found){
                if(InventoryRef.Inventory[i].Object.MyColorName == myBlob.MyColorName ){
                    //Debug.Log("found");
                    found = true;
                    Debug.Log( myBlob.MyColorName + "Exists");
                    myImage.enabled = true;
                    myImage.color = myBlob.MyColor;
                    break;
                    
                }
                else myImage.enabled = false;
            }
        }
        //if (!found) this.gameObject.SetActive(false);
    }



    void Update()
    {
        CheckMyColor();
    }
}