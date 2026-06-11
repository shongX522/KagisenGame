using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    [Header("目前狀態")]
    public bool[] currentState = new bool[4];

    [Header("正確答案")]
    public bool[] correctState = new bool[4];

    [Header("成功後顯示")]
    public GameObject openView;

    [Header("成功後隱藏")]
    public GameObject currentView;

    [Header("是否已解開")]
    public bool solved = false;

    private void Awake()
    {
        Instance = this;
    }
    public void CheckPuzzle()
    {
        // 已解開就不再檢查
        if (solved)
            return;

        for (int i = 0; i < 4; i++)
        {
            if (currentState[i] != correctState[i])
            {
                Debug.Log("錯誤");
                return;
            }
        }

        solved = true;

        Debug.Log("解謎成功！");

        // 顯示抽屜打開畫面
        if (openView != null)
        {
            openView.SetActive(true);

        }

        // 關閉謎題畫面
        if (currentView != null)
        {
            currentView.SetActive(false);
        }
    }
}