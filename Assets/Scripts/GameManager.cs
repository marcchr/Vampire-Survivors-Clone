using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        Gameplay,
        Paused,
        LevelUp,
        GameOver
    }

    public GameState currentState;
    public GameState previousState;

    [Header("UI")]
    public GameObject pauseScreen;
    // public GameObject levelUpScreen;
    public GameObject resultsScreen;


    public float timeLimit;
    float stopwatchTime;
    public TextMeshProUGUI stopwatchDisplay;

    public int killCount;
    public TextMeshProUGUI killCountText;

    public TextMeshProUGUI survivalTimeText;
    public TextMeshProUGUI enemiesKilledText;

    public bool choosingUpgrade;
    public bool isGameOver = false;

    [SerializeField] UnityEvent<int> _onLevelChanged;

    //protected override void Awake()
    //{
    //   DisableScreens();

    //}

    void Update()
    {
        UpdateStopwatch();
        UpdateKillCountText();

        switch(currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                break;
            case GameState.Paused:
                CheckForPauseAndResume();
                break;
            case GameState.GameOver:
                if (!isGameOver)
                {
                    isGameOver = true;
                    Time.timeScale = 0f;
                    Debug.Log("GAME OVER");
                    DisplayResults();
                }
                break;
            case GameState.LevelUp:
                if (!choosingUpgrade)
                {
                    choosingUpgrade = true;
                    Time.timeScale = 0f;
                    // levelUpScreen.SetActive(true);
                }
                CheckForPauseAndResume();
                break;

            default:
                Debug.LogWarning("STATE DOES NOT EXIST");
                break;
        }

    }

    private void OnEnable()
    {
        ExperienceManager.Instance.OnCurrentLevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        ExperienceManager.Instance.OnCurrentLevelChanged -= OnLevelChanged;
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    public void PauseGame()
    {
        if(currentState != GameState.Paused)
        {
            previousState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        if(currentState == GameState.Paused && previousState == GameState.LevelUp)
        {
            ChangeState(previousState);
            pauseScreen.SetActive(false);
        }

        else if(currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
        }
    }

    void CheckForPauseAndResume()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // disable screens unused
    void DisableScreens()
    {
        pauseScreen.SetActive(false);
        resultsScreen.SetActive(false);
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }

    void DisplayResults()
    {
        resultsScreen.SetActive(true);
    }

    private void OnLevelChanged(int level)
    {
        _onLevelChanged?.Invoke(level);
        StartLevelUp();

    }

    public void StartLevelUp()
    {
        ChangeState(GameState.LevelUp);
    }

    public void EndLevelUp()
    {
        choosingUpgrade = false;
        Time.timeScale = 1f;
        ChangeState(GameState.Gameplay);
    }

    void UpdateStopwatch()
    {
        stopwatchTime += Time.deltaTime;

        UpdateStopwatchDisplay();


    }

    void UpdateStopwatchDisplay()
    {
        int minutes = Mathf.FloorToInt(stopwatchTime / 60);
        int seconds = Mathf.FloorToInt(stopwatchTime % 60);

        stopwatchDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void UpdateKillCountText()
    {
        killCountText.text = "Kills: "+ killCount.ToString();
    }

    public void AssignSurvivalTime()
    {
        survivalTimeText.text = stopwatchDisplay.text;
    }

    public void AssignEnemiesKilled()
    {
        enemiesKilledText.text = killCount.ToString();
    }
}
