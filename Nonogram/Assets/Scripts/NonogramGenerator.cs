using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;
using static MainMenuScripts;

public class NonogramGenerator : MonoBehaviour
{
    private RectTransform nonogramContainer;
    [SerializeField] private Sprite markedTile;
    [SerializeField] private Sprite unmarkedTile;
    [SerializeField] private TMP_ColorGradient gradient;
    private int[][] nonogram;
    private int[][] xHints;
    private int[][] yHints;
    private void Awake()
    {
        nonogram = MainMenuScripts.matrix;
        xHints = MainMenuScripts.xHints;
        yHints = MainMenuScripts.yHints;
        nonogramContainer = transform.Find("NonogramHolder").GetComponent<RectTransform>();
        Debug.Log(nonogram.Length);
        Debug.Log(xHints.Length);
        Debug.Log(yHints.Length);
        showNonogram(nonogram,xHints,yHints);
        
    }

    private void createUnmarkedTile(Vector2 anchoredPosition,float size)
    {
        GameObject gameObject = new GameObject("UnmarkedTile",typeof(Image));
        gameObject.transform.SetParent(nonogramContainer,false);
        gameObject.GetComponent<Image>().sprite = unmarkedTile;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;

        rectTransform.sizeDelta = new Vector2(size, size);
        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0,0);
    }

    private void createHint(Vector2 anchoredPosition,String text,float size)
    {
        GameObject gameObject = new GameObject("HintTextMesh",typeof(TextMeshProUGUI));
        gameObject.transform.SetParent(nonogramContainer,false);
        gameObject.GetComponent<TextMeshProUGUI>().text = text;
        gameObject.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
        gameObject.GetComponent<TextMeshProUGUI>().enableVertexGradient = true;
        gameObject.GetComponent<TextMeshProUGUI>().colorGradientPreset = gradient;
        gameObject.GetComponent<TextMeshProUGUI>().fontSize = size;
        
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;

        rectTransform.sizeDelta = new Vector2(size, size);
        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0,0);
        

    }

    private void showNonogram(int[][] matrix,int[][] xHints,int[][] yHints)
    {
        float nonogramHeight = nonogramContainer.sizeDelta.y;
        float nonogramLenght = nonogramContainer.sizeDelta.x;
        float size = (nonogramLenght / matrix[0].Length) -10;
        float space = size / 32;
        
        while (matrix.Length*(size + space) >= nonogramHeight)
        {
            size -= 10;
        }
        
        float startingXPoint = (nonogramLenght / matrix[0].Length)+100;
        
        for (int i = 0; i < matrix.Length; i++)
        {
            
            float yPosition = nonogramHeight - 120 - i * size;
            if (i!=0)
            {
                yPosition -= space*i;
            }

            for (int j = 0; j < matrix[i].Length; j++)
            {
                float xPosition = (100 + j * size+space)+startingXPoint;
                if (j!=0)
                {
                    xPosition += space*j;
                }
                createUnmarkedTile(new Vector2(xPosition,yPosition),size);
                if (i == 0)
                {
                    String text = "";
                    for (int k = 0; k < yHints[j].Length; k++)
                    {
                        text += yHints[j][k] + "\n";
                    }
                    createHint(new Vector2(xPosition, nonogramHeight-30), text, (size / 4));
                }
            }

            string text1 = "";
            for (int k = 0; k < xHints[i].Length; k++)
            {
                text1 += xHints[i][k] + " ";
            }
            Debug.Log(startingXPoint);
            createHint(new Vector2(startingXPoint+matrix[0].Length, yPosition), text1, (size / 4) );

        }
    }
    
    
}
