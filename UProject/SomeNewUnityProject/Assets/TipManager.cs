using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipManager : MonoBehaviour
{
    private GameObject Player;
    private ColorBubbleInventory Inventory;
    private List<GameObject> Tips = new List<GameObject>();
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
            List<GameObject> ObjectsToDelete = new List<GameObject>();
            for (int i = 0; i < Tips.Count; i++)
            {
                if(i >= Inventory.TipsInventory.Count) ObjectsToDelete.Add(Tips[i]);
            }
            for (int i = ObjectsToDelete.Count-1; i < 0; i--)
            {
                Destroy(ObjectsToDelete[i]);
            }
        }

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
