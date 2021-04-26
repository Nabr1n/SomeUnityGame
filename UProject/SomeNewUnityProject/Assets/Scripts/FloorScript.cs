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
    
    public Transform LeftWallTransform;
    public GameObject WallBottom;

    [SerializeField] private GameObject[] Floors;
    [SerializeField] private GameObject[] Walls;
    [SerializeField] private Transform floortransform;

    [SerializeField] private Transform BlobPlace;
    [SerializeField] private List<BlobPickUpStruct> blobs;

    public GameObject BarrierLeft,BarrierBottom;
    

    private void SetWalls(){
        int RandLeft = Random.Range(0, Walls.Length -1);
        int RandBottom = Random.Range(0, Walls.Length -1);

        GameObject LeftWallMesh = Instantiate(Walls[RandLeft], WallLeft.transform);
        LeftWallMesh.transform.localPosition = new Vector3 (0,0,0);

        GameObject BottomWallMesh = Instantiate(Walls[RandBottom], WallBottom.transform);
        BottomWallMesh.transform.localPosition = new Vector3 (0,0,0);
        //BottomWallMesh.transform.localScale = 
    }

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
        SetWalls();
    }
    
    // Update is called once per frame
    void Update()
    {
        //if (MazeExit) myPlane.GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
