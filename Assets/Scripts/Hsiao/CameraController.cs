using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera Camera;
    float rightLimit, moveChunk;
    public float camPos;

    void Awake()
    {
        Camera = Object.FindFirstObjectByType<Camera>();

        var gameManarger = this.GetComponent<GameManager>();
        rightLimit = gameManarger.RightLimit;
        moveChunk = gameManarger.MoveChuck;
        Camera.transform.position = gameManarger.Start_Position;
        Camera.transform.rotation = Quaternion.Euler(gameManarger.Start_Rotation);
        Camera.orthographicSize  = gameManarger.Start_CameraSize;
    }
    public void Left()
    {
        if(camPos > 0f) camPos -= moveChunk;
        else camPos = rightLimit;
        Move();
    }

    public void Right()
    {
        if(camPos < rightLimit) camPos += moveChunk;
        else camPos = 0f;
        Move();
    }

    private void Move()
    {
        Camera.transform.position = new Vector3(camPos, this.GetComponent<GameManager>().Start_Position.y, -10f);
    }
    public void BackRoom()
    {
        var (Pos, Rot, ogS) = this.GetComponent<CameraMap>().BackThePosition(camPos);
        Camera.transform.position = Pos;
        Camera.transform.rotation = Quaternion.Euler(Rot);
        Camera.orthographicSize = ogS;
    }

    public void ZoomIn(string TagName)
    {
        if(TagName != "Untagged")
        {
            var (Pos, Rot, ogS) = this.GetComponent<CameraMap>().CheckIsCorrectTag(TagName, camPos);
            Camera.transform.position = Pos;
            Camera.transform.rotation = Quaternion.Euler(Rot);
            Camera.orthographicSize = ogS;
        }
    }
    public bool checkRoomPos(int roomNumber)
    {
        int i = (int)(camPos/moveChunk);
        if(i == roomNumber) return true; else return false;
    }
}
