using System;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField] protected Vector2Int startingPosition = Vector2Int.zero;
    [SerializeField] protected TilemapVisualizer tilemapVisualizer = null;

    public void GenerateDungeon()
    {
        tilemapVisualizer.ClearTiles();
        RunProceduralGeneration(); 
    }

    protected abstract void RunProceduralGeneration();
}
