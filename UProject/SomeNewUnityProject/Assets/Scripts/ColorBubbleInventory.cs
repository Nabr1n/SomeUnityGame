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


[System.Serializable]
public class StringToColor{
    public Color Color;
    public string Code;
}


public class ColorMath{
    

    public string MixUpColors(string Color1, string Color2 = "null"){
        string FinalColor = "null";
        if((Color2 == "null" && Color1 != "null")||(Color1 == "null" && Color2 != "null")){
            FinalColor = Color1;
        }
        else{
            if(Color1 == "Red"){
                if(Color2 == "Blue") FinalColor = "Purple";
                if (Color2 == "Yellow") FinalColor = "Orange";
                
            }
            if(Color1 == "Blue"){
                if(Color2 == "Red") FinalColor = "Purple";
                if (Color2 == "Yellow") FinalColor = "Green";
                
            }
            if(Color1 == "Yellow"){
                if(Color2 == "Red") FinalColor = "Orange";
                if (Color2 == "Blue") FinalColor = "Green";
                
            }
        }

        return FinalColor;
    }

    public List<string> GetBasicColorsFromMixed(string MixedColorCode){
        List<string> returnList = new List<string>();
        switch (MixedColorCode){
            case ("Red"):
            returnList.Add("Red");
            break;

            case ("Blue"):
            returnList.Add("Blue");
            break;

            case ("Yellow"):
            returnList.Add("Yellow");
            break;

            case ("Green"):
            returnList.Add("Blue");
            returnList.Add("Yellow");
            break;

            case ("Purple"):
            returnList.Add("Red");
            returnList.Add("Blue");
            break;

            case ("Orange"):
            returnList.Add("Red");
            returnList.Add("Yellow");
            break;

        }



        return returnList;
    }
}
    

public class ColorBubbleInventory : MonoBehaviour
{
    public List<InventoryBubble> Inventory = new List<InventoryBubble>();
    public List<SecretColor> TipsInventory = new List<SecretColor>();

    public List<StringToColor> CodeList = new List<StringToColor>();

    public ColorMath MyColorMath = new ColorMath();
    public string myActivatedColor;
    
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
                    CheckBlobUse("Left");
                    
                    
                }
                else LMBcurrent = 0;
            break;
            case "Right":
                if (RMBcurrent >= RMBMax/2 && RMBcurrent < RMBMax)
                {
                    SwitchBubble2("Right");
                    RMBcurrent = 0;
                

                }
                else if(RMBcurrent >= RMBMax){
                    CheckBlobUse("Right");
                    
                    
                    
                //действие 2
                }
                else RMBcurrent = 0;
            break;
        }
        
    }

    private void UseBlob(string Side){

        SantuaryKeyHolder myKeyHolder;

        if(currentCenteredGameObject.TryGetComponent<SantuaryKeyHolder>(out myKeyHolder)){
        CheckSpheresColor();
        
        
        switch (Side){
        case ("Left"):
            myKeyHolder.AddMyBlob(myActivatedColor, MyActivatedColorToColor(myActivatedColor));
            RemoveBlob("Left");
            LMBcurrent = 0;
        break;


        case ("Right"):
            myKeyHolder.AddMyBlob(myActivatedColor, MyActivatedColorToColor(myActivatedColor));
            RemoveBlob("Right");
            RMBcurrent = 0;
        break;


        case ("Both"):
            myKeyHolder.AddMyBlob(myActivatedColor, MyActivatedColorToColor(myActivatedColor));
            RemoveBlob("Both");
            LMBcurrent = 0;
            RMBcurrent = 0;
        break;
        }
        }
        else{
            switch (Side){
        case ("Left"):
            
            
            LMBcurrent = 0;
        break;


        case ("Right"):
            
            
            RMBcurrent = 0;
        break;


        case ("Both"):
           
            
            LMBcurrent = 0;
            RMBcurrent = 0;
        break;
        }
    }
    }

    private void RemoveBlob(string Side){
        switch(Side){
            case("Left"):
                Inventory[leftArmObj].Count--;
                if(!(Inventory[leftArmObj].Count>0)) SwitchBubble2("Left");
            
            break;

            case ("Right"):
                Inventory[RightArmObj].Count--;
                if(!(Inventory[RightArmObj].Count>0)) SwitchBubble2("Right");
            break;

            case("Both"):
                Inventory[leftArmObj].Count--;
                Inventory[RightArmObj].Count--;
                if(!(Inventory[leftArmObj].Count>0)) SwitchBubble2("Left");
                if(!(Inventory[RightArmObj].Count>0)) SwitchBubble2("Right");
            break;
        }
    }



    private void CheckBlobUse(string Side){
        switch (Side){
            case ("Left"):
            if(LMBcurrent>=LMBMax){
                if(RMBcurrent>=RMBMax){
                    UseBlob("Both");
                }
                else UseBlob("Left");
            }
            break;

            case ("Right"):
            if(RMBcurrent>=RMBMax){
                if(LMBcurrent>=LMBMax){
                    UseBlob("Both");
                }
                else UseBlob("Right");
            }

            break;
        }
    }




    private RaycastHit CheckForward(float distance, bool withDebug, Color DebugColor, float DebugDrawTime){
        RaycastHit hit;
        Physics.Raycast (myCamera.transform.position, myCamera.transform.forward,  out hit, distance);
        //Physics.CapsuleCast(myCamera.transform.position + myCamera.transform.forward*1f, myCamera.transform.forward*distance, 0.5f ,myCamera.transform.forward, out hit, distance);
        //Debug.Log(hit.collider);
        if (withDebug) Debug.DrawRay (hit.point, Vector3.up, Color.red, 30f);
        
        
        return hit;

    }

    private GameObject GetGameObjectInCenter (float distance, bool withDebug, Color DebugColor, float DebugDrawTime){
        GameObject CenteredObject = null;
        if(CheckForward(distance, withDebug, DebugColor, DebugDrawTime).collider!=null) CenteredObject = 
                                                        CheckForward(distance, withDebug, DebugColor, DebugDrawTime).collider.gameObject;
        
        


        return CenteredObject;
    }

    public Color MyActivatedColorToColor(string Code){
        Color color = Color.black;
        for (int i = 0; i < CodeList.Count; i++)
        {
            Debug.Log(Code+ ":   " + CodeList[i]);
            if(CodeList[i].Code == Code){
                color = CodeList[i].Color;
                Debug.Log(color);
                break;
            }
        }
        return color;
    }


     private void CheckSpheresColor(){
        BlobObject LeftBlob, RightBlob = null;
        LeftBlob = Inventory[leftArmObj].Object;
        RightBlob = Inventory[RightArmObj].Object;
        



        if (LMBcurrent >= LMBMax && RMBcurrent >= RMBMax) myActivatedColor = MyColorMath.MixUpColors(LeftBlob.MyColorName, RightBlob.MyColorName); 
        else if (LMBcurrent >= LMBMax && !(RMBcurrent >= RMBMax)) myActivatedColor = MyColorMath.MixUpColors(LeftBlob.MyColorName); 
        else if (!(LMBcurrent >= LMBMax) && RMBcurrent >= RMBMax) myActivatedColor = MyColorMath.MixUpColors(RightBlob.MyColorName); 
        else myActivatedColor = "null";

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

    private void FindUnassignedColor(string Hand){
        switch (Hand){
            case ("Left"):
            for (int i = 0; i < Inventory.Count; i++)
            {
                if(Inventory[i].Count > 0 && RightArmObj != i){
                    leftArmObj = i;
                    break;
                }
            }
            break;

            case("Right"):

            for (int i = 0; i < Inventory.Count; i++)
            {
                if(Inventory[i].Count > 0 && leftArmObj != i){
                    RightArmObj = i;
                    break;
                }
            }

            break;
        }
    }

    private void SwitchBubble2(string Hand){
        bool exist = false;
        
        switch (Hand){
            case "Left":
            if(leftArmObj!=-1){
            for (int i = 0; i < Inventory.Count; i++)
            {
                int selected = (i  + 1 +leftArmObj)%Inventory.Count;
                if(Inventory[selected].Count>0&&selected != RightArmObj){
                    leftArmObj = selected;
                    exist = true;
                    break;
                }
            }
            if (!exist) {
                leftArmObj = -1;
                //Debug.Log("REMOVEDLeft");
                }
            }
            else FindUnassignedColor("Left");
            break;

            case "Right":
             
             
            if(RightArmObj != -1 ){

             for (int i = 0; i < Inventory.Count; i++)
                {
                    int selected = (i  + 1 + RightArmObj)%(Inventory.Count);
                    Debug.Log(selected + ". RightArmObj = " + RightArmObj + ".  All count = " + Inventory.Count + " i = " + i );
                    if(Inventory[selected].Count>0&&selected != leftArmObj){
                        RightArmObj = selected;
                        exist = true;
                        break;
                    }
                }
            if (!exist){
                RightArmObj = -1;
                //Debug.Log("REMOVEDRIGHT");
            }
            //Debug.Log(RightArmObj);
            }
            else FindUnassignedColor("Right");
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
            if(leftArmObj==-1) SwitchBubble2("Left");
                
            
            if(leftArmObj!=-1) LMBcurrent = Mathf.Clamp(LMBcurrent+Time.deltaTime, 0, LMBMax);


        }
        else checkMouseInput("Left");

        if(Input.GetAxis("MouseRight")>0){
             if(RightArmObj==-1) SwitchBubble2("Right");
                
            
            if(RightArmObj!=-1) RMBcurrent = Mathf.Clamp(RMBcurrent+Time.deltaTime, 0, RMBMax);
        

            
            
            
        
        
        }

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


        CheckSpheresColor();
       }

        
    }
}
