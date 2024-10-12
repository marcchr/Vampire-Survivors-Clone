using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public float timeLimit;
    float stopwatchTime;
    public TextMeshProUGUI stopwatchDisplay;


    [SerializeField] UnityEvent<int> _onLevelChanged;

    void Update()
    {
        UpdateStopwatch();
    }

    private void OnEnable()
    {
        ExperienceManager.Instance.OnCurrentLevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        ExperienceManager.Instance.OnCurrentLevelChanged -= OnLevelChanged;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    private void OnLevelChanged(int level)
    {
        _onLevelChanged?.Invoke(level);
        PauseGame();
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
}
