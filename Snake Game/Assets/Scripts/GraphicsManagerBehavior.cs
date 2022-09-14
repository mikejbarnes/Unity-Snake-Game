using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManagerBehavior : MonoBehaviour
{
    public GameObject WallPiece;
    public GameObject SnakePiece;
    public GameObject Food;

    private Queue<GameObject> _walls = new Queue<GameObject>();
    private Queue<GameObject> _snake = new Queue<GameObject>();
    private GameObject _food = new GameObject();

    public void BuildWalls()
    {
        for (int i = 0; i < GameBoardParameters.XTiles; i++)
        {
            for (int j = 0; j < GameBoardParameters.YTiles; j++)
            {
                if (i == 0 || i == GameBoardParameters.XTiles - 1 || j == 0 || j == GameBoardParameters.YTiles - 1)
                {
                    GameObject newWall = Instantiate(WallPiece);
                    float tileXPosition = i * GameBoardParameters.TileSideLength + GameBoardParameters.TileSideLength / 2;
                    float tileYPosition = j * GameBoardParameters.TileSideLength + GameBoardParameters.TileSideLength / 2;

                    Vector3 newPosition = new Vector3(tileXPosition, tileYPosition, 0f);
                    newWall.transform.position = newPosition;
                    _walls.Enqueue(newWall);
                }
            }
        }
    }
}
