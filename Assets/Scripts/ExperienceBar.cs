using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField] Image _fillImage;

    private void OnEnable()
    {
        ExperienceManager.Instance.OnTotalExperiencePointsChanged += OnExpChanged;
    }

    private void OnDisable()
    {
        ExperienceManager.Instance.OnTotalExperiencePointsChanged -= OnExpChanged;
    }

    private void OnExpChanged(int totalPoints, int basePoints, int nextPoints)
    {
        var fillAmount = (float)Mathf.InverseLerp(basePoints, nextPoints, totalPoints);
        _fillImage.fillAmount = fillAmount;
    }
}
