using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryBubble{
    public BlobObject Object;
    public int Count;

    
    public InventoryBubble (BlobObject obj, int count){
        Object = obj;
        Count = count;
    }
}


public class ColorBubbleInventory : MonoBehaviour
{
    public List<InventoryBubble> Inventory = new List<InventoryBubble>();
    
    [SerializeField] private GameObject LeftArm, RightArm;
    private int leftArmObj, RightArmObj;
    public float LMBcurrent, RMBcurrent, LMBMax, RMBMax;



    [SerializeField] private Transform LeftHand, RightHand;
    

    public void PickUP(BlobObject blobObject, int count){
        bool Exist = false;
        for (int i = 0; i < Inventory.Count; i++)
        {
            if (Inventory[i].Object == blobObject) 
            {
                Inventory[i].Count+=count;
                Exist = true;
            }
        }
        if (!Exist){
            Inventory.Add(new InventoryBubble(blobObject, count));
        }

        //Debug.Log("ITEMADDED!");
    }

    private void checkMouseInput(string button){
        
        switch (button){
            case "Left":
                if (LMBcurrent >= LMBMax/2 && LMBcurrent < LMBMax)
                {
                
                LMBcurrent = 0;
                //действие 1

                }
                else if(LMBcurrent >= LMBMax){
                    LMBcurrent = 0;
                //действие 2
                }
                else LMBcurrent = 0;
            break;
            case "Right":
                if (RMBcurrent >= LMBMax/2 && RMBcurrent < LMBMax)
                {
                
                RMBcurrent = 0;
                //действие 1

                }
                else if(RMBcurrent >= LMBMax){
                    RMBcurrent = 0;
                //действие 2
                }
                else RMBcurrent = 0;
            break;
        }
        
    }



    private void Update() {
        if (Input.GetAxis("MouseLeft")>0) {
            LMBcurrent = Mathf.Clamp(LMBcurrent+Time.deltaTime, 0, LMBMax);
        }
        else checkMouseInput("Left");

        if(Input.GetAxis("MouseRight")>0) RMBcurrent = Mathf.Clamp(RMBcurrent+Time.deltaTime, 0, RMBMax);
        else checkMouseInput("Right");
        

        Debug.Log (LMBcurrent);
    }
}
