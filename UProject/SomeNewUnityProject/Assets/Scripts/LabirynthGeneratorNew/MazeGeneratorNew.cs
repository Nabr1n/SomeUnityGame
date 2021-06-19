using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneratorCellNew
{
    public int X;
    public int Y;

    public bool WallLeft = true;
    public bool WallBottom = true;

    
    public int DistanceFromStart;

    public bool Visited = false;

    public bool MazeExit = false;
    public bool AmISanctuary = false;
    public bool AmISanctuaryCenter = false;
    public bool AmIInClosedSanctuary;

    public bool ShouldBeWithBlob = false;

    public bool UnbreakableWallLeft, UnbreakableWallBottom;
    
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
    public MazeGeneratorCellNew(int x, int y){
        X = x;
        Y = y;
    } 
}


public class MazeGeneratorNew
{
    public int Width = 20;
    public int Height = 20;

    public int SantuarySideLength = 3;

    public MazeGeneratorCellNew[,] GenerateNewMaze(int width, int height, int sancturySideLen, bool ClosedSanctuary, int ClosedSanctuarySideLen){

        Width = width;
        Height = height;
        SantuarySideLength = sancturySideLen;




        MazeGeneratorCellNew[,] maze = new MazeGeneratorCellNew [Width,Height];
        for (int w = 0; w < maze.GetLength(0); w++)
        {
            for (int l = 0; l < maze.GetLength(1); l++)
            {
                maze [w,l] = new MazeGeneratorCellNew(w, l);
            }
        }

        for (int x = 0; x < maze.GetLength(0); x++){
            maze[x,Height-1].WallLeft = false;
            maze[x,Height-1].UnbreakableWallBottom = true;
            maze[x,0].UnbreakableWallBottom = true;
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            maze[Width-1, y].WallBottom = false;
            maze[Width-1, y].UnbreakableWallLeft = true;
            maze[0,y].UnbreakableWallLeft = true;
        }



        
        RemoveWallsWithBackTracker (maze);
        PlaceMazeExit(maze);
        PlaceMazeDoors(maze);
        PlaceMazeSanctuary(maze, ClosedSanctuary, ClosedSanctuarySideLen);

        return maze;
    }

    private void PlaceMazeDoors(MazeGeneratorCellNew[,] maze)
    {
       
    }

    private void RemoveWallsWithBackTracker(MazeGeneratorCellNew[,] maze){
        MazeGeneratorCellNew current = maze [0,0];
        bool LastMoveWasTrue = true;

        current.DistanceFromStart = 0;
        current.Visited = true;

        Stack<MazeGeneratorCellNew> stack = new Stack<MazeGeneratorCellNew>();
        //List<MazeGeneratorCellNew> CellsWithBlobs = new List<MazeGeneratorCellNew>();
        //List<string> Barriers = new List<string>();
        do
        {
            List<MazeGeneratorCellNew> unvisitedNeighbours = new List<MazeGeneratorCellNew>();

            int w = current.X;
            int l = current.Y;

            
            if (w > 0 && !maze[w-1, l].Visited) unvisitedNeighbours.Add(maze[w-1, l]);
            if (l > 0 && !maze[w, l-1].Visited) unvisitedNeighbours.Add(maze[w, l-1]);
            if (w < Width - 2 && !maze[w+1, l].Visited) unvisitedNeighbours.Add(maze[w+1, l]);
            if (l < Height - 2 && !maze[w, l+1].Visited) unvisitedNeighbours.Add(maze[w, l+1]);

            if (unvisitedNeighbours.Count > 0){
                MazeGeneratorCellNew chozen = unvisitedNeighbours [UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                
                RemoveWall(current,chozen);
                // if(CheckDoorsAndBlobs(CellsWithBlobs,Barriers).Count>0){
                //     int RandIndex = UnityEngine.Random.Range(0,CheckDoorsAndBlobs(CellsWithBlobs,Barriers).Count-1);
                //     Barriers.Add(CreatePortal(CheckDoorsAndBlobs(CellsWithBlobs,Barriers)[RandIndex], current,chozen));
                // }


                chozen.Visited = true;
                
                stack.Push(chozen);
                float Rand = UnityEngine.Random.Range(0f, 100f);
                if (Rand>=90f) {
                    current.ShouldBeWithBlob = true;
                    // current.RandomBlob();
                    // CellsWithBlobs.Add(current);
                }
                LastMoveWasTrue = true;
                current = chozen;
                chozen.DistanceFromStart = stack.Count;
                
            }
            else {
                if(LastMoveWasTrue) 
                {
                    current.ShouldBeWithBlob = true;
                    //current.RandomBlob();
                    //CellsWithBlobs.Add(current);
                    LastMoveWasTrue = false;
                    
                }
                
                current = stack.Pop();
            }

        }
        while (stack.Count>0);

        
    }

    private void RemoveSomeWalls(MazeGeneratorCellNew[,] maze, bool ShouldUseCount, int countofRemovedWalls, float ChanceToRemoveWall){
        
    }



    private void PlaceMazeSanctuary(MazeGeneratorCellNew[,] maze, bool bShouldMakeClosedSanctuary, int ClosedSanctuarySideLen){
        
        //Length
        
            int RandWidth = UnityEngine.Random.Range(1+(SantuarySideLength/2), Width - (SantuarySideLength/2)-1);
            int RandHeight = UnityEngine.Random.Range(1+(SantuarySideLength/2), Height - (SantuarySideLength/2)-1);

            maze [RandWidth, RandHeight].AmISanctuary = true;
            maze [RandWidth, RandHeight].AmISanctuaryCenter = true;

            int StartingWidth = RandWidth - SantuarySideLength/2;
            int StartingHeight = RandHeight - SantuarySideLength/2;

            //Debug.Log ((SantuarySideLength+StartingWidth));


            for (int w = StartingWidth; w < SantuarySideLength + StartingWidth; w++)
            {
                for (int l = StartingHeight; l < SantuarySideLength + StartingHeight; l++)
                {
                    maze[w,l].AmISanctuary = true;
                    
                    maze[w,l].WallLeft = false;
                    maze[w,l].WallBottom = false;

                    
                    
                    if (w == SantuarySideLength + StartingWidth - 1 && w!=Width) maze [w+1, l].WallLeft = false;
                    if (l == SantuarySideLength + StartingHeight - 1&& l!=Height) maze [w, l+1].WallBottom = false;

                }
            }
            
            //Debug.Log (ClosedSanctuarySideLen);


            if (SantuarySideLength >= 5 && bShouldMakeClosedSanctuary){
                for (int w = RandWidth - ClosedSanctuarySideLen/2; w < RandWidth + ClosedSanctuarySideLen/2 + 1; w++)
                {
                    for (int l = RandHeight - ClosedSanctuarySideLen/2; l < RandHeight+ ClosedSanctuarySideLen/2 + 1; l++)
                    {
                        if (w == RandWidth - ClosedSanctuarySideLen/2 && l!=RandHeight) maze [w,l].WallLeft = true;
                        if (w == RandWidth + ClosedSanctuarySideLen/2 && l!=RandHeight) maze [w+1,l].WallLeft = true;
                        if (l == RandHeight - ClosedSanctuarySideLen/2 && w!=RandWidth) maze [w, l].WallBottom = true;
                        if (l == RandHeight + ClosedSanctuarySideLen/2 && w!=RandWidth) maze [w, l+1].WallBottom = true;
                        maze[w,l].AmIInClosedSanctuary = true;

                       

                        //Debug.Log (w + " " + l);
                    }
                }
            }
            

    }







    // private string CreatePortal(string Type, MazeGeneratorCellNew current, MazeGeneratorCellNew chozen)
    // {
    //    if (current.X == chozen.X){
    //         if (current.Y > chozen.Y) {
                
    //             return Type;
    //         }
    //         else{
                
    //             return Type;
    //         } 
    //     }
    //     else{
    //         if (current.X > chozen.X){ 
    //             current.WallLeft = true;
                
    //             return Type;

    //         }
    //         else {
                
    //             return Type;
    //         }
    //     }


        // if (current.X == chozen.X){
        //     if (current.Y > chozen.Y) current.WallBottom = false;
        //     else chozen.WallBottom = false;
        // }
        // else{
        //     if (current.X > chozen.X) current.WallLeft = false;
        //     else chozen.WallLeft = false;
        // }
    //}

    private List<string> CheckDoorsAndBlobs(List<MazeGeneratorCellNew> cellsWithBlobs, List<string> Barriers){
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

    private void RemoveWall(MazeGeneratorCellNew current, MazeGeneratorCellNew chozen)
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

    private void PlaceMazeExit(MazeGeneratorCellNew[,] maze){
        MazeGeneratorCellNew furthest = maze[0,0];

        for (int x = 0; x < maze.GetLength(0); x++){
            if(maze[x,Width-2].DistanceFromStart > furthest.DistanceFromStart) furthest = maze [x, Width -2];
            if(maze[x,0].DistanceFromStart > furthest.DistanceFromStart) furthest = maze [x, 0];
        }

        for (int y = 0; y < maze.GetLength (1); y++){
            if(maze[Width-2, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze [Width - 2, y];
            if(maze[0, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze [0, y];
        }

        furthest.MazeExit = true;


        

    }}
