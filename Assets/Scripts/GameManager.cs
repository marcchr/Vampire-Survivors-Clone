using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] UnityEvent<int> _onLevelChanged;

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
}
