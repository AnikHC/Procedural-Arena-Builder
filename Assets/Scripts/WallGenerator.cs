using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions,TilemapVisualizer tilemapVisualizer)
    {
        var basicWallPositions = FindWallsInCardinalDirections(floorPositions,Direction2D.cardinalDirection);
        tilemapVisualizer.PaintWallTile(basicWallPositions);
    }

    private static HashSet<Vector2Int> FindWallsInCardinalDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directions)
    {
        HashSet<Vector2Int> wallPositions = new();
        foreach (var floorPosition in floorPositions)
        {
            foreach (var direction in directions)
            {
                Vector2Int position = floorPosition+direction;
                if(!floorPositions.Contains(position)) wallPositions.Add(position);
            }
        }
        return wallPositions;
    }

}
