using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    public GameObject cellPrefab;
    Cell[,] cells;
    Cell[,] nextCells;

    float cellSize = 0.25f; //Size of our cells
    int numberOfColums, numberOfRows;
    float spawnChancePercentage;
    int neighborIsAlive;
    public int generations;

    public bool inPause;

    public ValueHolder valueHolder;

    void Start()
    {        
        spawnChancePercentage = valueHolder.spawnChancePercentage;
        Camera.main.orthographicSize = valueHolder.cameraSize;

        //Lower framerate makes it easier to test and see whats happening.
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 4;

        //Calculate our grid depending on size and cellSize
        numberOfColums = (int)Mathf.Floor((Camera.main.orthographicSize *
            Camera.main.aspect * 2) / cellSize);
        numberOfRows = (int)Mathf.Floor(Camera.main.orthographicSize * 2 / cellSize);

        //Initiate our matrix array
        cells = new Cell[numberOfColums, numberOfRows];

        //For each row
        for (int y = 0; y < numberOfRows; y++)
        {
            //for each column in each row
            for (int x = 0; x < numberOfColums; x++)
            {
                //Create our game cell objects, multiply by cellSize for correct world placement
                Vector2 newPos = new Vector2(x * cellSize - Camera.main.orthographicSize *
                    Camera.main.aspect+cellSize,
                    y * cellSize - Camera.main.orthographicSize+cellSize);

                var newCell = Instantiate(cellPrefab, newPos, Quaternion.identity);
                newCell.transform.localScale = Vector2.one * cellSize;
                cells[x, y] = newCell.GetComponent<Cell>();

                //Random check to see if it should be alive
                if (Random.Range(0, 100) < spawnChancePercentage)
                {
                    cells[x, y].alive = true;
                }

                cells[x, y].UpdateStatus();
            }
        }
        Debug.Log("y = " + numberOfRows);
        Debug.Log("x = " + numberOfColums);
        Debug.Log("Total cells = " + numberOfRows * numberOfColums);
    }

    void Update()
    {
        if (!inPause)
        {
            generations++;
            for (int y = 0; y < numberOfRows; y++)
            {
                for (int x = 0; x < numberOfColums; x++)
                {
                    if (cells[(x - 1 + numberOfColums) % numberOfColums, (y - 1 + numberOfRows) % numberOfRows].alive)
                        neighborIsAlive++;
                    if (cells[(x + 1 + numberOfColums) % numberOfColums, (y - 1 + numberOfRows) % numberOfRows].alive)
                        neighborIsAlive++;
                    if (cells[x, (y - 1 + numberOfRows) % numberOfRows].alive)
                        neighborIsAlive++;
                    if (cells[x, (y + 1 + numberOfRows) % numberOfRows].alive)
                        neighborIsAlive++;
                    if (cells[(x - 1 + numberOfColums) % numberOfColums, y].alive)
                        neighborIsAlive++;
                    if (cells[(x - 1 + numberOfColums) % numberOfColums, (y + 1 + numberOfRows) % numberOfRows].alive)
                        neighborIsAlive++;
                    if (cells[(x + 1 + numberOfColums) % numberOfColums, (y + 1 + numberOfRows) % numberOfRows].alive)
                        neighborIsAlive++;
                    if (cells[(x + 1 + numberOfColums) % numberOfColums, y].alive)
                        neighborIsAlive++;

                    if (cells[x, y].alive == true)
                    {
                        if (neighborIsAlive < 2 || neighborIsAlive > 3)
                        {
                            cells[x, y].aliveNext = false;
                            cells[x, y].stableLifeCount = 0;
                        }
                        else
                        {
                            cells[x, y].aliveNext = true;
                        }
                    }

                    if (cells[x, y].alive == false)
                    {
                        if (neighborIsAlive == 3)
                        {
                            cells[x, y].aliveNext = true;
                        }
                    }
                    if (cells[x, y].alive)
                        cells[x, y].stableLifeCount++;
                    nextCells = cells;
                    neighborIsAlive = 0;
                }
            }
            cells = nextCells;
            for (int y = 0; y < numberOfRows; y++)
            {
                //for each column in each row
                for (int x = 0; x < numberOfColums; x++)
                {
                    cells[x, y].alive = cells[x, y].aliveNext;
                    cells[x, y].UpdateStatus();
                }
            }
        }
    }
    public void PauseGame()
    {
        inPause = !inPause;
    }
    
}
