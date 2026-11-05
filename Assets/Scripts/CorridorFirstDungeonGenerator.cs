using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField] private int corridorLength = 10, corridorCount = 5;
    [SerializeField] [Range(0f,1f)] float roomPercentage;
    protected override void RunProceduralGeneration()
    {
        tilemapVisualizer.ClearTiles();

        HashSet<Vector2Int> floorPositions = new();
        HashSet<Vector2Int> potentialRoomPositions = new();
        
        GenerateCorridor(floorPositions,potentialRoomPositions);
        List<Vector2Int> deadEnds = FindCorridorDeadEnds(floorPositions);
        HashSet<Vector2Int> roomPositions = CreateRoomsAtCorridor(potentialRoomPositions,deadEnds);
        
        floorPositions.UnionWith(roomPositions);

        tilemapVisualizer.GenerateFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions,tilemapVisualizer);
    }

    private List<Vector2Int> FindCorridorDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new();

        foreach (var floorPosition in floorPositions)
        {
            int neighbours  = 0;
            foreach (var direction in Direction2D.cardinalDirection)
            {
                if(floorPositions.Contains(floorPosition+direction)) neighbours++;
            }
            if(neighbours==1) deadEnds.Add(floorPosition);   
        }
        Debug.Log(deadEnds.Count);
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRoomsAtCorridor(HashSet<Vector2Int> potentialRoomPositions, List<Vector2Int> deadEnds)
    {
        HashSet<Vector2Int> roomPositions = new();
        int roomCount = Mathf.RoundToInt(roomPercentage*potentialRoomPositions.Count);
        List<Vector2Int> potentialRoomPositionsList = potentialRoomPositions.ToList();

        for (int i = 0; i < potentialRoomPositions.Count; i++)
        {
            int j = Random.Range(i,potentialRoomPositions.Count);
            (potentialRoomPositionsList[i],potentialRoomPositionsList[j]) = (potentialRoomPositionsList[j],potentialRoomPositionsList[i]);
        }
        potentialRoomPositionsList.InsertRange(0,deadEnds);
        for (int i = 0; i < roomCount; i++)
        {
            HashSet<Vector2Int> roomPosition = RunRandomWalk(potentialRoomPositionsList[i]);
            roomPositions.UnionWith(roomPosition);
        }
        return roomPositions;
    }

    private void GenerateCorridor(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        Vector2Int currentPosition = startingPosition;
        potentialRoomPositions.Add(startingPosition);

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.CorridorGenerator(currentPosition,corridorLength);
            floorPositions.UnionWith(corridor);
            currentPosition = corridor[corridor.Count-1];
            potentialRoomPositions.Add(currentPosition);
        }
    }
}
