using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManagerBehavior : MonoBehaviour
{
    public GameObject WallPiece;
    public GameObject SnakePiece;
    public GameObject Food;

    private List<GameObject> _walls;
    private List<GameObject> _snake;
    private GameObject _food;

    private void Awake()
    {
        _walls = new List<GameObject>();
        _snake = new List<GameObject>();
        _food = new GameObject();

        BuildWalls();
        BuildSnake();
        BuildFood();  
    }

    public void BuildWalls()
    {
        for (int i = 0; i < GameBoardParameters.XTiles; i++)
        {
            for (int j = 0; j < GameBoardParameters.YTiles; j++)
            {
                if (i == 0 || i == GameBoardParameters.XTiles - 1 || j == 0 || j == GameBoardParameters.YTiles - 1)
                {
                    GameObject newWall = Instantiate(WallPiece);
                    float tileXPosition = GameBoardParameters.ConvertGridPositionToWorldSpace(i);
                    float tileYPosition = GameBoardParameters.ConvertGridPositionToWorldSpace(j);

                    Vector3 newPosition = new Vector3(tileXPosition, tileYPosition, 0f);
                    newWall.transform.position = newPosition;
                    _walls.Add(newWall);
                }
            }
        }
    }
    
    private void BuildSnake()
    {
        for(int i = 0; i < GameBoardParameters.SnakeInitialSegments; i++)
        {
            GrowSnake();
        }
    }

    public void GrowSnake()
    {
        GameObject snakePiece = Instantiate(SnakePiece);
        _snake.Add(snakePiece);
    }

    public void BuildFood()
    {
        _food = Instantiate(Food);
    }

    public void RenderSnake(List<int[]> snakePositions)
    {
        for (int i = 0; i < snakePositions.Count; i++)
        {
            float snakeX = GameBoardParameters.ConvertGridPositionToWorldSpace(snakePositions[i][0]);
            float snakeY = GameBoardParameters.ConvertGridPositionToWorldSpace(snakePositions[i][1]);

            _snake[i].transform.position = new Vector3(snakeX, snakeY, 0f);
        }
    }

    public void RenderFood(int[] foodPosition)
    {
        float foodX = GameBoardParameters.ConvertGridPositionToWorldSpace(foodPosition[0]);
        float foodY = GameBoardParameters.ConvertGridPositionToWorldSpace(foodPosition[1]);

        _food.transform.position = new Vector3(foodX, foodY, 0f);
    }
}
