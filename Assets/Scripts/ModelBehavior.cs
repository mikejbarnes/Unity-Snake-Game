using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ModelBehavior : MonoBehaviour
{
    public List<int[]> Snake { get; private set; } 
    public int[] Food { get; private set; }

    private int[,] _gameGrid = new int[GameBoardParameters.XTiles, GameBoardParameters.YTiles];
    private int _xDirection = 1;
    private int _yDirection = 0;
    private int _gameState = ResultStates.Continue;

    void Awake()
    {
        Snake = new List<int[]>();
        Food = new int[2];

        InitializeGameGrid();
        SelectSnakeLocation();
        SelectFoodLocation();
    }

    public int OnUpdate(int xDirection, int yDirection)
    {
        _gameState = ResultStates.Continue;

        _xDirection = xDirection;
        _yDirection = yDirection;

        UpdateGrid();

        return _gameState;
    }

    private void InitializeGameGrid()
    {
        for (int i = 0; i < GameBoardParameters.XTiles; i++)
        {
            for (int j = 0; j < GameBoardParameters.YTiles; j++)
            {
                if (i == 0 || i == GameBoardParameters.XTiles - 1 || j == 0 || j == GameBoardParameters.YTiles - 1)
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

        for (int i = 0; i < GameBoardParameters.SnakeInitialSegments; i++)
        {
            _gameGrid[snakeX, snakeY] = GridStates.Snake;

            int[] snakeXY = new int[] { snakeX - i, snakeY };
            Snake.Add(snakeXY);
        }
    }

    private void GrowSnake()
    {
        int newSnakeX = Snake[Snake.Count - 1][0];
        int newSnakeY = Snake[Snake.Count - 1][1];
        int[] newSnakePiece = new int[] { newSnakeX, newSnakeY };

        Snake.Add(newSnakePiece);
    }

    private void SelectFoodLocation()
    {
        int foodX = 0;
        int foodY = 0;

        while (_gameGrid[foodX, foodY] != 0)
        {
            foodX = UnityEngine.Random.Range(0, (int)GameBoardParameters.XTiles);
            foodY = UnityEngine.Random.Range(0, (int)GameBoardParameters.YTiles);
        }

        _gameGrid[foodX, foodY] = GridStates.Food;
        Food = new int[] { foodX, foodY };
    }

    private void UpdateGrid()
    {
        //Store the tail position before updating the snake body so we can set that grid space to empty
        int[] previousSnakeTailPosition = new int[2];  
        Array.Copy(Snake[Snake.Count - 1], previousSnakeTailPosition, 2);

        UpdateSnakePositions();

        _gameGrid[previousSnakeTailPosition[0], previousSnakeTailPosition[1]] = GridStates.Empty;
        _gameGrid[Snake[0][0], Snake[0][1]] = GridStates.Snake;
    }

    private void UpdateSnakePositions()
    {
        int[] newHeadPosition = GetNewHeadPosition();

        if (_gameState != ResultStates.Collision)
        {
            ///Moves through the Snake list in reverse order, setting each position array to the value of the one in front of it
            for (int i = Snake.Count - 1; i > 0; i--)
            {
                Array.Copy(Snake[i - 1], Snake[i], 2);
            }

            Snake[0] = newHeadPosition;
        }
    }

    private int[] GetNewHeadPosition()
    {
        int newHeadX = Snake[0][0] + _xDirection;
        int newHeadY = Snake[0][1] + _yDirection;

        int[] newHeadPosition = new int[] { newHeadX, newHeadY };

        CheckForCollision(newHeadX, newHeadY);

        return newHeadPosition; 
    }

    private void CheckForCollision(int headX, int headY)
    {
        int gridState = _gameGrid[headX, headY];

        if(gridState == GridStates.Food)
        {
            _gameState = ResultStates.Grow;
            GrowSnake();
            SelectFoodLocation();
        }
        else if(gridState == GridStates.Wall || gridState == GridStates.Snake)
        {
            _gameState = ResultStates.Collision;
        }
    }
}
