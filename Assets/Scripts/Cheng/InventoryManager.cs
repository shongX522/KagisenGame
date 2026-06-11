using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public Image itemSlot1;

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(Sprite itemIcon)
    {
        itemSlot1.sprite = itemIcon;
        itemSlot1.enabled = true;

        Debug.Log("取得道具");
    }
}