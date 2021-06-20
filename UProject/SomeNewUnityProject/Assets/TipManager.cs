using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipManager : MonoBehaviour
{
    private GameObject Player;
    private ColorBubbleInventory Inventory;
    public List<GameObject> Tips = new List<GameObject>();
    [SerializeField] private GameObject TipPrefab;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Inventory = Player.GetComponent<ColorBubbleInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Inventory.TipsInventory.Count < Tips.Count){
            Debug.Log("DESTROY ALL");
            List<GameObject> ObjectsToDelete = new List<GameObject>();
            ObjectsToDelete = Tips;
            Debug.Log(ObjectsToDelete.Count);
            for (int i = ObjectsToDelete.Count-1; i >= 0; i--)
            {
                Debug.Log("Destroy");
                DestroyImmediate(ObjectsToDelete[i].gameObject);
                
            }
            Tips.Clear();
        }
        Debug.Log(Tips.Count);
        if (Inventory.TipsInventory.Count > Tips.Count){
            for (int i = 0; i < Inventory.TipsInventory.Count; i++)
            {
                if( i >= Tips.Count){
                    Tips.Add(Instantiate(TipPrefab, gameObject.transform));
                    Tips[i].GetComponent<UITipHolder>().UpdateMe(i);
                }
            }
        }
    }
}
