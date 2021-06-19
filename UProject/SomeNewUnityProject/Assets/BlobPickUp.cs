using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobPickUp : MonoBehaviour
{
    [System.Serializable]
    private class BlobSelection{
        public BlobObject BlobObject;
        public string Code;
    }




    [SerializeField] string ColorType;
    [SerializeField] private BlobObject PickUpOBj;
    [SerializeField] private List<BlobSelection> AllBlobList = new List<BlobSelection>();
    [SerializeField] private int PickUpCount;
    [SerializeField] private GameObject myBooma;
   
   

    public void OnInstantiate(string ColorCode){
        ColorType = ColorCode;
        for (int i = 0; i < AllBlobList.Count; i++)
        {
            if(ColorType == AllBlobList[i].Code){
                PickUpOBj = AllBlobList[i].BlobObject;
                break;
            }
        }
        
        myBooma.GetComponent<ColoredSphere>().UpdateMyColor(PickUpOBj.MyColor);

    }


    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<ColorBubbleInventory>().PickUP(PickUpOBj, PickUpCount);
            Destroy(gameObject);
        }
    }
}
