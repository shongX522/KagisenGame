using UnityEngine;

public class ZoomTrigger : MonoBehaviour
{
    public GameObject currentView;

    public GameObject zoomView;

    private void OnMouseDown()
    {
        currentView.SetActive(false);

        zoomView.SetActive(true);
    }
}
