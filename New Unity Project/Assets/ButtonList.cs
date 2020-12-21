using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonList : MonoBehaviour
{
    public Button b_Start;
    public Button b_Exit;
    public Button b_Host;
    public Button b_Join;
    public Button b_Back;
    public Button b_Settings;
    public Button b_Credits;

    public GameObject g_TitleMenu;
    public GameObject g_JoinHostMenu;

    // Start is called before the first frame update
    void Start()
    {
        b_Start.onClick.AddListener(GoToHostJoin);
        b_Start.onClick.AddListener(QuitGame);
        b_Back.onClick.AddListener(BackToTitle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GoToHostJoin()
    {
        g_TitleMenu.SetActive(false);
        g_JoinHostMenu.SetActive(true);

        Debug.Log("You have clicked the button!");
    }

    void QuitGame()
    {
        Application.Quit();

        Debug.Log("You have clicked the button!");
    }
    
    void BackToTitle()
    {
        g_TitleMenu.SetActive(true);
        g_JoinHostMenu.SetActive(false);

        Debug.Log("You have clicked the button!");
    }
}
