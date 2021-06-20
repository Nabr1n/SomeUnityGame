using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class FloorScript : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public bool AmIMainRoad;
    [SerializeField] private GameObject[] Rocks;
    [SerializeField] int MaxRockCount, MinRockCount;

    [SerializeField] float MinRocksScale, MaxRockScale;

    public GameObject Celling;

    //public int myLevel;
   
    public GameObject WallLeft;
    
    public Transform LeftWallTransform;
    public GameObject WallBottom;

    public GameObject Floor;

    [SerializeField] private GameObject[] Floors;
    [SerializeField] private GameObject[] Walls;

    [SerializeField] private GameObject Sanctuary, SanctuaryHolder;

    [SerializeField] public GameObject floortransform;

    public bool bIsSanctuartyCenter;
    [SerializeField] private Transform BlobPlace;
    [SerializeField] private GameObject PickUpToInstantiate;

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

    public void PlaceSanctuary(int Level){
        SanctuaryGenerator generator = Instantiate (Sanctuary, SanctuaryHolder.transform).GetComponent<SanctuaryGenerator>() ;
        generator.MyLevel = Level;
        generator.OnMySpawn(Level);
    }

    private void SpawnRocks(){
        int count = Random.Range(MinRockCount, MaxRockCount);
        for (int i = 0; i < count; i++)
        {
           GameObject newRock = Instantiate(Rocks[Random.Range(0, Rocks.Length)], 
                            transform.position + new Vector3(Random.Range(-5f*transform.localScale.x, 5f*transform.localScale.x), 2f, Random.Range(-5f*transform.localScale.z, 5f*transform.localScale.z)), Quaternion.identity);
            float RockScale = Random.Range(MinRocksScale, MaxRockScale);
            newRock.transform.localScale = new Vector3 (RockScale, RockScale, RockScale);
            newRock.AddComponent<Rigidbody>();
            //newRock.GetComponent<Rigidbody>().isKinematic = true;
            newRock.AddComponent<MeshCollider>();
            newRock.GetComponent<MeshCollider>().sharedMesh = newRock.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh;
            newRock.GetComponent<MeshCollider>().convex = true;
            
        }
    }

    

    [HideInInspector] public bool MazeExit;

    public void SetRoad(bool IsMainRoad){
        AmIMainRoad = IsMainRoad;
        
            
    }
    

    private void PickRandomFloor(){
        int Rand = Random.Range(0, Floors.Length-1);
        Floor = Instantiate(Floors[Rand], floortransform.transform);
    }

    public void CheckBlob(bool ShouldBeWithBlob, string BlobType = "none"){
        if(ShouldBeWithBlob){
            GameObject newBlob = Instantiate(PickUpToInstantiate, BlobPlace);
            newBlob.GetComponent<BlobPickUp>().OnInstantiate(BlobType);
        }
    }

     void Start()
    {
        PickRandomFloor();
        SetWalls();
        SpawnRocks();
        
    }
    
    // Update is called once per frame
    void Update()
    {
        //if (MazeExit) myPlane.GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
