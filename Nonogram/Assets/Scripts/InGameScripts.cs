using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;
using  UnityEngine.UI;

public class InGameScripts : MonoBehaviour
{

    public void backToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void solve()
    {
        Debug.Log("Solving Nonogram");
    }
}
