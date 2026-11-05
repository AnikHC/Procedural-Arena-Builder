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
    [SerializeField] private TileBase wallTile;

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

    private void PaintSingleTile(Vector2Int position, Tilemap tileMap, TileBase tile)
    {
        var tilePosition = tileMap.WorldToCell((Vector3Int)position);
        tileMap.SetTile(tilePosition,tile);
    }
    public void PaintWallTile(IEnumerable<Vector2Int> wallPosition)
    {
        PaintTiles(wallPosition,wallTileMap,wallTile);
    }
    public void ClearTiles()
    {
        floorTileMap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
    }

}
