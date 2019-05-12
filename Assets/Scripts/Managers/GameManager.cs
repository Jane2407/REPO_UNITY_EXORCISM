using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayersManager playersManager;
    public GameObject pm;

    [Header("HUD")]
    public GameObject HUDpanel;                 //Hud panel
    public GameObject crown;                    //crown prefab
    public GameObject MaeCrownsPanel;           //Mae's Panel for placing crowns
    public GameObject RossCrownsPanel;          //Ross's Panel for placing crowns
    public TextMeshProUGUI roundText;           //Text for round number
    public TextMeshProUGUI roundTextInPanel;
    public GameObject wonText;                  //Text who won this round2
    public GameObject roundPanel;               //Round changing panel
    public GameObject gameOverPanel;            //Game over panel
    public TextMeshProUGUI winner;              //Winner text on game over panel

    [Header("Pause")]
    public GameObject pausePanel;               //Pause panel with button and text
    public static bool isPause;                 //Is game paused?
     bool isOver;

    [Header("Loading")]
    public GameObject loadinglevel;             //Loading panel with animation   

    [Header("Audio")]
    public AudioClip gameSound;
    public AudioClip loadingSound;
    AudioSource source;
    float vol = 1;

    string playerwon;


    private void Start()
    {
        source = GetComponent<AudioSource>();
        loadinglevel.SetActive(true);
        source.PlayOneShot(loadingSound, vol);
        Invoke("StartGame", 2);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause&&!isOver)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPause&&!isOver)
        {
            ContinueGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isOver)
        {
            LoadMenu();
        }
    }

    void PauseGame()
    {
        isPause = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }
    void ContinueGame()
    {
        isPause = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        //enable the scripts again
    }

    void StartGame()
    {
        loadinglevel.SetActive(false);
        roundPanel.SetActive(true);
        source.PlayOneShot(gameSound);
        Invoke("LoadManager", 1);
    }

    void LoadManager()
    {
        roundPanel.SetActive(false);
        pm.SetActive(true);
    }

    //Calling when round is finished
    public void ChangeRound() 
    {

        if (playersManager.playerWonRound == 0)
        {
            playerwon = "Ross";
        }
        else if (playersManager.playerWonRound == 1)
        {
            playerwon = "Mae";
        }

        //Showing which player won
        wonText.GetComponent<TextMeshProUGUI>().text= playerwon + " won";
        wonText.SetActive(true);

        //Calling for 
        Invoke("CallForPanel", 2f);
        Invoke("EnabledWonText", 1.5f);
    }

    void CallForPanel()
    {
        //Setting next round in round panel
        roundTextInPanel.text = "Round " + playersManager.rounds;
        roundPanel.SetActive(true);

        //Updating round on the screen
        roundText.text = "Round " + playersManager.rounds;

        Invoke("EnabledRoundPanel", 1f);
    }

    //Enabling game object
    void EnabledRoundPanel()
    {
        roundPanel.SetActive(false);
    }

    //Closing won text
    void EnabledWonText()
    {
        wonText.SetActive(false);
    }

    //Adding crown to winner
    public void AddCrown(int player)
    {
        if (player == 0)
        {
            Instantiate(crown, RossCrownsPanel.transform);
        }
        else
        {
            Instantiate(crown, MaeCrownsPanel.transform);
        }
    }

    public void GameOver(int _winner)
    {
        isOver = true;
        source.Stop();
        gameOverPanel.SetActive(true);
        if (_winner == 0)
        {
            winner.text = "Ross won";
        }
        else if(_winner == 1)
        {
            winner.text = "Mae won";
        }
        else
        {
            winner.text = "No ones";
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
