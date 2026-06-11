using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    private void OnMouseEnter()
    {
        Debug.Log("滑鼠進入");
    }

    private void OnMouseDown()
    {
        Debug.Log("點到了");
    }
}