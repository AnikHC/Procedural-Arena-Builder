using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField] private int corridorLength = 10, corridorCount = 5;
    [SerializeField] [Range(0f,1f)] float roomPercentage;
    protected override void RunProceduralGeneration()
    {
        tilemapVisualizer.ClearTiles();
        HashSet<Vector2Int> floorPositions = new();
        GenerateCorridor(floorPositions);
        tilemapVisualizer.GenerateFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions,tilemapVisualizer);
    }

    private void GenerateCorridor(HashSet<Vector2Int> floorPositions)
    {
        Vector2Int currentPosition = startingPosition;

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.CorridorGenerator(currentPosition,corridorLength);
            floorPositions.UnionWith(corridor);
            currentPosition = corridor[corridor.Count-1];
        }
    }
}
