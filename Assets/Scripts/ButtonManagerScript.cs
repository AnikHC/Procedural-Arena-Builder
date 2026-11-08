using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManagerScript : MonoBehaviour
{
    [Header("Room Configuration")]
    public TMP_InputField walkLength;
    public TMP_InputField iterations;
    public Button randomizeIterations;
    public Button minimizeRoom;

    [Header("SRW Configuration")]
    public TMP_InputField corridorLength;
    public TMP_InputField corridorCount;
    public TMP_InputField roomPercentage;
    public Button minimizeSRW;

    [Header("BSP Configuration")]
    public TMP_InputField dungeonSizeX,dungeonSizeY;
    public TMP_InputField minRoomSizeX,minRoomSizeY;
    public Button SRWForRooms;
    public TMP_InputField offset;
    public Button minimizeBSP;

    [Header("Camera Configurations")]
    public Button cameraReset;
    public Button cameraZoomIn;
    public Button cameraZoonOut;

    [Header("Generation Choices")]
    public Button generateWalls;
    public Button room,SRW,BSP;
    public Button minimizeGeneration;
}
