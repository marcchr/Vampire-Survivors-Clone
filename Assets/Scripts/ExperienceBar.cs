using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField] Image _fillImage;
    [SerializeField] TextMeshProUGUI _levelText;

    private void OnEnable()
    {
        ExperienceManager.Instance.OnTotalExperiencePointsChanged += OnExpChanged;
        ExperienceManager.Instance.OnCurrentLevelChanged += OnLvlChanged;
    }

    private void OnDisable()
    {
        ExperienceManager.Instance.OnTotalExperiencePointsChanged -= OnExpChanged;
        ExperienceManager.Instance.OnCurrentLevelChanged -= OnLvlChanged;
    }

    private void OnExpChanged(int totalPoints, int basePoints, int nextPoints)
    {
        var fillAmount = (float)Mathf.InverseLerp(basePoints, nextPoints, totalPoints);
        _fillImage.fillAmount = fillAmount;
    }

    private void OnLvlChanged(int level)
    {
        _levelText.text = "LV "+(level+1).ToString();
    }
}
