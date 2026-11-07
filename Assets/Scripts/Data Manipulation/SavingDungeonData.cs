using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class DungeonData
{
    public List<Vector2Int> floorPositions;
    public List<Vector2Int> straightUpDown_Left;
    public List<Vector2Int>straightUpDown_Right;
    public List<Vector2Int> straightLeftRight_Up;
    public List<Vector2Int> straightLeftRight_Down;
    public List<Vector2Int> downRightCorner;
    public List<Vector2Int> downLeftCorner;
    public List<Vector2Int> bentLeft;
    public List<Vector2Int> bentRight;
}
[ExecuteAlways]
public class SavingDungeonData : MonoBehaviour
{
    private DungeonData dungeonData = new();
    [SerializeField] private ToolManagerScript toolManagerScript;

    public void SaveData(string fileName)
    {
        dungeonData.floorPositions = toolManagerScript.floorPositions.ToList();
        dungeonData.straightUpDown_Left = toolManagerScript.straightUpDown_Left;
        dungeonData.straightUpDown_Right = toolManagerScript.straightUpDown_Right;
        dungeonData.straightLeftRight_Up = toolManagerScript.straightLeftRight_Up;
        dungeonData.straightLeftRight_Down = toolManagerScript.straightLeftRight_Down;
        dungeonData.downLeftCorner = toolManagerScript.downLeftCorner;
        dungeonData.downRightCorner = toolManagerScript.downRightCorner;
        dungeonData.bentLeft = toolManagerScript.bentLeft;
        dungeonData.bentRight = toolManagerScript.bentRight;

        Debug.Log(toolManagerScript.floorPositions.Count+" "+toolManagerScript.straightUpDown_Left.Count+" "+
            toolManagerScript.straightUpDown_Right.Count+" "+toolManagerScript.straightLeftRight_Up.Count+" "+
            toolManagerScript.straightLeftRight_Down.Count+" "+toolManagerScript.downLeftCorner.Count+" "+
            toolManagerScript.bentLeft.Count+" "+toolManagerScript.bentRight.Count);
        Debug.Log(dungeonData.floorPositions.Count+" "+dungeonData.straightUpDown_Left.Count+" "+
            dungeonData.straightUpDown_Right.Count+" "+dungeonData.straightLeftRight_Up.Count+" "+
            dungeonData.straightLeftRight_Down.Count+" "+dungeonData.downLeftCorner.Count+" "+
            dungeonData.bentLeft.Count+" "+dungeonData.bentRight.Count);

        string folderPath = Path.Combine(Application.persistentDataPath, fileName);
        string filePath = Path.Combine(folderPath,"Dungeon Data.json");

        string json = JsonUtility.ToJson(dungeonData,true);
        Directory.CreateDirectory(folderPath);
        File.WriteAllText(filePath,json);

        Debug.Log("File saved at path : "+folderPath);
    }
}
