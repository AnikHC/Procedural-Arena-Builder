using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField] private int walkLength = 10;
    [SerializeField] private int iterations = 10;
    [SerializeField] private bool startRandomlyEachIteration = true;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        tilemapVisualizer.ClearTiles();
        tilemapVisualizer.GenerateFloorTiles(floorPositions);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startingPosition;
        HashSet<Vector2Int>  floorPositions = new();

        for (int i = 0; i < iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalkGenerator(currentPosition,walkLength);
            floorPositions.UnionWith(path);
            if(startRandomlyEachIteration) currentPosition = floorPositions.ElementAt(Random.Range(0,floorPositions.Count));
        }
        return floorPositions;
    }
}
