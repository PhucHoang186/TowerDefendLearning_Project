using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string LevelToLoad ="MainLevel";
    public SceneFader sceneFader;
    public void Play()
    {
        sceneFader.FadeTo(LevelToLoad);
    }
    public void Quit()
    {
        Debug.Log("quit Game!");
        Application.Quit();
    }
}
