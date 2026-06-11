using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public int index;

    public bool state = false;

    public PuzzleManager manager;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        UpdateColor();
    }

    private void OnMouseDown()
    {
        state = !state;

        manager.currentState[index] = state;

        UpdateColor();

        manager.CheckPuzzle();
    }

    void UpdateColor()
    {
        if(sr != null)
        {
            sr.color = state ? Color.green : Color.white;
        }
    }
}
