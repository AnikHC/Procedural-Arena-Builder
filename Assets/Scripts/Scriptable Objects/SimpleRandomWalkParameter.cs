using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkParameter_", menuName = "Dungeon Parameters/SimpleRandomWalkParameter_")]
public class SimpleRandomWalkParameter : ScriptableObject
{
    public int iterations = 10, walkLength = 10;
    public bool startRandomlyEachIteration = true;
}
