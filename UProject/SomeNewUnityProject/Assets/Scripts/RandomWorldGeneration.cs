
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.CoreModule;


public struct Floor
{
    public GameObject Object;
    public int Width;
    public int Length;

    public bool IsExisting;

    public FloorScript ObjFloorScript;

    public Floor ( GameObject obj, int width, int length)
    {
        Object = obj;
        Width = width;
        Length = length;
        ObjFloorScript = Object!=null? Object.GetComponent<FloorScript>(): null;
        IsExisting = Object!=null? true:false;
    }
}
public class RandomWorldGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Floor;
    [SerializeField] private GameObject Wall;
    
    [SerializeField] float tileSize;
    [SerializeField] int gridLength;

    [SerializeField] int gridWidth;
    [SerializeField] bool ShoudGenerateInstantly;
    
    
    private List<Floor> FloorRefs = new List<Floor>();
    private List<Floor> RoadFloorRefs = new List<Floor>();






    private Floor GetTileAtIndex(int width, int length){
        Floor CurrentFloor = new Floor (null, -1, -1);
        
        for (int i = 0; i < FloorRefs.Count; i++)
        {
            if(FloorRefs[i].Length == length&& FloorRefs[i].Width == width){
                CurrentFloor = FloorRefs[i];
            }
        }

        return CurrentFloor;
    }

    private void SpawnFloor(Vector3 location, int width, int length){
        GameObject newfloor = Instantiate (Floor, location, Quaternion.identity);
        newfloor.transform.localScale = new Vector3 (tileSize, tileSize, tileSize);
        
        FloorRefs.Add(new Floor(newfloor, width, length));
    }

    private IEnumerator placeFloors(){

        Vector3 startplace = transform.position - new Vector3(gridWidth*tileSize/2f, 0, gridLength*tileSize/2f);
        //Debug.DrawLine(startplace, startplace + new Vector3(gridWidth * tileSize, 0,0), Color.red, 10f);
        //Debug.DrawLine(startplace, startplace + new Vector3(0, 0,gridLength * tileSize), Color.blue, 10f);
        

        
        for (int w = 0; w < gridWidth; w++)
        {
            for (int l = 0; l < gridLength; l++)
            {
                SpawnFloor(startplace + new Vector3(10*tileSize * w, 0 , 10 * tileSize * l), w, l);
                if(!ShoudGenerateInstantly)
                    yield return null;
            }
        }
        yield return null;
        StartCoroutine(MakeMainRoad());
    }

    private Vector2Int Random1(){
        int RandomGen = Random.Range(0,3);
        Vector2Int returnVector = new Vector2Int (0,0);

        switch (RandomGen)
        {
            case 0:
            returnVector =  new Vector2Int(-1, 0);
            break;
            case 1:
            returnVector = new Vector2Int(1, 0);
            break;
            case 2:
            returnVector = new Vector2Int(0, 1);
            break;
            case 3:
            returnVector = new Vector2Int(0, -1);
            break;
        }
        return returnVector;
    }

    private Floor MakeFloorRoad(int width, int length, bool IsItAFirst = false){
        Floor ReturnGameObj = new Floor(null, -1, -1);
        for (int i = 0; i < FloorRefs.Count; i++)
        {
            if(FloorRefs[i].Length == length&& FloorRefs[i].Width == width){
                FloorRefs[i].Object.GetComponent<FloorScript>().SetRoad(true);
                ReturnGameObj = FloorRefs[i];
            }
        }
        return ReturnGameObj;
    }
    
    private void EnteringTile(){
        int RandomSide = Random.Range(0,3);
        //int RandomSideIndex;
        switch (RandomSide)
        {
            case 0:
               RoadFloorRefs.Add( MakeFloorRoad(0,Random.Range(0,gridLength-1)));
            break;
            case 1:
               RoadFloorRefs.Add( MakeFloorRoad(Random.Range(0,gridWidth-1),gridLength-1));
            break;
            case 2:
                RoadFloorRefs.Add(MakeFloorRoad(gridWidth-1,Random.Range(0,gridLength-1)));
            break;
            case 3:
                RoadFloorRefs.Add(MakeFloorRoad(Random.Range(0,gridWidth-1),0));
            break;
        }
        Debug.Log(RoadFloorRefs[0].Length + " " + RoadFloorRefs[0].Width);
    }

    private IEnumerator MakeMainRoad(){
        bool Created = false;
        int PrevTileIndex = 0;
        EnteringTile(); 
        while (!Created){
            int CurrentCheckWidth = RoadFloorRefs[PrevTileIndex].Width;
            int CurrentCheckLength = RoadFloorRefs[PrevTileIndex].Length;
            Vector2Int randomednum = Random1();
            if(GetTileAtIndex(CurrentCheckWidth + randomednum.x, CurrentCheckLength + randomednum.y).IsExisting){
                //GetTileAtIndex(CurrentCheckWidth + randomednum.x, CurrentCheckLength + randomednum.y).ObjFloorScript.SetRoad(true);
            } 
            
        }


        yield return null;
    }


    
    void Start()
    {
        StartCoroutine(placeFloors());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
