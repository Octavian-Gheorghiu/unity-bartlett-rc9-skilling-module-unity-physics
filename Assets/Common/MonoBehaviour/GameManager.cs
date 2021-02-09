using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score;
    public TextMeshProUGUI pelletsUI, scoreTextUI;
    public GameObject titleUI, gameUI, winUI, loseUI;

    public void Awake()
    {
        instance = this;
    }

    public void Reset()
    {
        SwitchUI(titleUI);
        score = 0;
    }

    public void InGame()
    {
        SwitchUI(gameUI);
    }

    public void Win()
    {
        SwitchUI(winUI);
    }

    public void Lose()
    {
        SwitchUI(loseUI);
    }

    public void SwitchUI(GameObject newUI)
    {
        titleUI.SetActive(false);
        gameUI.SetActive(false);
        winUI.SetActive(false);
        loseUI.SetActive(false);

        newUI.SetActive(true);
    }

    public void AddPoints(int points)
    {
        score += points;
        scoreTextUI.text = "Score: " + score;
    }
}
