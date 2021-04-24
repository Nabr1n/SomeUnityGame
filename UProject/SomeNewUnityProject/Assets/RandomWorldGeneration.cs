using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public struct Floor{
public GameObject Object;
public int Width;
public int Length;

public Floor ( GameObject obj, int width, int length){
    Object = obj;
    Width = width;
    Length = length;
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

    
    
    private List<Floor> FloorRefs = new List<Floor>();
    private List<Floor> RoadFloorRefs = new List<Floor>();



    private void SpawnFloor(Vector3 location, int width, int length){
        GameObject newfloor = Instantiate (Floor, location, Quaternion.identity);
        newfloor.transform.localScale = new Vector3 (tileSize, tileSize, tileSize);
        
        FloorRefs.Add(new Floor(newfloor, width, length));
    }

    private IEnumerator placeFloors(){

        Vector3 startplace = transform.position - new Vector3(gridWidth*tileSize/2f, 0, gridLength*tileSize/2f);
        Debug.DrawLine(startplace, startplace + new Vector3(gridWidth * tileSize, 0,0), Color.red, 10f);
        Debug.DrawLine(startplace, startplace + new Vector3(0, 0,gridLength * tileSize), Color.blue, 10f);
        

        
        for (int w = 0; w < gridWidth; w++)
        {
            for (int l = 0; l < gridLength; l++)
            {
                SpawnFloor(startplace + new Vector3(10*tileSize * w, 0 , 10 * tileSize * l), w, l);
                yield return null;
            }
        }
        StartCoroutine(MakeMainRoad());
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
        EnteringTile();


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
