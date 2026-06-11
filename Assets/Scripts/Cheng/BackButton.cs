using Unity.VisualScripting;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    [Header("房間畫面")]
    public GameObject roomView;

    [Header("Zoom畫面")]
    public GameObject drawerPuzzleView;

    public GameObject drawerOpenView;

    void Awake()
    {
        this.gameObject.SetActive(false);  //開啟遊戲不會有返回鍵
    }
    public void GoBack()
    {
        Debug.Log("BackButton Clicked!");

        // 關閉所有抽屜畫面
        if (drawerPuzzleView != null)
        {
            drawerPuzzleView.SetActive(false);
        }

        if (drawerOpenView != null)
        {
            drawerOpenView.SetActive(false); //如果玩家沒拿key直接按返回劍會有bug
        }

        // 回到房間
        if (roomView != null)
        {
            roomView.SetActive(true);
        }

        // 隱藏返回鍵
        gameObject.SetActive(false);

        Debug.Log("返回 Room1");
    }
}