using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractDungeonGenerator),true)]
public class DungeonGeneratorEditor : Editor
{
    AbstractDungeonGenerator generator;

    void OnEnable()
    {
        generator = (AbstractDungeonGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Create Dungeon"))
        {
            generator.GenerateDungeon();
        }
    }
}
