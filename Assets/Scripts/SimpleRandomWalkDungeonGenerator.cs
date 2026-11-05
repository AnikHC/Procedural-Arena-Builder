using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField] private SimpleRandomWalkParameter simpleRandomWalkParameter;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        tilemapVisualizer.ClearTiles();
        tilemapVisualizer.GenerateFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions,tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startingPosition;
        HashSet<Vector2Int>  floorPositions = new();

        for (int i = 0; i < simpleRandomWalkParameter.iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalkGenerator(currentPosition,simpleRandomWalkParameter.walkLength);
            floorPositions.UnionWith(path);
            if(simpleRandomWalkParameter.startRandomlyEachIteration) currentPosition = floorPositions.ElementAt(Random.Range(0,floorPositions.Count));
        }
        return floorPositions;
    }
}
