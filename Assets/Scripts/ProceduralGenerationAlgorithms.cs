using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    public static HashSet<Vector2Int> SimpleRandomWalkGenerator(Vector2Int startPostion, int walkLength)
    {
        HashSet<Vector2Int> path = new()
        {
            startPostion
        };

        var previousPosition = startPostion;

        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }

        return path;
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
}
