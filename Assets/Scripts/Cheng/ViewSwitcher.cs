using UnityEngine;

public class ViewSwitcher : MonoBehaviour
{
    public GameObject currentView;
    public GameObject targetView;

    public void SwitchView()
    {
        currentView.SetActive(false);
        targetView.SetActive(true);
    }
}