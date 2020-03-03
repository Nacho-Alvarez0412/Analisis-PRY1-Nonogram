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
        readTextFile(path);
    }
    
    public void readTextFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path);
        bool first = true;
        int[][] matrix;

        while(!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine( );

            if (first)
            {
                //matrix = new int[][];
                string[] matrix_lenght = inp_ln.Split(","[0]);
                Debug.Log(matrix_lenght[0]);
                Debug.Log(matrix_lenght[1]);
                first = !first;
            }
            Debug.Log(inp_ln);
            
        }

        inp_stm.Close( );  
    }

}
