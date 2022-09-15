using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardParameters : MonoBehaviour
{
    public static int XTiles = 64;
    public static int YTiles = 32;
    public static float TileSideLength = 0.278f;
    public static int SnakeInitialSegments = 5;
    public static float SecondsPerGameTic = 0.05f;

    public static float ConvertGridPositionToWorldSpace(int gridDimension)
    {
        float worldDimension = gridDimension * TileSideLength + TileSideLength / 2;

        return worldDimension;
    }
}
