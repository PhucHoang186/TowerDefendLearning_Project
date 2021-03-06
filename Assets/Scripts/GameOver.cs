using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
public class GameOver : MonoBehaviour
{
    public Text RoundSurvived;
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";

    private void OnEnable()
    {
        RoundSurvived.text = PlayerStats.Rounds.ToString();
    }
    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
