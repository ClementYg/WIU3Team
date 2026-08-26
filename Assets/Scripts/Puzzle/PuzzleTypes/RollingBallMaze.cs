using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEditor.Rendering;

public class RollingMazePuzzle : ScreenPuzzle
{
    [Header("Maze References")]
    [SerializeField] Transform mazeRoot;
    [SerializeField] Rigidbody2D ballRigidbody;
    [SerializeField] Camera mazeCamera;

    [Header("Tilt Settings")]
    [SerializeField] float tiltSpeed = 60f;   //degrees per second
    [SerializeField] float maxTiltAngle = 20f;

    [Header("Maze Generation")]
    [SerializeField] bool hasGenerated = false;
    [SerializeField] int mazeSeed;
    [SerializeField][Delayed] int gridWidth = 8;
    [SerializeField][Delayed] int gridHeight = 8;
    [SerializeField] float cellSize = 1f;
    [SerializeField] GameObject wallPrefab;
    [SerializeField] float wallThickness = 0.1f; // walls are thin bars, not full cell-sized squares
    [SerializeField] Vector2Int exitCell = new Vector2Int(7, 0);
    [SerializeField] MazeExitTrigger exitTrigger;
    [System.Flags]
    enum WallSides { North = 1, South = 2, East = 4, West = 8 }

    float currentTilt;


#if UNITY_EDITOR
    //just have to check that exit cell will be placed correctly
    private void OnValidate()
    {
        exitCell.x = Mathf.Clamp(exitCell.x, 0, Mathf.Max(0, gridWidth - 1));
        exitCell.y = Mathf.Clamp(exitCell.y, 0, Mathf.Max(0, gridHeight - 1));

        // exitCell must sit on the top row or right column
        bool onTopRow = exitCell.y == gridHeight - 1;
        bool onRightColumn = exitCell.x == gridWidth - 1;
        if (!onTopRow && !onRightColumn)
        {
            exitCell.x = gridWidth - 1;
            exitCell.y = gridHeight - 1;
        }
    }
#endif

    public override void StartPuzzle(string puzzleID)
    {
        base.StartPuzzle(puzzleID);
        //stop it from regenerating every time start and end puzzle
        //will regenerate every time reload scene etc
        if (!hasGenerated)
        {
            GenerateMaze();
            hasGenerated = true;
        }

        ResetBall();
        currentTilt = 0f;
        mazeRoot.localRotation = Quaternion.identity;
    }
    void GenerateMaze()
    {
        //this is the randomiser
        mazeSeed = System.Guid.NewGuid().GetHashCode();
        Random.InitState(mazeSeed);

        //this part walls in the entire grid/playArea
        //WallSides is to decide what side each grid is, direction based, which is instantiated in the loop
        //Visited tracks whether the algorithm has been there, so wont repeat
        WallSides[,] grid = new WallSides[gridWidth, gridHeight];
        bool[,] visited = new bool[gridWidth, gridHeight];
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                grid[x, y] = WallSides.North | WallSides.South | WallSides.East | WallSides.West;
            }
        }

        //Start the traversal from 0,0
        //Stack is used here to prevent overflowing
        Stack<Vector2Int> stack = new();
        Vector2Int start = Vector2Int.zero;
        visited[start.x, start.y] = true;
        stack.Push(start);


        //Algorithm: Runs till the end of the stack, 
        while (stack.Count > 0)
        {
            Vector2Int current = stack.Peek();
            List<Vector2Int> unvisited = GetUnvisitedNeighbors(current, visited);

            if (unvisited.Count == 0)
            {
                stack.Pop(); // dead end - backtrack
                continue;
            }
            //if there are unvisited routes, randomly pick one to go towards
            Vector2Int next = unvisited[Random.Range(0, unvisited.Count)];
            //remove from the grid
            RemoveWallBetween(grid, current, next);
            visited[next.x, next.y] = true;
            stack.Push(next);
        }

        InstantiateWalls(grid);
        FrameCamera();
    }

    void FrameCamera()
    {
        if (mazeCamera == null) return;

        RenderTexture rt = mazeCamera.targetTexture;
        if (rt != null)
        {
            mazeCamera.aspect = (float)rt.width / rt.height;
        }

        Vector3 camPos = mazeCamera.transform.position;
        mazeCamera.transform.position = new Vector3(mazeRoot.position.x, mazeRoot.position.y, camPos.z);
        float halfWidth = gridWidth * cellSize / 2f;
        float halfHeight = gridHeight * cellSize / 2f;

        float tiltRad = maxTiltAngle * Mathf.Deg2Rad;
        float rotatedHalfWidth = halfWidth * Mathf.Cos(tiltRad) + halfHeight * Mathf.Sin(tiltRad);
        float rotatedHalfHeight = halfWidth * Mathf.Sin(tiltRad) + halfHeight * Mathf.Cos(tiltRad);
        float padding = 1.1f;
        mazeCamera.orthographicSize = Mathf.Max(rotatedHalfHeight, rotatedHalfWidth / mazeCamera.aspect) * padding;
    }
   
    List<Vector2Int> GetUnvisitedNeighbors(Vector2Int cell, bool[,] visited)
    {
        //This function checks each of the directions to see if on the edge or already visited, else will add as a neighbour
        //Returns possible routes/neighbours that are open

        List<Vector2Int> neighbors = new();
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        foreach (Vector2Int dir in directions)
        {
            Vector2Int neighbor = cell + dir;
            bool inBounds = neighbor.x >= 0 && neighbor.x < gridWidth && neighbor.y >= 0 && neighbor.y < gridHeight;
            if (inBounds && !visited[neighbor.x, neighbor.y])
                neighbors.Add(neighbor);
        }

        return neighbors;
    }

    void RemoveWallBetween(WallSides[,] grid, Vector2Int a, Vector2Int b)
    {
        //diff ==> vector direction 
        //checks which direction and decides which wall to remove based on whats in between  and the direction towards the diff
        Vector2Int diff = b - a;

        if (diff == Vector2Int.up) { grid[a.x, a.y] &= ~WallSides.North; grid[b.x, b.y] &= ~WallSides.South; }
        else if (diff == Vector2Int.down) { grid[a.x, a.y] &= ~WallSides.South; grid[b.x, b.y] &= ~WallSides.North; }
        else if (diff == Vector2Int.right) { grid[a.x, a.y] &= ~WallSides.East; grid[b.x, b.y] &= ~WallSides.West; }
        else if (diff == Vector2Int.left) { grid[a.x, a.y] &= ~WallSides.West; grid[b.x, b.y] &= ~WallSides.East; }
    }

    void InstantiateWalls(WallSides[,] grid)
    {
        Vector2 gridOffset = new Vector2((gridWidth - 1) * cellSize / 2f, (gridHeight - 1) * cellSize / 2f);

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector2 cellCenter = new Vector2(x * cellSize, y * cellSize) - gridOffset;
                WallSides walls = grid[x, y];
                bool isExitCell = new Vector2Int(x, y) == exitCell;

                if ((walls & WallSides.South) != 0) SpawnWall(cellCenter + Vector2.down * (cellSize / 2f), 0f);
                if ((walls & WallSides.West) != 0) SpawnWall(cellCenter + Vector2.left * (cellSize / 2f), 90f);

                if (y == gridHeight - 1 && (walls & WallSides.North) != 0 && !isExitCell)
                    SpawnWall(cellCenter + Vector2.up * (cellSize / 2f), 0f);
                if (x == gridWidth - 1 && (walls & WallSides.East) != 0 && !isExitCell)
                    SpawnWall(cellCenter + Vector2.right * (cellSize / 2f), 90f);
            }
        }

        if (exitTrigger != null)
        {
            exitTrigger.transform.SetParent(mazeRoot);
            exitTrigger.transform.localPosition = new Vector2(exitCell.x * cellSize, exitCell.y * cellSize) - gridOffset;
        }
    }
    void SpawnWall(Vector2 localPosition, float zRotation)
    {
        GameObject wall = Instantiate(wallPrefab, mazeRoot);
        wall.transform.localPosition = localPosition;
        wall.transform.localRotation = Quaternion.Euler(0f, 0f, zRotation);
        wall.transform.localScale = new Vector3(cellSize, wallThickness, 1f);
    }

    //only have to check puzzleInstance and do the actual tilting (rotate left/right)
    void Update()
    {
        if (PuzzleManager.Instance.currentPuzzle != this) return;

        InputAction tiltAction = InputSystem.actions.FindAction("Tilt");
        float tiltInput = tiltAction != null ? tiltAction.ReadValue<Vector2>().x : 0f;

        currentTilt = Mathf.Clamp(currentTilt - tiltInput * tiltSpeed * Time.deltaTime, -maxTiltAngle, maxTiltAngle);
        mazeRoot.localRotation = Quaternion.Euler(0f, 0f, currentTilt);
    }

    //below is just complete&&resetting
    void ResetBall()
    {
        ballRigidbody.linearVelocity = Vector2.zero;
        ballRigidbody.angularVelocity = 0f;
        ballRigidbody.transform.position = mazeRoot.position;
        ballRigidbody.transform.rotation = Quaternion.identity;
    }

    public void OnBallReachedExit()
    {
        ballRigidbody.linearVelocity = Vector2.zero;
        ballRigidbody.angularVelocity = 0f;
        ballRigidbody.simulated = false;

        CompletePuzzle(puzzleID);
    }
}