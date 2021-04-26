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

        Debug.Log("ITEMADDED!");
    }


}
