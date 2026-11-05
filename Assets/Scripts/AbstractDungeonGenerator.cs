using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField] protected Vector2Int startingPosition = Vector2Int.zero;
    [SerializeField] protected TilemapVisualizer tilemapVisualizer = null;

    public void GenerateDungeon()
    {
        RunProceduralGeneration(); 
    }
    public void ClearTile()
    {
        tilemapVisualizer.ClearTiles();
    }

    protected abstract void RunProceduralGeneration();
}
