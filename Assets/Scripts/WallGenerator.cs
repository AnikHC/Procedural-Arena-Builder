using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions,TilemapVisualizer tilemapVisualizer)
    {
        var basicWallPositions = FindWallsInCardinalDirections(floorPositions,Direction2D.cardinalAndDiagonalDirection);
        PaintWalls(floorPositions,basicWallPositions,tilemapVisualizer);
    }

    private static void PaintWalls(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> basicWallPositions,TilemapVisualizer tilemapVisualizer)
    {
        foreach(var wallPosition in basicWallPositions)
        {
            //Walls in between
            if(CheckFloorNeighbourCount(floorPositions,wallPosition)>=3||CheckFloorNeighbourCount(basicWallPositions,wallPosition)>=3) 
                tilemapVisualizer.PaintWallTile(wallPosition,tilemapVisualizer.straightLeftRight_Up);

            //corner 1
            else if(floorPositions.Contains(wallPosition+Vector2Int.up)&&floorPositions.Contains(wallPosition+Vector2Int.right))
                tilemapVisualizer.PaintWallTile(wallPosition,tilemapVisualizer.bentLeft);
            else if(floorPositions.Contains(wallPosition+Vector2Int.up)&&floorPositions.Contains(wallPosition+Vector2Int.left))
                tilemapVisualizer.PaintWallTile(wallPosition,tilemapVisualizer.bentRight);

            //corner 2
            else if(basicWallPositions.Contains(wallPosition+Vector2Int.left)&&basicWallPositions.Contains(wallPosition+Vector2Int.down))
                tilemapVisualizer.PaintWallTile(wallPosition,tilemapVisualizer.straightUpDown_Right);
            else if(basicWallPositions.Contains(wallPosition+Vector2Int.right)&&basicWallPositions.Contains(wallPosition+Vector2Int.down))
                tilemapVisualizer.PaintWallTile(wallPosition,tilemapVisualizer.straightUpDown_Left);
            
            //straight Walls
            else if(floorPositions.Contains(wallPosition+Vector2Int.up))
                tilemapVisualizer.PaintWallTile(wallPosition,tilemapVisualizer.straightLeftRight_Down);
            else if(floorPositions.Contains(wallPosition+Vector2Int.down))
                tilemapVisualizer.PaintWallTile(wallPosition,tilemapVisualizer.straightLeftRight_Up);
            else if(floorPositions.Contains(wallPosition+Vector2Int.right))
                tilemapVisualizer.PaintWallTile(wallPosition,tilemapVisualizer.straightUpDown_Left);
            else if(floorPositions.Contains(wallPosition+Vector2Int.left))
                tilemapVisualizer.PaintWallTile(wallPosition,tilemapVisualizer.straightUpDown_Right);
            
            //corner3
            else if(basicWallPositions.Contains(wallPosition + Vector2Int.up)&&basicWallPositions.Contains(wallPosition+Vector2Int.left))
                tilemapVisualizer.PaintWallTile(wallPosition,tilemapVisualizer.downRightCorner);
            else if(basicWallPositions.Contains(wallPosition + Vector2Int.up)&&basicWallPositions.Contains(wallPosition+Vector2Int.right))
                tilemapVisualizer.PaintWallTile(wallPosition,tilemapVisualizer.downLeftCorner);
            
            
        } 
    }

    private static byte CheckFloorNeighbourCount(HashSet<Vector2Int> floorPositions,Vector2Int wallPosition)
    {
        byte count = 0;
        foreach (var direction in Direction2D.cardinalDirection)
        {
            if(floorPositions.Contains(wallPosition+direction)) count++;
        }
        return count;
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
