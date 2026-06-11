using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("Message Panel")]
    [SerializeField] public GameObject TextPanel;
    [SerializeField] public GameObject FinishGamePanel;

    [Header("Buttons")]
    [SerializeField] public Button ButtonLeft;
    [SerializeField] public Button ButtonRight;
    [SerializeField] public Button ButtonReturn;
    [SerializeField] public Button ButtonHomeReturn;

    [Header("Check the boolean")]
    public bool FadeIn = false;

    void Awake()
    {
        //Find the camera Controller script
        var CamCtrl = this.GetComponent<CameraController>();

        if(CamCtrl == null)
        {
            Debug.LogError("not found camera controller!");
            return;
        }
        //check the buttons are all selected, if not return the error msg to screen.
        if(ButtonLeft != null) ButtonLeft.onClick.AddListener(CamCtrl.Left); else Debug.LogError("not found Left Button!");
        if(ButtonRight != null) ButtonRight.onClick.AddListener(CamCtrl.Right); else Debug.LogError("not found Right Button!");
        if(ButtonReturn != null) ButtonReturn.onClick.AddListener(CamCtrl.BackRoom) ; else Debug.LogError("not found Return Button!");
        if(ButtonHomeReturn != null) ButtonHomeReturn.onClick.AddListener(BackToMainMenu) ; else Debug.LogError("not found Home Button!");


        ActiveLeftRightButton();
        FinishGamePanel.GetComponent<CanvasGroup>().alpha = 0;
        FinishGamePanel.SetActive(false);
    }

    public void IgnoreLeftRightButton()
    {
        ButtonLeft.gameObject.SetActive(false);
        ButtonRight.gameObject.SetActive(false);
        ButtonReturn.gameObject.SetActive(true);
    }
    public void ActiveLeftRightButton()
    {
        ButtonLeft.gameObject.SetActive(true);
        ButtonRight.gameObject.SetActive(true);
        ButtonReturn.gameObject.SetActive(false);
    }
    public void ActiveText(string TextToShow)
    {
        StartCoroutine(ShowTheText(TextToShow));
    }
    private IEnumerator ShowTheText(string TextToPanel)
    {
        TextPanel.GetComponentInChildren<Text>().text = TextToPanel;
        TextPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        TextPanel.SetActive(false);
    }
    void Update()
    {
        if(FadeIn)
        {
            ButtonLeft.gameObject.SetActive(false);
            ButtonRight.gameObject.SetActive(false);
            ButtonReturn.gameObject.SetActive(false);

            if (FinishGamePanel.GetComponent<CanvasGroup>().alpha < 1)
            {
                FinishGamePanel.GetComponent<CanvasGroup>().alpha += Time.deltaTime;
                if(FinishGamePanel.GetComponent<CanvasGroup>().alpha >= 1)
                {
                    FinishGamePanel.SetActive(true);
                    FadeIn = false;
                }
            }
        }
    }
        //Home return button
    public void BackToMainMenu()
    {
        Debug.Log("Home");
        SceneManager.LoadScene("MainMenu");
    }
}
