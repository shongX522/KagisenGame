using UnityEngine;

public class DrawerInteraction : MonoBehaviour
{
    public GameObject roomView;

    public GameObject puzzleView;

    public GameObject openView;

    public GameObject backButton;

    private void OnMouseDown()
    {
        Debug.Log("櫃子被點擊");

        roomView.SetActive(false);

        backButton.SetActive(true);

        if (PuzzleManager.Instance.solved)
        {
            Debug.Log("已解謎 → 開啟抽屜");

            openView.SetActive(true);
        }
        else
        {
            Debug.Log("未解謎 → 顯示謎題");

            puzzleView.SetActive(true);
        }
    }
}