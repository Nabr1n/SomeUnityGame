using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class FloorScript : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public bool AmIMainRoad;
    [SerializeField] private GameObject[] Rocks, FloorDecor, WallDecor, FloorDecorBig;
    [SerializeField] int MaxRockCount, MinRockCount, MinFloorBigCount, MaxFloorBigCount, MinFloorSmallCount, MaxFloorSmallCount;

    [SerializeField] float MinRocksScale, MaxRockScale, MinWallDecorScale, MaxWallDecorScale, MinFloorDecorScale, MaxFloorDecorScale, MinFloorDecorBigScale, MaxFloorDecorBigScale;

    public GameObject Celling;

    //public int myLevel;
   
    public GameObject WallLeft;
    
    public Transform LeftWallTransform;
    public GameObject WallBottom;

    public GameObject Floor;

    [SerializeField] Transform[] FloorDecorPlacementBig, FloorDecorPlacementSmall;
    
    [SerializeField] private GameObject[] Floors;
    [SerializeField] private GameObject[] Walls;

    [SerializeField] private GameObject Sanctuary, SanctuaryHolder;

    [SerializeField] public GameObject floortransform;

    public bool bIsSanctuartyCenter;
    public bool AmIInClosedSanctuary;

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

    private void PlaceFloorDecor(GameObject[] objects, Transform[] transforms, float Minscale, float MaxScale, int count){
        Debug.Log("Spawnin1");
        List<GameObject> LockedObjects = new List<GameObject>();
        List<Transform> lockedTransforms = new List<Transform>();
        
        if(count > 0){
        for (int i = 0; i < count; i++)
        {
            
            Transform currentTransform;
            GameObject currentGameObjectToInstantiate;

            while (true){
                int Randint = Random.Range(0, objects.Length);
                if(!LockedObjects.Contains(objects[Randint])){
                    currentGameObjectToInstantiate = objects[Randint];
                    LockedObjects.Add(currentGameObjectToInstantiate);
                    break;

                }
            }
             while (true){
                int Randint = Random.Range(0, transforms.Length);
                if(!lockedTransforms.Contains(transforms[Randint])){
                    currentTransform = transforms[Randint];
                    lockedTransforms.Add(currentTransform);
                    break;

                }
            }
            GameObject G = Instantiate (currentGameObjectToInstantiate, currentTransform.position, Quaternion.identity);
            float scale = Random.Range(Minscale, MaxScale);
            G.transform.localScale = new Vector3(scale, scale, scale);



        }
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

    public void OnMyStart()
    {
        PickRandomFloor();
        SetWalls();
        SpawnRocks();
        if(!AmIInClosedSanctuary){
        PlaceFloorDecor(FloorDecorBig, FloorDecorPlacementBig,MinFloorDecorBigScale, MaxFloorDecorBigScale,Random.Range(MinFloorBigCount, MaxFloorBigCount));
        PlaceFloorDecor(FloorDecor, FloorDecorPlacementSmall,MinFloorDecorScale, MaxFloorDecorScale, Random.Range(MinFloorSmallCount, MaxFloorSmallCount));
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        //if (MazeExit) myPlane.GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
