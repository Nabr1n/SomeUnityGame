using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlobPickUpStruct{
    public string blobkey;
    public GameObject blob;
}

public class FloorScript : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public bool AmIMainRoad;
   
    public GameObject WallLeft;
    public GameObject WallBottom;

    [SerializeField] private GameObject[] Floors;
    [SerializeField] private Transform floortransform;

    [SerializeField] private Transform BlobPlace;
    [SerializeField] private List<BlobPickUpStruct> blobs;

    

    [HideInInspector] public bool MazeExit;

    public void SetRoad(bool IsMainRoad){
        AmIMainRoad = IsMainRoad;
        
            
    }
    

    private void PickRandomFloor(){
        int Rand = Random.Range(0, Floors.Length-1);
        Instantiate(Floors[Rand], floortransform);
    }

    public void CheckBlob(bool ShouldBeWithBlob, string BlobType = "none"){
        if(ShouldBeWithBlob){
            GameObject NeededBlob = null;
            for (int i = 0; i < blobs.Count; i++)
            {
                if(blobs[i].blobkey == BlobType) NeededBlob = blobs[i].blob;
            }
            if(NeededBlob!=null) Instantiate(NeededBlob, BlobPlace);
        }
    }

    void Start()
    {
        PickRandomFloor();
    }

    // Update is called once per frame
    void Update()
    {
        //if (MazeExit) myPlane.GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
