using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneratorCell
{
    public int X;
    public int Y;

    public bool WallLeft = true;
    public bool WallBottom = true;

    public bool BarrierBottom = false;
    public bool BarrierLeft = false;

    public string BarrierLeftObj;
    public string BarrierBottomObj;
    public int DistanceFromStart;

    public bool Visited = false;

    public bool MazeExit = false;

    public bool ShouldBeWithBlob = false;
    
    public string RandomBlob() {
       int Rand = UnityEngine.Random.Range(0,4);
       switch (Rand){
           case 0:
           BlobType = "Green";
           break;
           case 1:
           BlobType = "Red";
           break;
           case 2:
           BlobType = "Yellow";
           break;
           case 3:
           BlobType = "Blue";
           break;
        
       }
       return BlobType;
    }

    public string BlobType = "Null";
    public MazeGeneratorCell(int x, int y){
        X = x;
        Y = y;
    } 
}


public class MazeGenerator
{
    public int Width = 20;
    public int Height = 20;

    public MazeGeneratorCell[,] GenerateMaze(){
        MazeGeneratorCell[,] maze = new MazeGeneratorCell [Width,Height];
        for (int w = 0; w < maze.GetLength(0); w++)
        {
            for (int l = 0; l < maze.GetLength(1); l++)
            {
                maze [w,l] = new MazeGeneratorCell(w, l);
            }
        }

        for (int x = 0; x < maze.GetLength(0); x++){
            maze[x,Height-1].WallLeft = false;
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            maze[Width-1, y].WallBottom = false;
        }



        
        RemoveWallsWithBackTracker (maze);
        PlaceMazeExit(maze);
        PlaceMazeDoors(maze);

        return maze;
    }

    private void PlaceMazeDoors(MazeGeneratorCell[,] maze)
    {
        
    }

    private void RemoveWallsWithBackTracker(MazeGeneratorCell[,] maze){
        MazeGeneratorCell current = maze [0,0];
        bool LastMoveWasTrue = true;

        current.DistanceFromStart = 0;
        current.Visited = true;

        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
        List<MazeGeneratorCell> CellsWithBlobs = new List<MazeGeneratorCell>();
        List<string> Barriers = new List<string>();
        do
        {
            List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();

            int w = current.X;
            int l = current.Y;

            
            if (w > 0 && !maze[w-1, l].Visited) unvisitedNeighbours.Add(maze[w-1, l]);
            if (l > 0 && !maze[w, l-1].Visited) unvisitedNeighbours.Add(maze[w, l-1]);
            if (w < Width - 2 && !maze[w+1, l].Visited) unvisitedNeighbours.Add(maze[w+1, l]);
            if (l < Height - 2 && !maze[w, l+1].Visited) unvisitedNeighbours.Add(maze[w, l+1]);

            if (unvisitedNeighbours.Count > 0){
                MazeGeneratorCell chozen = unvisitedNeighbours [UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                
                RemoveWall(current,chozen);
                if(CheckDoorsAndBlobs(CellsWithBlobs,Barriers).Count>0){
                    int RandIndex = UnityEngine.Random.Range(0,CheckDoorsAndBlobs(CellsWithBlobs,Barriers).Count-1);
                    Barriers.Add(CreatePortal(CheckDoorsAndBlobs(CellsWithBlobs,Barriers)[RandIndex], current,chozen));
                }


                chozen.Visited = true;
                
                stack.Push(chozen);
                float Rand = UnityEngine.Random.Range(0f, 100f);
                if (Rand>=90f) {
                    current.ShouldBeWithBlob = true;
                    current.RandomBlob();
                    CellsWithBlobs.Add(current);
                }
                LastMoveWasTrue = true;
                current = chozen;
                chozen.DistanceFromStart = stack.Count;
                
            }
            else {
                if(LastMoveWasTrue) 
                {
                    current.ShouldBeWithBlob = true;
                    current.RandomBlob();
                    CellsWithBlobs.Add(current);
                    LastMoveWasTrue = false;
                    
                }
                
                current = stack.Pop();
            }

        }
        while (stack.Count>0);

        
    }

    private string CreatePortal(string Type, MazeGeneratorCell current, MazeGeneratorCell chozen)
    {
       if (current.X == chozen.X){
            if (current.Y > chozen.Y) {
                current.BarrierBottom = true;
                current.BarrierBottomObj = Type;
                return Type;
            }
            else{
                chozen.BarrierBottom = true;
                chozen.BarrierBottomObj = Type;
                return Type;
            } 
        }
        else{
            if (current.X > chozen.X){ 
                current.WallLeft = true;
                current.BarrierLeftObj = Type;
                return Type;

            }
            else {
                chozen.BarrierLeft= true;
                chozen.BarrierLeftObj = Type;
                return Type;
            }
        }


        // if (current.X == chozen.X){
        //     if (current.Y > chozen.Y) current.WallBottom = false;
        //     else chozen.WallBottom = false;
        // }
        // else{
        //     if (current.X > chozen.X) current.WallLeft = false;
        //     else chozen.WallLeft = false;
        // }
    }

    private List<string> CheckDoorsAndBlobs(List<MazeGeneratorCell> cellsWithBlobs, List<string> Barriers){
        List<string> Result = new List<string>();
        int CountGreenBlobs= 0, CountRedBlobs= 0, CountBlueBlobs= 0, CountYellowBlobs = 0;
        int CountGreenBars= 0, CountRedBars= 0, CountBlueBars= 0, CountYellowBars = 0;

        for (int i = 0; i < cellsWithBlobs.Count; i++)
        {
            switch (cellsWithBlobs[i].BlobType){
                case "Red":
                    CountRedBlobs++;
                break;
                case "Green":
                    CountGreenBlobs++;
                break;
                case "Yellow":
                CountYellowBlobs++;
                break;
                case "Blue":
                CountBlueBlobs++;
                break;
            }
        }

        for (int i = 0; i < Barriers.Count; i++)
        {
            switch (Barriers[i]){
                case "Red":
                    CountRedBars++;
                break;
                case "Green":
                    CountGreenBars++;
                break;
                case "Yellow":
                CountYellowBars++;
                break;
                case "Blue":
                CountBlueBars++;
                break;
            }
        }

        if (CountBlueBars<CountBlueBlobs) Result.Add("Blue");
        if (CountRedBars<CountRedBlobs) Result.Add("Red");
        if (CountYellowBars<CountYellowBlobs) Result.Add("Yellow");
        if (CountGreenBars<CountGreenBlobs) Result.Add("Green");


        return Result;
    }

    private void RemoveWall(MazeGeneratorCell current, MazeGeneratorCell chozen)
    {
        if (current.X == chozen.X){
            if (current.Y > chozen.Y) current.WallBottom = false;
            else chozen.WallBottom = false;
        }
        else{
            if (current.X > chozen.X) current.WallLeft = false;
            else chozen.WallLeft = false;
        }
    }

    private void PlaceMazeExit(MazeGeneratorCell[,] maze){
        MazeGeneratorCell furthest = maze[0,0];

        for (int x = 0; x < maze.GetLength(0); x++){
            if(maze[x,Width-2].DistanceFromStart > furthest.DistanceFromStart) furthest = maze [x, Width -2];
            if(maze[x,0].DistanceFromStart > furthest.DistanceFromStart) furthest = maze [x, 0];
        }

        for (int y = 0; y < maze.GetLength (1); y++){
            if(maze[Width-2, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze [Width - 2, y];
            if(maze[0, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze [0, y];
        }

        furthest.MazeExit = true;


        

    }


}
