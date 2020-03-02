using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScripts : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void exit()
    {
        Debug.Log("Quitted Program!");
        Application.Quit();
    }
    
    public void loadNonogram()
    {
        Debug.Log("Loading Nonogram!!!");
    }
}
