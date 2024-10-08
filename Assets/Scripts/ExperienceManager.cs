using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : Singleton<ExperienceManager>
{
    [SerializeField] private AnimationCurve _levelCurve;

    private int _totalExperiencePoints;
    private int _currentLevel;

    public int MaxLevel => (int)_levelCurve.keys[_levelCurve.keys.Length - 1].time;

    public int CurrentLevel
    {
        get => _currentLevel;
        set
        {
            _currentLevel = Mathf.Clamp(value, 0, MaxLevel);

            OnCurrentLevelChanged?.Invoke(_currentLevel);
        }
    }

    private int BasePoints => (int)_levelCurve.Evaluate(CurrentLevel);
    private int NextPoints => (int)_levelCurve.Evaluate(CurrentLevel + 1);

    public int TotalExperiencePoints
    {
        get => _totalExperiencePoints;
        set
        {
            _totalExperiencePoints = (int)Mathf.Clamp(value, 0, _levelCurve.Evaluate(MaxLevel));
            if (_totalExperiencePoints >= NextPoints)
            {
                CurrentLevel++;
            }

            OnTotalExperiencePointsChanged?.Invoke(_totalExperiencePoints, BasePoints, NextPoints);
        }
    }

    public event Action<int> OnCurrentLevelChanged;
    public event Action<int, int, int> OnTotalExperiencePointsChanged;
}
