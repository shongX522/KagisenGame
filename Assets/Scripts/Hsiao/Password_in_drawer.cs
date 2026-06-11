using System.Collections.Generic;
using UnityEngine;


public class Password_in_drawer : MonoBehaviour
{
    [SerializeField] private GameObject GameManager;
    [SerializeField] private bool[] passwordList = new bool[8];
    [SerializeField] private bool[] correctPassWord = new bool[8];
    [SerializeField] private Color selectedColor = Color.red;
    [SerializeField] private Color normalColor = Color.white;


    [SerializeField] private List<GameObject> Buttons = new List<GameObject>();

    void Update()
    {
        
        if (GameManager.GetComponent<GameManager>().IsPasswordLocking_drawer) //link the gamemanager
        {
            if(IsPasswordCorrect()) 
            {
                GameManager.GetComponent<GameManager>().IsPasswordLocking_drawer = false; //link the gamemanager
                GameManager.GetComponent<GameManager>().Dummy = true; 
            }
        }
        setTheColor();
    }
    private bool IsPasswordCorrect()
    {
        for(int i = 0; i < 8 ; i++)
        {
            if(passwordList[i] != correctPassWord[i])
            {
                return false;
            }
        }
        return true;
    }
    public void changeTheBool(string Name)
    {
        int i = int.Parse(Name);
        if(i >= 0 && i <= 7)
        {
            if(passwordList[i]) passwordList[i] = false;
            else passwordList[i] = true; 
        }
    }
    
    private void setTheColor()
    {
        for(int i = 0; i < 8; i++)
        {
            Renderer btnRenderer = Buttons[i].GetComponent<Renderer>();
            if(passwordList[i])
            {
                btnRenderer.material.color = selectedColor;
            }
            else
            {
                btnRenderer.material.color = normalColor;
            }
        }
    }
}
