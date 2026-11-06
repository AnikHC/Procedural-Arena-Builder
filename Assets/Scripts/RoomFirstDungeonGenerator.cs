using System;
using System.Collections.Generic;
using System.Linq;
// using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int dungeonWidth, dungeonHeight;
    [SerializeField]
    private int minRoomWidth, minRoomHeight;
    [SerializeField]
    private bool useRandomWalkForRoomGenerations;
    [SerializeField]
    [Range(0,10)]
    private int offset;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        BoundsInt dungeonSize = new((Vector3Int)startingPosition,new Vector3Int(dungeonWidth,dungeonHeight,0));
        List<BoundsInt> roomList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(minRoomWidth,minRoomHeight,dungeonSize);
        HashSet<Vector2Int> floorPositions = new();

        HashSet<Vector2Int> rooms = new();
        if (!useRandomWalkForRoomGenerations)
        {
            rooms = UseBSP(roomList);
        }
        else
        {
            rooms = CreateRoomsWithRandomWalk(roomList); 
        }
        floorPositions.UnionWith(rooms);

        HashSet<Vector2Int> corridors = CreateCorridors(roomList);
        floorPositions.UnionWith(corridors);

        ClearTile();
        tilemapVisualizer.GenerateFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions,tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateRoomsWithRandomWalk(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floorPositions = new();
        for (int i = 0; i < roomList.Count; i++)
        {
            var roomBounds = roomList[i];
            var roomPosition = (Vector2Int)Vector3Int.RoundToInt(roomBounds.center);
            var room = RunRandomWalk(roomPosition);
            foreach (var position in room)
            {
                if(position.x>=roomBounds.xMin+offset && position.x <= roomBounds.xMax-offset && 
                    position.y>=roomBounds.yMin+offset && position.y <= roomBounds.yMax-offset)
                {
                    floorPositions.Add(position);
                }
            }
        }
        return floorPositions;
    }

    private HashSet<Vector2Int> CreateCorridors(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> corridorFloors = new();
        List<Vector2Int> roomPositions = new();

        foreach (var room in roomList)
        {
            roomPositions.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }
        Vector2Int currentRoomPosition = roomPositions[Random.Range(0,roomPositions.Count)];
        roomPositions.Remove(currentRoomPosition);
        
        while(roomPositions.Count > 0)
        {
            var closestRoomPosition = FindClossestRoom(roomPositions,currentRoomPosition);
            HashSet<Vector2Int> connectedFloor = ConnectRooms(closestRoomPosition,currentRoomPosition);
            corridorFloors.UnionWith(connectedFloor);
            currentRoomPosition = closestRoomPosition;
            roomPositions.Remove(currentRoomPosition);
        }
        return corridorFloors;
    }

    private Vector2Int FindClossestRoom(List<Vector2Int> roomPositions, Vector2Int currentRoomPosition)
    {
        Debug.Log("Searching nearby rooms");
        Vector2Int closestRoom = new();
        float distance = float.MaxValue;
        foreach (var roomPosition in roomPositions)
        {
            float calculatedDistance = Vector2.Distance(roomPosition,currentRoomPosition);
            if(calculatedDistance<distance) closestRoom = roomPosition;
            distance = calculatedDistance;
        }
        return closestRoom;
    }
    private HashSet<Vector2Int> ConnectRooms(Vector2Int closestRoomPosition, Vector2Int currentRoomPosition)
    {
        Debug.Log("Connecting Rooms");
        HashSet<Vector2Int> floorPositions = new();
        var floorPosition = currentRoomPosition;
        
        floorPositions.Add(floorPosition);

        while(floorPosition == currentRoomPosition)
        {
            if (Random.value > 0.5f)
            {
                while(floorPosition.y != closestRoomPosition.y)
                {
                    if(floorPosition.y > closestRoomPosition.y)
                    {
                        floorPosition += Vector2Int.down;
                        floorPositions.Add(floorPosition);
                    }
                    else
                    {
                        floorPosition += Vector2Int.up;
                        floorPositions.Add(floorPosition);
                    }
                }
                while(floorPosition.x != closestRoomPosition.x)
                {
                    if(floorPosition.x > closestRoomPosition.x)
                    {
                        floorPosition += Vector2Int.left;
                        floorPositions.Add(floorPosition);
                    }
                    else
                    {
                        floorPosition += Vector2Int.right;
                        floorPositions.Add(floorPosition);
                    }
                }   
            }
            else
            {
                while(floorPosition.x != closestRoomPosition.x)
                {
                    if(floorPosition.x > closestRoomPosition.x)
                    {
                        floorPosition += Vector2Int.left;
                        floorPositions.Add(floorPosition);
                    }
                    else
                    {
                        floorPosition += Vector2Int.right;
                        floorPositions.Add(floorPosition);
                    }
                }  
                while(floorPosition.y != closestRoomPosition.y)
                {
                    if(floorPosition.y > closestRoomPosition.y)
                    {
                        floorPosition += Vector2Int.down;
                        floorPositions.Add(floorPosition);
                    }
                    else
                    {
                        floorPosition += Vector2Int.up;
                        floorPositions.Add(floorPosition);
                    }
                }   
            }
        }

        return floorPositions;
    }


    private HashSet<Vector2Int> UseBSP(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floorPositions = new();
     
        foreach(var room in roomList)
        {
            for (int col = offset; col < room.size.x; col++)
            {
                for (int row = offset; row < room.size.y; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col,row);
                    floorPositions.Add(position);
                }
            }
        }
        return floorPositions;
    }
}
