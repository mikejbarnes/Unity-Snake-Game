using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehavior : MonoBehaviour
{
    public GameObject GraphicsManager;
    
    private GraphicsManagerBehavior graphicsScripts;
    private Queue<int[]> _snake = new Queue<int[]>();
    private int[] _foodPosition = new int[2];
    private int[,] _gameGrid = new int[GameBoardParameters.XTiles, GameBoardParameters.YTiles];

    // Start is called before the first frame update
    void Awake()
    {
        graphicsScripts = GraphicsManager.GetComponent<GraphicsManagerBehavior>();

        graphicsScripts.BuildWalls();
        InitializeGameGrid();
        SelectSnakeLocation();
        SelectFoodLocation();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    
    private void InitializeGameGrid()
    {
        for(int i = 0; i < GameBoardParameters.XTiles; i++)
        {
            for(int j = 0; j < GameBoardParameters.YTiles; j++)
            {
                if(i == 0 || i == GameBoardParameters.XTiles - 1 || j == 0 || j == GameBoardParameters.YTiles - 1)
                {
                    _gameGrid[i, j] = GridStates.Wall;   
                }
            }
        }
    }    

    
    
    private void SelectSnakeLocation()
    {
        int snakeX = Mathf.FloorToInt(GameBoardParameters.XTiles / 2);
        int snakeY = Mathf.FloorToInt(GameBoardParameters.YTiles / 2);
        
        _gameGrid[snakeX, snakeY] = GridStates.Snake;

        int[] snakeXY = new int[] { snakeX, snakeY };
        _snake.Enqueue(snakeXY);
    }

    private void SelectFoodLocation()
    {
        int foodX = 0;
        int foodY = 0;

        while (_gameGrid[foodX, foodY] != 0)
        {
            foodX = Random.Range(0, (int)GameBoardParameters.XTiles);
            foodY = Random.Range(0, (int)GameBoardParameters.YTiles);
        }

        _gameGrid[foodX, foodY] = GridStates.Food;
    }



}
