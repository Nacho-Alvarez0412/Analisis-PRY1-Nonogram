using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NonogramGenerator : MonoBehaviour
{
    private RectTransform nonogramContainer;
    [SerializeField] private Sprite markedTile;
    [SerializeField] private Sprite unmarkedTile;

    private void Awake()
    {
        nonogramContainer = transform.Find("NonogramHolder").GetComponent<RectTransform>();
        int[] array = new int[3];
        int[][] matrix = new int[3][];
        matrix[0] = array;
        matrix[1] = array;
        matrix[2] = array;
        showNonogram(matrix);
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

    private void showNonogram(int[][] matrix)
    {
        float nonogramHeight = nonogramContainer.sizeDelta.y;
        float nonogramLenght = nonogramContainer.sizeDelta.x;
        float sizeX = (nonogramLenght / matrix[0].Length) -10;
        float sizeY = (nonogramHeight / matrix.Length) - 10;
        float xSize = sizeX / 32;
        
        for (int i = 0; i < matrix.Length; i++)
        {
            float yPosition = nonogramHeight - 100 - i * sizeX;
            if (i!=0)
            {
                yPosition -= xSize*i;
            }

            for (int j = 0; j < matrix[i].Length; j++)
            {
                float xPosition = 100 + j * sizeX+xSize;
                if (j!=0)
                {
                    xPosition += xSize*j;
                }

                while (matrix.Length*(sizeX + xSize) >= nonogramHeight)
                {
                    sizeX -= 50;
                }
                createUnmarkedTile(new Vector2(xPosition,yPosition),sizeX);
            }
        }
    }
}
