using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;
using  UnityEngine.SceneManagement;
using  UnityEngine.UI;

public class InGameScripts : MonoBehaviour
{
    [SerializeField] private Sprite markedTile;
    [SerializeField] private Sprite unmarkedTile;
    int[][] nonogram = MainMenuScripts.matrix;
    private float elapsedMiliseconds;
    private Thread solverThread;
    private bool hasSolution = true;
    
    public void backToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void solve()
    {
        solverThread = new Thread(new ThreadStart(startSolving));
        solverThread.Start();
    }

    public void startSolving()
    {
        var watch = new System.Diagnostics.Stopwatch();
        watch.Start();
        
        bool solved = solveAux(nonogram);
        
        watch.Stop();

        elapsedMiliseconds = watch.ElapsedMilliseconds;

        if (!solved)
        {
            hasSolution = false;
        }
    }

    public bool solveAux(int[][] nonogram)
    {
        Vector2? position = findEmpty(nonogram);

        if (position == null)
        {
            return true;
        }

        for (int i = 2; i>0; i--)
        {
            nonogram[(int) position.Value.x][(int) position.Value.y] = i;
            
            if (isValidNonogram(nonogram, position.Value) && solveAux(nonogram))
            {
                return true;
            }
            nonogram[(int) position.Value.x][(int) position.Value.y] = 0;
        }

        return false;
    }

    public void closePopUp()
    {
        RectTransform popUp = transform.Find("MessageDialog").GetComponent<RectTransform>();
        popUp.anchoredPosition = new Vector2(5000,0);
        hasSolution = true;
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

    public bool isValidNonogram(int[][] matrix, Vector2 pos)
    {
        int[] colHints = MainMenuScripts.yHints[(int) pos.x];
        int[] col = matrix[(int) pos.x];
        
        if (isValidList(colHints, col))
        {
            int[] rowHints = MainMenuScripts.xHints[(int) pos.y];
            int[] row = getMatrixRow(matrix, (int) pos.y);
            
            if (isValidList(rowHints, row))
            {
                return true;
            }
        }

        return false;
    }

    private int[] getMatrixRow(int[][] matrix, int y)
    {
        int[] row = new int[matrix.Length];

        for (int x = 0; x < matrix.Length; x++)
        {
            row[x] = matrix[x][y];
        }

        return row;
    }

    private bool isValidList(int[] hints, int[] list)
    {
        int counter = 0;
        int blankSpaceCounter = 0;
        List<int> resultHints = new List<int>();

        int hintsSum = 0;
        for (int i = 0; i < hints.Length; i++)
        {
            hintsSum += hints[i];
        }
        int maxBlankSpaces = list.Length - hintsSum;
        
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i] == 1)
            {
                counter++;
                if (i + 1 == list.Length || list[i + 1] == 2) {
                    resultHints.Add(counter);
                    counter = 0;
                }
            }
            else if (list[i] == 2)
            {
                blankSpaceCounter++;
            }
            else if (list[i] == 0) break;
        }

        if (hints.Length < resultHints.Count)
        {
            return false;
        }

        if (blankSpaceCounter > maxBlankSpaces)
        {
            return false;
        }
        
        for (int i = 0; i < resultHints.Count; i++)
        {
            if (hints[i] != resultHints[i])
            {
                return false;
            }
        }

        return true;
    }

    private void refreshMatrix()
    {
        for (int i = 0; i < nonogram.Length; i++)
        {
            for (int j = 0; j < nonogram[i].Length; j++)
            {
                try
                {
                    if (nonogram[j][i] == 1)
                    {
                        NonogramGenerator.tiles[j][i].GetComponent<Image>().sprite = markedTile;
                    }
                    else
                    {
                        NonogramGenerator.tiles[j][i].GetComponent<Image>().sprite = unmarkedTile;
                    }
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e);
                    //throw;
                }
                
            }
        } 
    }

    private void updateTimer()
    {
        Transform timerText = transform.Find("TimeText (TMP)");
        timerText.GetComponent<TextMeshProUGUI>().text = elapsedMiliseconds.ToString();
    }

    private void OnGUI()
    {
        refreshMatrix();
        updateTimer();

        if (!hasSolution)
        {
            RectTransform popUp = transform.Find("MessageDialog").GetComponent<RectTransform>();
            popUp.anchoredPosition = new Vector2(0,0);
        }
    }
}
