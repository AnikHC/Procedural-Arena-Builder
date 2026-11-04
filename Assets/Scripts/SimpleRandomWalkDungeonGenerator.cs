using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int startingPosition;
    [SerializeField] private int walkLength = 10;
    [SerializeField] private int iterations = 10;
    [SerializeField] private bool startRandomlyEachIteration = true;
    [SerializeField] private TilemapVisualizer tilemapVisualizer;

    public void StartProceduralGeneration()
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
