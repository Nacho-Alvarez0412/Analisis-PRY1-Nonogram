using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;

public class InGameScripts : MonoBehaviour
{
    public void backToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
