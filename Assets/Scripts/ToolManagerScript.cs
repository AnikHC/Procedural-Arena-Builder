using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
public class ToolManagerScript : MonoBehaviour
{
    public static ToolManagerScript instance;
    public HashSet<Vector2Int> floorPositions;
    public int floorCount;
    public List<Vector2Int> straightUpDown_Left;
    public List<Vector2Int>straightUpDown_Right;
    public List<Vector2Int> straightLeftRight_Up;
    public List<Vector2Int> straightLeftRight_Down;
    public List<Vector2Int> downRightCorner;
    public List<Vector2Int> downLeftCorner;
    public List<Vector2Int> bentLeft;
    public List<Vector2Int> bentRight;
    public string folderName;
    [TextArea(3,5)] public string savedFileLocation;

    [SerializeField]private SavingDungeonData savingDungeonData;
    [SerializeField]private LoadSavedDungeonData loadSavedDungeonData;

    void Awake()
    {
        instance = this;
        ClearContainers();
    }
    void OnEnable()
    {
        instance = this;
        ClearContainers();
    }
    public void ClearContainers()
    {
        floorPositions = new();
        floorCount = 0;
        straightUpDown_Left = new();
        straightUpDown_Right = new();
        straightLeftRight_Up = new();
        straightLeftRight_Down = new();
        downRightCorner = new();
        downLeftCorner = new();
        bentLeft = new();
        bentRight = new();
    }

    public void SaveDungeon()
    {
        savingDungeonData.SaveData(folderName);
    }
    public void LoadSavedData()
    {
        loadSavedDungeonData.LoadData(savedFileLocation);
    }
}
