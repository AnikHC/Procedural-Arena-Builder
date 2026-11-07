using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
public class DungeonDatas
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
public class LoadSavedDungeonData : MonoBehaviour
{
    DungeonDatas dungeonDatas = new();

    #region FloorGeneration
    [SerializeField] private Tilemap floorTileMap;
    [SerializeField] private TileBase floorTile;
    [SerializeField] private Tilemap wallTileMap;
    public TileBase straightUpDown_Left, straightUpDown_Right, straightLeftRight_Up, straightLeftRight_Down, downRightCorner, downLeftCorner, bentLeft, bentRight;
    public void GenerateFloorTiles(IEnumerable <Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions,floorTileMap,floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tileMap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(position,tileMap,tile);
        }
    }

    private void PaintSingleTile(Vector2Int position, Tilemap tileMap, TileBase tile)
    {
        var tilePosition = tileMap.WorldToCell((Vector3Int)position);
        tileMap.SetTile(tilePosition,tile);
    }
    public void PaintWallTile(List<Vector2Int> wallPosition, TileBase wallTile)
    {
        PaintTiles(wallPosition,wallTileMap,wallTile);
    }
    #endregion

    public void LoadData(string fileLocation)
    {
        string json = File.ReadAllText(fileLocation);

        dungeonDatas = JsonUtility.FromJson<DungeonDatas>(json);
        Debug.Log(dungeonDatas.floorPositions.Count+" "+dungeonDatas.straightUpDown_Left.Count+" "+
            dungeonDatas.straightUpDown_Right.Count+" "+dungeonDatas.straightLeftRight_Up.Count+" "+
            dungeonDatas.straightLeftRight_Down.Count+" "+dungeonDatas.downLeftCorner.Count+" "+dungeonDatas.downRightCorner.Count+" "+
            dungeonDatas.bentLeft.Count+" "+dungeonDatas.bentRight.Count);   

        GenerateFloorTiles(dungeonDatas.floorPositions);
        PaintWallTile(dungeonDatas.straightUpDown_Left,straightUpDown_Left);
        PaintWallTile(dungeonDatas.straightUpDown_Right,straightUpDown_Right);
        PaintWallTile(dungeonDatas.straightLeftRight_Up,straightLeftRight_Up);
        PaintWallTile(dungeonDatas.straightLeftRight_Down,straightLeftRight_Down);
        PaintWallTile(dungeonDatas.downLeftCorner,downLeftCorner);
        PaintWallTile(dungeonDatas.downRightCorner,downRightCorner);
        PaintWallTile(dungeonDatas.bentLeft,bentLeft);
        PaintWallTile(dungeonDatas.bentRight,bentRight);
    }
    
}
