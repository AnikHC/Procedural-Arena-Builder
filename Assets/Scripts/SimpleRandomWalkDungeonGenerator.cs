using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    #region Variables
    [SerializeField]protected int iterations = 10;
    [SerializeField]protected int walkLength = 10;
    [SerializeField]protected bool startRandomlyEachIteration = true;
    #endregion

    #region Overrides
    protected override void RunProceduralGeneration()
    {
        GenerateRooms(iterations,walkLength,startRandomlyEachIteration,generateWallAutomatically);
    }
    protected override void RunWallGenerations()
    {
        WallGenerator.CreateWalls(floorPositions,tilemapVisualizer);
    }
    #endregion

    #region Generation
    public void GenerateRooms(int iterations, int walkLength, bool startRandomlyEachIteration,bool generateWallAutomatically)
    {   
        floorPositions = RunRandomWalk(startingPosition, iterations,walkLength,startRandomlyEachIteration);
        SaveFloorPositions(floorPositions);
        tilemapVisualizer.ClearTiles();
        tilemapVisualizer.GenerateFloorTiles(floorPositions);
        CenterCamera(floorPositions);
        if(generateWallAutomatically) RunWallGenerations();
    }

    protected HashSet<Vector2Int> RunRandomWalk(Vector2Int startingPosition, int iterations, int walkLength, bool startRandomlyEachIteration)
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

    internal void GenerateRooms(bool v, string text, bool startRandomlyEachIteration)
    {
        throw new NotImplementedException();
    }
    #endregion
}
