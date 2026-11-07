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
        GenerateRooms();
    }
    protected override void RunWallGenerations()
    {
        WallGenerator.CreateWalls(floorPositions,tilemapVisualizer);
    }
    #endregion

    #region Generation
    protected void GenerateRooms()
    {   
        floorPositions = RunRandomWalk(startingPosition);
        SaveFloorPositions(floorPositions);
        tilemapVisualizer.ClearTiles();
        tilemapVisualizer.GenerateFloorTiles(floorPositions);
        CenterCamera(floorPositions);
        if(generateWallAutomatically) RunWallGenerations();
    }

    protected HashSet<Vector2Int> RunRandomWalk(Vector2Int startingPosition)
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
    #endregion
}
