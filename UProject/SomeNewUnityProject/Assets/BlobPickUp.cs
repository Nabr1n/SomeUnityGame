using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobPickUp : MonoBehaviour
{
    [SerializeField] private BlobObject PickUpOBj;
    [SerializeField] private int PickUpCount;
   
   
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<ColorBubbleInventory>().PickUP(PickUpOBj, PickUpCount);
            Destroy(gameObject);
        }
    }
}
