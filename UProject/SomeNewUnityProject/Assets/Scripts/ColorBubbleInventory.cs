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
    public List<SecretColor> TipsInventory = new List<SecretColor>();
    
    [SerializeField] private GameObject LeftArm, RightArm;
    [HideInInspector] public int leftArmObj, RightArmObj;
    public float LMBcurrent, RMBcurrent, LMBMax, RMBMax;
    [SerializeField] private Camera myCamera;
    
    private GameObject currentCenteredGameObject = null;

    [SerializeField] private Transform LeftHand, RightHand;

    public GameObject PlayerUI;

    public static BlobObject ActivatedBlob;

    

    private void Start() {
        leftArmObj = -1;
        RightArmObj = -1;
        ActivatedBlob = null;
    }

    public void PickUP(BlobObject blobObject, int count){
        bool Exist = false;
        for (int i = 0; i < Inventory.Count; i++)
        {
            if (Inventory[i].Object.MyColorName == blobObject.MyColorName) 
            {
                Inventory[i].Count+=count;
                Exist = true;
            }
        }
        if (!Exist){
            Inventory.Add(new InventoryBubble(blobObject, count));
        }
        else if(leftArmObj==-1&&RightArmObj==-1){
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i].Object.MyColorName == blobObject.MyColorName){
                    leftArmObj = i;
                    break;
                }
            }
            
            
            
        }
        else if (leftArmObj!=-1&RightArmObj==-1){
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i].Object.MyColorName == blobObject.MyColorName){
                    RightArmObj = i;
                    break;
                }
            }
        }
        // else if(leftArmObj==-1&&RightArmObj!=-1){
        //     SwitchBubble2("Left");
        // }
        //Debug.Log("ITEMADDED!");
    }

    private void checkMouseInput(string button){
        
        switch (button){
            case "Left":
                if (LMBcurrent >= LMBMax/2 && LMBcurrent < LMBMax)
                {
                
                SwitchBubble2("Left");
                
                LMBcurrent = 0;
                //действие 1

                }
                else if(LMBcurrent >= LMBMax){
                    LMBcurrent = 0;
                    UseCurrentBubble("Left");
                }
                else LMBcurrent = 0;
            break;
            case "Right":
                if (RMBcurrent >= LMBMax/2 && RMBcurrent < LMBMax)
                {
                SwitchBubble2("Right");
                RMBcurrent = 0;
                //действие 1

                }
                else if(RMBcurrent >= LMBMax){
                    RMBcurrent = 0;
                    UseCurrentBubble("Right");
                //действие 2
                }
                else RMBcurrent = 0;
            break;
        }
        
    }

    private RaycastHit CheckForward(float distance, bool withDebug, Color DebugColor, float DebugDrawTime){
        RaycastHit hit;
        Physics.Raycast (myCamera.transform.position, myCamera.transform.forward,  out hit, distance);
        //Physics.CapsuleCast(myCamera.transform.position + myCamera.transform.forward*1f, myCamera.transform.forward*distance, 0.5f ,myCamera.transform.forward, out hit, distance);
        Debug.Log(hit.collider);
        if (withDebug) Debug.DrawRay (hit.point, Vector3.up, Color.red, 30f);
        
        
        return hit;

    }

    private GameObject GetGameObjectInCenter (float distance, bool withDebug, Color DebugColor, float DebugDrawTime){
        GameObject CenteredObject = null;
        if(CheckForward(distance, withDebug, DebugColor, DebugDrawTime).collider!=null) CenteredObject = 
                                                        CheckForward(distance, withDebug, DebugColor, DebugDrawTime).collider.gameObject;
        
        


        return CenteredObject;
    }



    private void SwitchBubble(string Hand){
        
        
        switch (Hand){
            case "Left":

            
            if(!(leftArmObj+1>Inventory.Count-1)&&leftArmObj+1!=RightArmObj){
                leftArmObj ++;
            }
            else if (!(leftArmObj+1>Inventory.Count-1)&&leftArmObj+1==RightArmObj){
                leftArmObj += 2;
            }
            else if (leftArmObj+1>Inventory.Count-1&&RightArmObj!=0){
                leftArmObj = 0;
            }
            else if (leftArmObj+1>Inventory.Count-1&&RightArmObj==0){
                leftArmObj = 1;
            }
            
            break;

            case "Right":
             if(!(RightArmObj+1>Inventory.Count-1)&&RightArmObj+1!=leftArmObj){
                RightArmObj ++;
            }
            else if(!(RightArmObj+1>Inventory.Count-1)&&RightArmObj+1==leftArmObj){
                RightArmObj +=2;
            }
            else if (RightArmObj+1>Inventory.Count-1&&leftArmObj!=0){
                RightArmObj = 0;
            }

            else if (RightArmObj+1>Inventory.Count-1&&leftArmObj==0){
                RightArmObj = 1;
            }
            
            Debug.Log(RightArmObj);
            
            break;
        }
    }



    private void SwitchBubble2(string Hand){
        bool exist = false;
        
        switch (Hand){
            case "Left":

            for (int i = 0; i < Inventory.Count; i++)
            {
                int selected = (i + 1 + leftArmObj)%Inventory.Count;
                if(Inventory[selected].Count>0&&selected != RightArmObj){
                    leftArmObj = selected;
                    exist = true;
                }
            }
            if (!exist) {
                leftArmObj = -1;
                Debug.Log("REMOVEDLeft");
                }
            break;

            case "Right":
             for (int i = 0; i < Inventory.Count; i++)
            {
                int selected = (i + 1 + RightArmObj)%Inventory.Count;
                if(Inventory[selected].Count>0&&selected != leftArmObj){
                    RightArmObj = selected;
                    exist = true;
                }
            }
            if (!exist){
                RightArmObj = -1;
                Debug.Log("REMOVEDRIGHT");
            }
            Debug.Log(RightArmObj);
            
            break;
        }
    }


    private void UseCurrentBubble(string Side){
        switch (Side){
            case "Left":
               
                    ActivatedBlob = Inventory[leftArmObj].Object;
                    Inventory[leftArmObj].Count--;
                    if(Inventory[RightArmObj].Count<1) SwitchBubble2 ("Left");
                
                    
               

            break;
            case "Right":
                
                    ActivatedBlob = Inventory[RightArmObj].Object;
                    Inventory[RightArmObj].Count--;
                    if(Inventory[RightArmObj].Count<1) SwitchBubble2("Right");
                
            break;
        }
    }

    private void RefreshHands(){
        if (leftArmObj > Inventory.Count-1){
            if (RightArmObj != 0) leftArmObj = 0;
            else if (RightArmObj == 0 && Inventory.Count>2) leftArmObj = 1;
            else if (Inventory.Count<=1) leftArmObj = -1;
        }
    }


    private void Update() {
        
        if (Input.GetAxis("MouseLeft")>0) {
            LMBcurrent = Mathf.Clamp(LMBcurrent+Time.deltaTime, 0, LMBMax);
        }
        else checkMouseInput("Left");

        if(Input.GetAxis("MouseRight")>0) RMBcurrent = Mathf.Clamp(RMBcurrent+Time.deltaTime, 0, RMBMax);
        else checkMouseInput("Right");
        
        if (GetGameObjectInCenter(4f, true, Color.red, 1f)!=currentCenteredGameObject){
            OutlineVisibilityManager newManager;
           if(currentCenteredGameObject!=null && currentCenteredGameObject.TryGetComponent<OutlineVisibilityManager>(out newManager)){
               newManager.myOutlineObject.GetComponent<Outline>().enabled = false;
           }
           currentCenteredGameObject = GetGameObjectInCenter(4f, true, Color.red, 1f);
           if (currentCenteredGameObject!=null && currentCenteredGameObject.TryGetComponent<OutlineVisibilityManager>(out newManager)){
                 newManager.myOutlineObject.GetComponent<Outline>().enabled = true;
           }
        }

        if(Input.GetKeyDown(KeyCode.E)){
            SanctuaryTip myTip;
            if (currentCenteredGameObject.TryGetComponent<SanctuaryTip>(out myTip)){
                TipsInventory.Add (myTip.mySecret);
                Destroy(currentCenteredGameObject);
                currentCenteredGameObject = null;
            }
        }
       
       if(Input.GetKeyDown(KeyCode.Tab)){
           switch(PlayerUI.GetComponent<WidgetSwitcher>().GetActiveWidgetIndex()){
               case (0):
               PlayerUI.GetComponent<WidgetSwitcher>().SetActiveWidgetIndex(1);
               break;
               case(1):
               PlayerUI.GetComponent<WidgetSwitcher>().SetActiveWidgetIndex(0);
               break;

           }

       }

        
    }
}
