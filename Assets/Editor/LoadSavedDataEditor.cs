using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LoadSavedDungeonData),false)]
public class LoadSavedDataEditor : Editor
{
    LoadSavedDungeonData generator;
    public void OnEnable()
    {
        generator = (LoadSavedDungeonData)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Load Dungeon"))
        {
            generator.LoadData();
        }
    }
}
