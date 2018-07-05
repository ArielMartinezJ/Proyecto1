using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Public Variables
    [Header("\t--Public Variables--")]

    [Header("Time Variables")]
    public Text timeText;

    [Header("Menu Variables")]
    public GameObject menuPanel;

    [Header("Victory Panel Variables")]
    public GameObject victoryPanel;

    [Header("Defeat Panel Variables")]
    public GameObject defeatPanel;
    #endregion

    //Time Variables
    private int hours;
    private int minutes;
    private int seconds;

    //Game Variables
    private static GameManager _instance;
    private int gameTime;
    private bool gameFinished = false;
    private bool isPlaying = false;
    private bool playerIsDead = false;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {

    }

    void Update()
    {
        if (!gameFinished)
        {
            isPlaying = true;
        }
        else
        {
            isPlaying = false;
            if (!playerIsDead)
            {
                ShowVictoryScreen();
            }
            else
            {
                ShowDefeatScreen();
            }
        }

        if (isPlaying)
        {
            DisplayTime();
            LockCursor();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }

            if (Input.GetKeyDown(KeyCode.CapsLock))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; //si le damos al botón de Quit en Unity, parará de jugar
#endif
            }
        }
    }

    void DisplayTime()
    {
        gameTime = (int)Time.deltaTime;
        hours = ((int)Time.timeSinceLevelLoad - gameTime) / 3600;
        minutes = Mathf.Abs(((int)Time.timeSinceLevelLoad - gameTime) / 60);
        seconds = ((int)Time.timeSinceLevelLoad - gameTime) % 60;

        timeText.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    void LockCursor()
    {
        if (isPlaying)
        {
            Cursor.lockState = CursorLockMode.Locked;

            if (Input.GetAxis("Cancel") > 0)
                Cursor.lockState = CursorLockMode.None;

            if (Input.GetMouseButtonDown(0))
                Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    
    public void GameOver()
    {
        playerIsDead = true;
        gameFinished = true;
    }

    #region Pause Methods
    public void PauseGame()
    {
        menuPanel.SetActive(true);
        Time.timeScale = 0;
    }
    #endregion

    #region Game States
    public void ShowVictoryScreen()
    {
        victoryPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ShowDefeatScreen()
    {
        defeatPanel.SetActive(true);
        Time.timeScale = 0;
    }
    #endregion
}
