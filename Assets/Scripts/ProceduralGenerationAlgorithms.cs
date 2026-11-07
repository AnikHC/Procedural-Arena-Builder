using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    #region Simple Random Walk Algorithm
    public static HashSet<Vector2Int> SimpleRandomWalkGenerator(Vector2Int startPostion, int walkLength)
    {
        HashSet<Vector2Int> room = new()
        {
            startPostion
        };

        var previousPosition = startPostion;

        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            room.Add(newPosition);
            previousPosition = newPosition;
        }

        return room;
    }

    public static List<Vector2Int> CorridorGenerator(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new(){startPosition};
        Vector2Int direction = Direction2D.GetRandomCardinalDirection();
        Vector2Int currentPosition = startPosition;

        for (int i = 0; i < corridorLength; i++)
        {
            Vector2Int newPosition = currentPosition + direction;
            corridor.Add(newPosition);
            currentPosition = newPosition;
        }
        return corridor;
    }
    #endregion

    #region BSP Algorithm
    public static List<BoundsInt> BinarySpacePartitioning(int minWidth, int minHeight, BoundsInt spaceToSplit)
    {
        Queue<BoundsInt> roomsQueue = new();
        List<BoundsInt> roomsList = new();
        roomsQueue.Enqueue(spaceToSplit);

        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if(room.size.x>=minWidth && room.size.y >= minWidth)
            {
                if (Random.value > 0.5f)
                {
                    if(room.size.y >= minHeight * 2)
                    {
                        SplitRoomHorizontally(roomsQueue,minWidth,room);
                    }
                    else if (room.size.x >= minWidth * 2)
                    {
                        SplitRoomVertically(roomsQueue,minWidth,room);
                    }
                    else
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitRoomVertically(roomsQueue,minWidth,room);
                    }
                    else if(room.size.y >= minHeight * 2)
                    {
                        SplitRoomHorizontally(roomsQueue,minWidth,room);
                    }
                    else
                    {
                        roomsList.Add(room);
                    }
                }  
            }
        }
        return roomsList;
    }
    private static void SplitRoomVertically(Queue<BoundsInt> roomsQueue, int minWidth, BoundsInt room)
    {
        int xSplit = Random.Range(1,room.size.x);

        BoundsInt room1 = new(room.min,new Vector3Int(xSplit,room.size.y,room.size.z));
        BoundsInt room2 = new(new Vector3Int(room.min.x+xSplit,room.min.y,room.min.z),
            new Vector3Int(room.size.x-xSplit,room.size.y,room.size.z));
        
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
    private static void SplitRoomHorizontally(Queue<BoundsInt> roomsQueue, int minWidth, BoundsInt room)
    {
        int ySplit = Random.Range(1,room.size.y);

        BoundsInt room1 = new(room.min, new Vector3Int(room.size.x, ySplit,room.size.y));
        BoundsInt room2 = new(new Vector3Int(room.min.x,room.min.y+ySplit,room.min.z),
            new Vector3Int(room.size.x, room.size.y-ySplit, room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
    #endregion
}

#region Helper Direction Class
public static class Direction2D
    {
        public static List<Vector2Int> cardinalDirection = new()
        {
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left
        };  
        public static List<Vector2Int> cardinalAndDiagonalDirection = new()
        {
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left,
            new Vector2Int(1,1),
            new Vector2Int(1,-1),
            new Vector2Int(-1,1),
            new Vector2Int(-1,-1)
        };

        public static Vector2Int GetRandomCardinalDirection()
        {
            return cardinalDirection[Random.Range(0,cardinalDirection.Count)];
        } 
    }
#endregion