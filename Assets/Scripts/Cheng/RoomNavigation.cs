using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    public GameObject currentRoom;
    public GameObject nextRoom;

    public void GoRight()
    {
        currentRoom.SetActive(false);
        nextRoom.SetActive(true);
    }
}