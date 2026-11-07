using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap floorTileMap;
    [SerializeField] private TileBase floorTile;
    [SerializeField] private Tilemap wallTileMap;
    public TileBase straightUpDown_Left, straightUpDown_Right, straightLeftRight_Up, straightLeftRight_Down, downRightCorner, downLeftCorner, bentLeft, bentRight;

    public void GenerateFloorTiles(IEnumerable <Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions,floorTileMap,floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tileMap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(position,tileMap,tile);
        }
    }
    private void PaintTiles(Vector2Int positions, Tilemap tileMap, TileBase tile)
    {
        PaintSingleTile(positions,tileMap,tile);
    }

    private void PaintSingleTile(Vector2Int position, Tilemap tileMap, TileBase tile)
    {
        var tilePosition = tileMap.WorldToCell((Vector3Int)position);
        tileMap.SetTile(tilePosition,tile);
    }
    public void PaintWallTile(Vector2Int wallPosition, TileBase wallTile)
    {
        PaintTiles(wallPosition,wallTileMap,wallTile);
    }
    public void ClearTiles()
    {
        floorTileMap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
    }

}
