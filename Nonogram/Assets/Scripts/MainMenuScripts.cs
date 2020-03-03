using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
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
        string path = EditorUtility.OpenFilePanel("Choose txt file", "","txt");
        int[][] matrix = readTextFile(path);
    }
    
    public int[][] readTextFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path);
        bool first = true;
        int[][] matrix= new int[1][];
        int[][] hintsFilas = new int[1][];
        int[][] hintsColumnas = new int[1][];
        int x=0;
        int y=0;
        int counterX=0;
        int counterY=0;

        while(!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine( );

            if (first)
            {
                string[] matrix_lenght = inp_ln.Split(","[0]);
                
                Debug.Log(matrix_lenght[0]);
                Debug.Log(matrix_lenght[1]);
                
                x = int.Parse(matrix_lenght[0]);
                y = int.Parse(matrix_lenght[1]);
                matrix = new int[x][];
                
                hintsFilas = new int[x][];
                hintsColumnas = new int[y][];
                
                for (int i = 0; i < matrix.Length; i++)
                {
                    matrix[i] = new int[y];
                }

                first = !first;
            }
            else if (!inp_ln.Equals("FILAS")&& !inp_ln.Equals("COLUMNAS"))
            {
                Debug.Log(inp_ln);
                string[] hint = inp_ln.Split(","[0]);
                
                if (counterX < x)
                {
                    
                    int[] localHints = new int[hint.Length];
                    
                    for (int i = 0; i < hint.Length; i++)
                    {
                        localHints[i] = int.Parse(hint[i]);
                    }

                    hintsFilas[counterX] = localHints;

                    counterX++;
                }
                else if (counterY < y)
                {
                    
                    
                    int[] localHints = new int[hint.Length];
                    
                    for (int i = 0; i < hint.Length; i++)
                    {
                        localHints[i] = int.Parse(hint[i]);
                    }

                    hintsColumnas[counterY] = localHints;

                    counterY++;
                }
            }
            
            Debug.Log(inp_ln);
        }

        inp_stm.Close( );
        
        Debug.Log("VECTORES DE HINTS");
        Debug.Log("FILAS");
        foreach (var num in hintsFilas)
        {
            Debug.Log(num[0]);
        }
        Debug.Log("COLUMNAS");
        foreach (var num in hintsColumnas)
        {
            Debug.Log(num[0]);
        }
        return matrix;
    }

}
