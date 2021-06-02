using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] GameObject CellPrefab;
    [SerializeField] float CellSize;

    public int MazeWidth, MazeLength, SanctuarySideLength;
    private MazeGeneratorCellNew[,] maze;
    [System.Serializable]
    public class BlobsDict{
        public string Name;
        public BlobObject Blob;
    
    }

    [SerializeField] private BlobsDict[] Blobs;

    void Start()
    {
        MazeGeneratorNew generator = new MazeGeneratorNew();
        maze = generator.GenerateNewMaze(MazeWidth, MazeLength, SanctuarySideLength);
        StartCoroutine(SpawnMaze());    
    }

    private IEnumerator SpawnMaze(){
        for (int w = 0; w < maze.GetLength(0); w++)
        {
            for (int l = 0; l < maze.GetLength(1); l++)
            {
                FloorScript F = Instantiate(CellPrefab, new Vector3(w*10*CellSize,0,l*10*CellSize),Quaternion.identity).GetComponent<FloorScript>();
                F.transform.localScale = new Vector3 (CellSize, CellSize, CellSize);
                //if(w==0&&l==0) GameObject.FindWithTag("Player").transform.position = F.transform.position;
                F.WallLeft.SetActive(maze[w,l].WallLeft);
                F.WallBottom.SetActive(maze[w,l].WallBottom);
                F.MazeExit = maze[w,l].MazeExit;
                F.CheckBlob(maze[w,l].ShouldBeWithBlob, maze[w,l].BlobType);
                //if ( maze[w,l].AmISanctuary) F.floortransform.SetActive(false);

                
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
                yield return null;
            }
        }

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
