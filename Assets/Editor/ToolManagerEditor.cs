using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

[CustomEditor(typeof(ToolManagerScript),false)]
public class ToolManagerEditor : Editor
{
    ToolManagerScript generator;
    void OnEnable()
    {
        generator = (ToolManagerScript)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Save Dungeon Data"))
        {
            generator.SaveDungeon();
        }

        if(GUILayout.Button("Load Dungeon Data"))
        {
            generator.LoadSavedData();
        }
    }
}