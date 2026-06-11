using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class MenuController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button Start;
    [SerializeField] Button Option;
    [SerializeField] Button Exit;
    [SerializeField] Button Back;

    [Header("Stages")]
    [SerializeField] Button [] Stages = new Button[2];

    private int countStages;
    private bool isInStageMenu;

    void Awake()
    {
        if(Start != null) Start.onClick.AddListener(ChooseStages);
        if(Option != null) Option.onClick.AddListener(OptionPage) ; Debug.LogError("not found Option Button");
        if(Exit != null) Exit.onClick.AddListener(ExitGame);Debug.LogError("not found Exit Button");
        if(Back != null) 
        {
            Back.onClick.AddListener(MainMenu);
            Back.gameObject.SetActive(false);
        }
        
        countStages = Stages.Length;
        for(int i = 0; i < countStages; ++i) Stages[i].gameObject.SetActive(false);
    }

    private void ChooseStages()
    {
        isInStageMenu = true;
        for(int i = 0; i < countStages; ++i) Stages[i].gameObject.SetActive(true);
        Start.gameObject.SetActive(false);
        Option.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);
        Back.gameObject.SetActive(true);
    }

    private void MainMenu()
    {
        if(isInStageMenu) for(int i = 0; i < countStages; ++i) Stages[i].gameObject.SetActive(false);
        Start.gameObject.SetActive(true);
        Option.gameObject.SetActive(true);
        Exit.gameObject.SetActive(true);
        Back.gameObject.SetActive(false);
    }
    private void OptionPage()
    {
        Start.gameObject.SetActive(false);
        Option.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);
        Back.gameObject.SetActive(true);
    }
    private void ExitGame()
    {
        Application.Quit();
    }

    //Function of choose stages
    public void stagesSelect(string Scene_Name)
    {
        try
        {
            SceneManager.LoadScene(Scene_Name);
        }
        catch
        {
            Debug.Log("Not found Scene");
            return;
        }
    }
}
