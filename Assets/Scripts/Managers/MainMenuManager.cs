using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header ("Main Menu")]
    public GameObject mainMenuPanel;            //Panel Main menu 
    public GameObject deathmatch;
    public GameObject options;
    public bool inMainMenu = true;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && inMainMenu)
            Exit();
        else if (Input.GetKeyDown(KeyCode.Escape) && !inMainMenu)
        {
            deathmatch.SetActive(false);
            options.SetActive(false);
        }
    }

    public void Deathmatch()
    {
        deathmatch.SetActive(true);
        inMainMenu = false;
    }

    public void Options()
    {
        options.SetActive(true);
        inMainMenu = false;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

}
