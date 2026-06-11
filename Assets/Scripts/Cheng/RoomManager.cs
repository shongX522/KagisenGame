using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] rooms;

    private int currentRoom = 0;

    private void Start()
    {
        ShowRoom(currentRoom);
    }

    void ShowRoom(int index)
    {
        for(int i = 0; i < rooms.Length; i++)
        {
            rooms[i].SetActive(i == index);
        }
    }

    public void GoRight()
    {
        if(currentRoom < rooms.Length - 1)
        {
            currentRoom++;
            ShowRoom(currentRoom);
        }
    }

    public void GoLeft()
    {
        if(currentRoom > 0)
        {
            currentRoom--;
            ShowRoom(currentRoom);
        }
    }
}