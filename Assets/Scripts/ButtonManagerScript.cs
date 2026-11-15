using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonManagerScript : MonoBehaviour
{
    [Header("Room Configuration")]
    public RectTransform roomMenu;
    public TMP_InputField walkLength;
    public TMP_InputField iterations;
    public Button randomizeIterations;
    public Button minimizeRoom;

    [Header("SRW Configuration")]
    public RectTransform SRWMenu;
    public TMP_InputField corridorLength;
    public TMP_InputField corridorCount;
    public TMP_InputField roomPercentage;
    public Button minimizeSRW;

    [Header("BSP Configuration")]
    public RectTransform BSPMenu;
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
    public RectTransform generationMenu;
    public Button generateWalls;
    public Button room,SRW,BSP;
    public Button minimizeGeneration;

    [Header("Saving System")]
    public GameObject mainPanel;
    public GameObject panel1;
    public Button saveButton;
    public Button cancelButton;
    public TMP_InputField fileName;
    public GameObject panel2;
    public Button copyToClipboard;
    public Button closeButton;

    [Header("Quit Settings")]
    public Button quitButton;

    [Header("Refferences")]
    [SerializeField] private SimpleRandomWalkDungeonGenerator simpleRandomWalkDungeonGenerator;
    [SerializeField] private CorridorFirstDungeonGenerator corridorFirstDungeonGenerator;
    [SerializeField] private RoomFirstDungeonGenerator roomFirstDungeonGenerator;
    [SerializeField] private ToolManagerScript toolManagerScript;
    [SerializeField] private new Camera camera;
    [SerializeField] private Sprite boolTrueImage;
    [SerializeField] private Sprite boolFalseImage;

    [Header("Fields")]
    [SerializeField] private bool startRandomlyEachIteration = true;
    [SerializeField] private bool generateWallAutomatically = true;
    [SerializeField] private bool useRandomWalkForRoomGenerations = false;
    [SerializeField] private float sideMenuHiddenX;
    [SerializeField] private float generationMenuHiddenY;
    [SerializeField] private float animationTime;
    private bool roomMenuVisibility = true;
    private bool SRWMenuVisibility = true;
    private bool BSPMenuVisibility = true;
    private bool generationMenuVisibility = true;
    private string filePath;
    private int cameraOrtho;

    void OnEnable()
    {
        room.onClick.AddListener(GenerateRoomOnly);
        randomizeIterations.onClick.AddListener(() => startRandomlyEachIteration = ToggleBool(randomizeIterations,startRandomlyEachIteration));
        generateWalls.onClick.AddListener(() => generateWallAutomatically = ToggleBool(generateWalls,generateWallAutomatically));
        SRW.onClick.AddListener(GenerateSRW);
        SRWForRooms.onClick.AddListener(() => useRandomWalkForRoomGenerations = ToggleBool(SRWForRooms,useRandomWalkForRoomGenerations));
        BSP.onClick.AddListener(GenerateBSP);
        cameraReset.onClick.AddListener(ResetCamera);
        cameraZoomIn.onClick.AddListener(CameraZoomIn);
        cameraZoonOut.onClick.AddListener(CameraZoomOut);
        saveButton.onClick.AddListener(SaveDungeon);
        saveButton.onClick.AddListener(HidePanel1);
        closeButton.onClick.AddListener(CloseButtonSave);
        copyToClipboard.onClick.AddListener(CopyToClipboard);
        cancelButton.onClick.AddListener(CloseButtonSave);
        minimizeRoom.onClick.AddListener(()=>roomMenuVisibility = HideSideMenus(roomMenu,roomMenuVisibility));
        minimizeBSP.onClick.AddListener(()=>BSPMenuVisibility = HideSideMenus(BSPMenu,BSPMenuVisibility));
        minimizeSRW.onClick.AddListener(()=>SRWMenuVisibility = HideSideMenus(SRWMenu,SRWMenuVisibility));
        minimizeGeneration.onClick.AddListener(()=>generationMenuVisibility = HideUpMenus(generationMenu,generationMenuVisibility));
        quitButton.onClick.AddListener(QuitTool);
    }


    void OnDisable()
    {
        room.onClick.RemoveAllListeners();
        randomizeIterations.onClick.RemoveAllListeners();
        generateWalls.onClick.RemoveAllListeners();
        SRW.onClick.RemoveAllListeners();
        SRWForRooms.onClick.RemoveAllListeners();
        BSP.onClick.RemoveAllListeners();
        cameraReset.onClick.RemoveAllListeners();
        cameraZoomIn.onClick.RemoveAllListeners();
        cameraZoonOut.onClick.RemoveAllListeners();
        saveButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        copyToClipboard.onClick.RemoveAllListeners();
        minimizeRoom.onClick.RemoveAllListeners();
        minimizeBSP.onClick.RemoveAllListeners();
        minimizeSRW.onClick.RemoveAllListeners();
        minimizeGeneration.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }

    private void Awake()
    {
        cameraOrtho = 50;
        camera.orthographicSize = cameraOrtho;
    }
    private void GenerateRoomOnly()
    {
        string i = iterations.text;
        string w = walkLength.text;
        if(i=="") i = "70";
        if(w=="") w = "30";
        ToolManagerScript.instance.ClearContainers();
        simpleRandomWalkDungeonGenerator.GenerateRooms(int.Parse(i),int.Parse(w),
            startRandomlyEachIteration,generateWallAutomatically);
    }
    private void GenerateSRW()
    {
        string i = iterations.text;
        string w = walkLength.text;
        string cl = corridorLength.text;
        string cc = corridorCount.text;
        string rp = roomPercentage.text;
        if(i=="") i = "70";
        if(w=="") w = "30";
        if(cl=="") cl = "10";
        if(cc=="") cc = "10";
        if(rp=="") rp = "99";
        ToolManagerScript.instance.ClearContainers();
        corridorFirstDungeonGenerator.StartGenerationFromButton(int.Parse(i),int.Parse(w),
            startRandomlyEachIteration,generateWallAutomatically,int.Parse(cl),int.Parse(cc),int.Parse(rp));
    }
    private void GenerateBSP()
    {
        string i = iterations.text;
        string w = walkLength.text;
        string dw = dungeonSizeX.text;
        string dh = dungeonSizeY.text;
        string mrw = minRoomSizeX.text;
        string mrh = minRoomSizeY.text;
        string o = offset.text;
        if(i=="") i = "70";
        if(w=="") w = "30";
        if(dw=="") dw = "70";
        if(dh=="") dh = "70";
        if(mrw=="") mrw = "20";
        if(mrh=="") mrh = "20";
        if(o=="") o = "3";
        ToolManagerScript.instance.ClearContainers();
        roomFirstDungeonGenerator.StartGenerationFromButton(int.Parse(i),int.Parse(w),
            startRandomlyEachIteration,generateWallAutomatically,int.Parse(dw),int.Parse(dh),int.Parse(mrw),int.Parse(mrh),
                useRandomWalkForRoomGenerations,int.Parse(o));
    }
    private void ResetCamera()
    {
        cameraOrtho = 20;
        camera.orthographicSize = 20;
    }
    private void CameraZoomOut()
    {
        for(int i = 1; i<=50; i++)
        {
            int nextCameraOrtho = i+cameraOrtho;
            if (nextCameraOrtho % 50 == 0)
            {
                cameraOrtho = nextCameraOrtho;
                cameraOrtho = Mathf.Clamp(cameraOrtho,20,701);
                camera.orthographicSize = cameraOrtho;
                return;
            }
        }
    }
    private void CameraZoomIn()
    {
        for(int i = -1; i>=-50; i--)
        {
            int nextCameraOrtho = cameraOrtho+i;
            if (nextCameraOrtho % 50 == 0)
            {
                cameraOrtho = nextCameraOrtho;
                cameraOrtho = Mathf.Clamp(cameraOrtho,20,701);
                camera.orthographicSize = cameraOrtho;
                return;
            }
        }
    }
    private void SaveDungeon()
    {
        if(fileName.text == "")
        {
            filePath = toolManagerScript.SaveDungeon("Dungeon Data");
        }
        else
        {
            filePath = toolManagerScript.SaveDungeon(fileName.text);
        }
    }
    private void CloseButtonSave()
    {
        mainPanel.SetActive(false);
    }
    private void CopyToClipboard()
    {
        GUIUtility.systemCopyBuffer = filePath;
    }
    private void HidePanel1()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
    }
    private bool HideSideMenus(RectTransform menu, bool isOpen)
    {
        if (isOpen)
        {
            menu.DOAnchorPos(new Vector2 (menu.anchoredPosition.x+sideMenuHiddenX,menu.anchoredPosition.y),animationTime).SetEase(Ease.OutBack);
        }
        else
        {
            menu.DOAnchorPos(new Vector2 (menu.anchoredPosition.x-sideMenuHiddenX,menu.anchoredPosition.y),animationTime).SetEase(Ease.OutBack);
        }
        return !isOpen;
    }
    private bool HideUpMenus(RectTransform menu, bool isOpen)
    {
        if (isOpen)
        {
            menu.DOAnchorPos(new Vector2 (menu.anchoredPosition.x,menu.anchoredPosition.y+generationMenuHiddenY),animationTime).SetEase(Ease.OutBack);
        }
        else
        {
            menu.DOAnchorPos(new Vector2 (menu.anchoredPosition.x,menu.anchoredPosition.y-generationMenuHiddenY),animationTime).SetEase(Ease.OutBack);
        }
        return !isOpen;
    }
    private void QuitTool()
    {
        Application.Quit();
    }

    private bool ToggleBool(Button button, bool statement)
    {
        statement = !statement;
        if (statement)
        {
            button.image.sprite = boolTrueImage;
        }
        else
        {
            button.image.sprite = boolFalseImage;
        } 
        return statement;
    }
}
