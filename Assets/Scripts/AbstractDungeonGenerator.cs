using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteAlways]
public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField] protected Vector2Int startingPosition = Vector2Int.zero;
    [SerializeField] protected TilemapVisualizer tilemapVisualizer = null;
    [SerializeField] protected new Camera camera;
    [SerializeField] [Range(5,500)] private float cameraOrtho;
    public bool generateWallAutomatically = false;
    protected HashSet<Vector2Int> floorPositions;

    public void GenerateDungeon()
    {
        ToolManagerScript.instance.ClearContainers();
        RunProceduralGeneration(); 
    }
    public void GenerateWalls()
    {
        RunWallGenerations();
    }
    public void ClearTile()
    {
        ToolManagerScript.instance.ClearContainers();
        tilemapVisualizer.ClearTiles();
    }
    public void ResetCamera()
    {
        cameraOrtho = 20;
        camera.orthographicSize = 20;
    }
    private void Update()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (scroll != 0)
        {
            cameraOrtho -= scroll ;

            cameraOrtho = Mathf.Clamp(cameraOrtho, 5, 500);

            camera.orthographicSize = cameraOrtho;
        }
    }
    protected void CenterCamera(HashSet<Vector2Int> floorPositions)
    {
        int xMax = int.MinValue, yMax = int.MinValue;
        int xMin = int.MaxValue, yMin = int.MaxValue;

        foreach (var floorPosition in floorPositions)
        {
            if(floorPosition.x > xMax) xMax = floorPosition.x;
            else if(floorPosition.x < xMin) xMin = floorPosition.x;

            if(floorPosition.y > yMax) yMax=floorPosition.y;
            else if(floorPosition.y < yMin) yMin = floorPosition.y;
        }

        int xMid = Mathf.Abs(xMax-xMin);
        int yMid = Mathf.Abs(yMax-yMin);

        int camX = xMin+xMid/2;
        int camY = yMin+yMid/2;
        
        camera.transform.position = new Vector3(camX,camY,-10);
    }
    protected void SaveFloorPositions(HashSet<Vector2Int> floorPositions)
    {
        ToolManagerScript.instance.floorPositions.UnionWith(floorPositions);
        ToolManagerScript.instance.floorCount = ToolManagerScript.instance.floorPositions.Count;
    }
    private void OnValidate()
    {
        camera.orthographicSize = cameraOrtho;
    }

    protected abstract void RunProceduralGeneration();
    protected abstract void RunWallGenerations();
}
