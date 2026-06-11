using UnityEngine;
using System.Collections.Generic;
using System;
using System.Security.Cryptography.X509Certificates;
using TMPro;

public class CameraMap : MonoBehaviour
{
    public enum CameraView
    {
        Room0,
        Room1,
        Room2,
        Room3,
        Cabinet,
        PasswordPanel,
        DoorLock,
        PasswordPanelTable,
        OpenedDrawer

    }

    private struct CameraPose
    {
        public Vector3 position;
        public Vector3 rotation;
        public float orthographicSize;

        public CameraPose(Vector3 pos, Vector3 rot, float ogS)
        {
            position = pos;
            rotation = rot;
            orthographicSize = ogS;
        }
    }
    private Dictionary<CameraView, CameraPose> cameraMaps = new Dictionary<CameraView, CameraPose>();

    private float MoveChunk, RoomNum, StartCamSize, StartPosY;
    private Vector3 StartRot;

    void Start()
    {
        var gameManager = this.GetComponent<GameManager>();
        MoveChunk = gameManager.MoveChuck;
        RoomNum = Mathf.CeilToInt(gameManager.RightLimit / MoveChunk);
        StartCamSize = gameManager.Start_CameraSize;
        StartPosY = gameManager.Start_Position.y;
        StartRot = gameManager.Start_Rotation;
        
        for(int i = 0; i <= RoomNum; i++)
        {
            CameraView roomKey = (CameraView)i;
            Vector3 roomPos = new Vector3(MoveChunk * i, StartPosY, -10f);
            cameraMaps[roomKey] = new CameraPose(roomPos, StartRot, StartCamSize);
        }
        //------------Name----------------------------------------------------Posistion-------------------------------Rotation--------CameraSize
        cameraMaps[CameraView.Cabinet]            = new CameraPose(new Vector3(-0.6f, 2f, -10f),          new Vector3(15f, 0f, 0f),     1f);
        cameraMaps[CameraView.PasswordPanel]      = new CameraPose(new Vector3(-0.6f, 2f, -10f),          new Vector3(16.4f, 0f, 0f),   0.3f);
        cameraMaps[CameraView.DoorLock]           = new CameraPose(new Vector3(7.65f, 2f, -10f),           new Vector3(12f, 0f, 0f),     0.5f);        
        cameraMaps[CameraView.PasswordPanelTable] = new CameraPose(new Vector3(23.567f, -0.061f, -0.816f),new Vector3(12f, 19f, 0f),    0.2f);
        cameraMaps[CameraView.OpenedDrawer]       = new CameraPose(new Vector3(-0.6f,2f,-0.65f),          new Vector3(56f,0f,0f),       1f);
    }
    public (Vector3 Position, Vector3 Rotation, float orthographicSize) BackThePosition(float camPos)
    {
        int j = (int)(camPos / MoveChunk);
        CameraView Key = (CameraView)j;
        this.GetComponent<GameManager>().IsZoomIn = false;
        return (cameraMaps[Key].position, cameraMaps[Key].rotation, cameraMaps[Key].orthographicSize);
    }

    public (Vector3 Position, Vector3 Rotation, float orthographicSize) CheckIsCorrectTag(string TagName, float camPos)
    {
        if (Enum.TryParse<CameraView>(TagName, out CameraView ListPos))
        {
            CameraView Key = (CameraView)ListPos;
            this.GetComponent<GameManager>().IsZoomIn = true;
            return (cameraMaps[Key].position, cameraMaps[Key].rotation, cameraMaps[Key].orthographicSize);
        }
        else
        {
            this.GetComponent<GameManager>().IsZoomIn = false;
            return BackThePosition(camPos);
        }
    }

}
