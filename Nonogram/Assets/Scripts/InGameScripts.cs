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

    public Vector2? findEmpty(int[][] matrix)
    {
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                if (matrix[i][j]==0)
                {
                    return new Vector2(i,j);
                }
            }
        }

        return null;
    }

    public bool validAxisX(int[][] matrix, Vector2 pos, int[][] xHints, int[][] yHints)
    {
        int counter = 0;
        
        for (int i = 0; i < matrix[0].Length; i++)
        {
            if (matrix[(int) pos[0]][i] == 1)
            {
                counter++;
            }
        }

        if (counter >= yHints[(int) pos[0]][0])
        {
            return false;
        }
        else
        {
            return true;
        }

        return true;
    }
}
