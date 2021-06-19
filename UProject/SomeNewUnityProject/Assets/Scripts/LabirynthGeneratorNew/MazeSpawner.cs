using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] GameObject CellPrefab;
    [SerializeField] GameObject CellPrefabSecondLevel;
    public float CellSize;

    public int MazeWidth, MazeLength, SanctuarySideLength, ClosedSanctuarySideLen;
    private MazeGeneratorCellNew[,] maze, secondmaze;
    public bool ShouldSanctuaryBeClosed = true;
    public bool WithDelay;

    public int ElevatorShaftLength;

    private GameObject SanctuaryCenter;

    [System.Serializable]
    
    public class BlobsDict{
        public string Name;
        public BlobObject Blob;
    
    }

    [SerializeField] private BlobsDict[] Blobs;

    void Start()
    {
        MazeGeneratorNew generator = new MazeGeneratorNew();
        maze = generator.GenerateNewMaze(MazeWidth, MazeLength, SanctuarySideLength, ShouldSanctuaryBeClosed, ClosedSanctuarySideLen);
        secondmaze = generator.GenerateNewMaze(MazeWidth, MazeLength, SanctuarySideLength, ShouldSanctuaryBeClosed, ClosedSanctuarySideLen);
        StartCoroutine(SpawnMaze());    

        GlobalSettings.GameGlobalSettings.FloorsSize = 10*CellSize;
    }

    private IEnumerator SpawnMaze(){
        for (int w = 0; w < maze.GetLength(0); w++)
        {
            for (int l = 0; l < maze.GetLength(1); l++)
            {
                FloorScript F = Instantiate(CellPrefab, new Vector3(w*10*CellSize,0,l*10*CellSize),Quaternion.identity).GetComponent<FloorScript>();
                F.transform.localScale = new Vector3 (CellSize, CellSize, CellSize);
                if(w==0&&l==0&&GameObject.FindWithTag("Player")!=null) GameObject.FindWithTag("Player").transform.position = F.transform.position;
                F.WallLeft.SetActive(maze[w,l].UnbreakableWallLeft?maze[w,l].UnbreakableWallLeft : maze[w,l].WallLeft );
                F.WallBottom.SetActive(maze[w,l].UnbreakableWallBottom ? maze[w,l].UnbreakableWallBottom : maze[w,l].WallBottom);
                F.MazeExit = maze[w,l].MazeExit;
                //F.CheckBlob(maze[w,l].ShouldBeWithBlob, maze[w,l].BlobType);
                if ( maze[w,l].AmISanctuaryCenter) F.floortransform.SetActive(false);
                if(maze[w,l].ShouldBeWithBlob) GlobalSettings.GameGlobalSettings.AllCellsWithBlobsFirstLevel.Add(F.gameObject);
                F.bIsSanctuartyCenter = maze [w,l].AmISanctuaryCenter;
                if(maze[w,l].AmISanctuaryCenter) {
                    F.PlaceSanctuary();
                    SanctuaryCenter = F.gameObject;
                }

                if(maze[w,l].AmIInClosedSanctuary && !maze[w,l].AmISanctuaryCenter) {
                    GlobalSettings.GameGlobalSettings.ClosedSanctuaryFloors.Add(F.gameObject);
                    //F.floortransform.SetActive(false);
                }
                if(maze[w,l].AmISanctuary) GlobalSettings.GameGlobalSettings.SanctuaryFloors.Add(F.gameObject);

                if(!maze[w,l].AmISanctuary)  GlobalSettings.GameGlobalSettings.NotSanctuaryFloors.Add(F.gameObject);

            



                // if(maze[w,l].BarrierLeft){
                //     F.BarrierLeft.SetActive(true);
                //     F.BarrierLeft.GetComponent<BarrierScript>().myKeyBlob = FindBlobByString(maze[w,l].BarrierLeftObj);
                // }
                // else F.BarrierLeft.SetActive(false);

                // if(maze[w,l].BarrierBottom){
                //     F.BarrierBottom.SetActive(true);
                //     F.BarrierBottom.GetComponent<BarrierScript>().myKeyBlob = FindBlobByString(maze[w,l].BarrierBottomObj);
                // }
                // else F.BarrierBottom.SetActive(false);

                
                //F.CheckBlob(false, "Green");
                if(WithDelay) yield return null;
                
            }
        }
        Vector3 newMazeStartingPoint= SanctuaryCenter.transform.position - new Vector3(0,10f*CellSize, 0);
        Vector3 startingElevatorplace = SanctuaryCenter.transform.position - new Vector3(0,10f*CellSize, 0);
        for (int i = 0; i < ElevatorShaftLength; i++)
        {
            Vector3 curentFloorCenterPos = startingElevatorplace - new Vector3(0,10f*CellSize*i,0);
            
            GameObject GameObject1 = Instantiate(CellPrefab, curentFloorCenterPos, Quaternion.identity);
            GameObject GameObject2 = Instantiate(CellPrefab, curentFloorCenterPos + new Vector3(10*CellSize, 0, 0), Quaternion.identity);
            GameObject GameObject3 = Instantiate(CellPrefab, curentFloorCenterPos + new Vector3(0, 0, 10*CellSize), Quaternion.identity);
            GameObject1.transform.localScale = new Vector3 (CellSize, CellSize, CellSize);
            GameObject2.transform.localScale = new Vector3 (CellSize, CellSize, CellSize);
            GameObject3.transform.localScale = new Vector3 (CellSize, CellSize, CellSize);
            GameObject1.GetComponent<FloorScript>().floortransform.SetActive(false);
            GameObject2.GetComponent<FloorScript>().floortransform.SetActive(false);
            GameObject3.GetComponent<FloorScript>().floortransform.SetActive(false);
            newMazeStartingPoint = curentFloorCenterPos;
        }
        
        newMazeStartingPoint -= new Vector3 (0, 10f*CellSize, 0);
        
        for (int w = 0; w < maze.GetLength(0); w++)
        {
            for (int l = 0; l < secondmaze.GetLength(1); l++)
            {
                FloorScript F = Instantiate(CellPrefabSecondLevel, newMazeStartingPoint + new Vector3(w*10*CellSize,0,l*10*CellSize),Quaternion.identity).GetComponent<FloorScript>();
                if (w == 0 && l == 0) F.Celling.SetActive(false);
                F.transform.localScale = new Vector3 (CellSize, CellSize, CellSize);
                //if(w==0&&l==0&&GameObject.FindWithTag("Player")!=null) GameObject.FindWithTag("Player").transform.position = F.transform.position;
                F.WallLeft.SetActive(secondmaze[w,l].UnbreakableWallLeft?secondmaze[w,l].UnbreakableWallLeft : secondmaze[w,l].WallLeft );
                F.WallBottom.SetActive(secondmaze[w,l].UnbreakableWallBottom ? secondmaze[w,l].UnbreakableWallBottom : secondmaze[w,l].WallBottom);
                F.MazeExit = secondmaze[w,l].MazeExit;
                //F.CheckBlob(maze[w,l].ShouldBeWithBlob, maze[w,l].BlobType);
                if ( secondmaze[w,l].AmISanctuaryCenter) F.floortransform.SetActive(false);
                //if(secondmaze[w,l].ShouldBeWithBlob) GlobalSettings.GameGlobalSettings.AllCellsWithBlobsFirstLevel.Add(F.gameObject);
                F.bIsSanctuartyCenter = secondmaze [w,l].AmISanctuaryCenter;
                if(secondmaze[w,l].AmISanctuaryCenter) {
                    F.PlaceSanctuary();
                    SanctuaryCenter = F.gameObject;
                }

                if(secondmaze[w,l].AmIInClosedSanctuary && !secondmaze[w,l].AmISanctuaryCenter) {
                    //GlobalSettings.GameGlobalSettings.ClosedSanctuaryFloors.Add(F.gameObject);
                    //F.floortransform.SetActive(false);
                }
                //if(secondmaze[w,l].AmISanctuary) GlobalSettings.GameGlobalSettings.SanctuaryFloors.Add(F.gameObject);

                //if(!secondmaze[w,l].AmISanctuary)  GlobalSettings.GameGlobalSettings.NotSanctuaryFloors.Add(F.gameObject);

            



                // if(maze[w,l].BarrierLeft){
                //     F.BarrierLeft.SetActive(true);
                //     F.BarrierLeft.GetComponent<BarrierScript>().myKeyBlob = FindBlobByString(maze[w,l].BarrierLeftObj);
                // }
                // else F.BarrierLeft.SetActive(false);

                // if(maze[w,l].BarrierBottom){
                //     F.BarrierBottom.SetActive(true);
                //     F.BarrierBottom.GetComponent<BarrierScript>().myKeyBlob = FindBlobByString(maze[w,l].BarrierBottomObj);
                // }
                // else F.BarrierBottom.SetActive(false);

                
                //F.CheckBlob(false, "Green");
                if(WithDelay) yield return null;
                
            }
        }




        yield return new WaitForSeconds (0.5f);
        GlobalSettings.GameGlobalSettings.SpawnColorBlobs(GlobalSettings.GameGlobalSettings.AllBasicColorsToBePlaced, GlobalSettings.GameGlobalSettings.AllCellsWithBlobsFirstLevel);
        
        yield return null;
    }

    public BlobObject FindBlobByString(string Name){
        BlobObject blob = null;
        
        for (int i = 0; i < Blobs.Length; i++)
        {
            if (Blobs[i].Name == Name){
                
                blob =  Blobs[i].Blob;
            }
        }
        return blob;
    }

    void Update()
    {
        
    }
}
