using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    public GameObject creditsDisplay;

    private void Awake()
    {
        creditsDisplay.SetActive(false);
    }
    public void DisplayCredits()
    {
        creditsDisplay.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsDisplay.SetActive(false);
    }

}
