using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class NonogramGenerator : MonoBehaviour
{
    private RectTransform nonogramContainer;
    [SerializeField] private Sprite markedTile;
    [SerializeField] private Sprite unmarkedTile;
    [SerializeField] private TMP_ColorGradient gradient;

    private void Awake()
    {
        nonogramContainer = transform.Find("NonogramHolder").GetComponent<RectTransform>();
        int[] array = new int[6];
        int[] array1 = new int[2] {1, 2};
        
        int[][] xHints = new int[6][];
        int[][] yHints = new int[6][];
        
        int[][] matrix = new int[6][];
        matrix[0] = array;
        matrix[1] = array;
        matrix[2] = array;
        matrix[3] = array;
        matrix[4] = array;
        matrix[5] = array;
        
        xHints[0] = array1;
        xHints[1] = array1;
        xHints[2] = array1;
        xHints[3] = array1;
        xHints[4] = array1;
        xHints[5] = array1;
        
        yHints[0] = array1;
        yHints[1] = array1;
        yHints[2] = array1;
        yHints[3] = array1;
        yHints[4] = array1;
        yHints[5] = array1;
        showNonogram(matrix,xHints,yHints);
        
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
            size -= 50;
        }

        float startingPoint = (nonogramLenght / matrix[0].Length)-100;
        
        for (int i = 0; i < matrix.Length; i++)
        {
            float yPosition = nonogramHeight - 100 - i * size;
            if (i!=0)
            {
                yPosition -= space*i;
            }

            for (int j = 0; j < matrix[i].Length; j++)
            {
                float xPosition = (100 + j * size+space)+startingPoint;
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
                    createHint(new Vector2(xPosition, nonogramHeight-30), text, (size / 4)-yHints[i].Length);
                }
            }

            string text1 = "";
            for (int k = 0; k < xHints[i].Length; k++)
                {
                    text1 += yHints[i][k] + " ";
                }
                createHint(new Vector2(startingPoint+50, yPosition), text1, (size / 4)-xHints[i].Length);

        }
    }
    
    
}
