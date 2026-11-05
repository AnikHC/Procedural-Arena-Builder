using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
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

}

    public static class Direction2D
    {
        public static List<Vector2Int> cardinalDirection = new()
        {
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left
        };  

        public static Vector2Int GetRandomCardinalDirection()
        {
            return cardinalDirection[Random.Range(0,cardinalDirection.Count)];
        } 
    }
