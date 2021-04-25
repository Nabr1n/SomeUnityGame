using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] GameObject CellPrefab;
    private MazeGeneratorCell[,] maze;

    void Start()
    {
        MazeGenerator generator = new MazeGenerator();
        maze = generator.GenerateMaze();
        StartCoroutine(SpawnMaze());    
    }

    private IEnumerator SpawnMaze(){
        for (int w = 0; w < maze.GetLength(0); w++)
        {
            for (int l = 0; l < maze.GetLength(1); l++)
            {
                FloorScript F = Instantiate(CellPrefab, new Vector3(w*10,0,l*10),Quaternion.identity).GetComponent<FloorScript>();
                F.WallLeft.SetActive(maze[w,l].WallLeft);
                F.WallBottom.SetActive(maze[w,l].WallBottom);
                F.MazeExit = maze[w,l].MazeExit;
                yield return null;
            }
        }
    }

  

    void Update()
    {
        
    }
}
