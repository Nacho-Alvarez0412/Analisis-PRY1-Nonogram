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
        Debug.Log("Im alive!!");
        nonogramContainer = transform.Find("NonogramHolder").GetComponent<RectTransform>();
        createUnmarkedTile(new Vector2(200,200));
    }

    private void createUnmarkedTile(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("UnmarkedTile",typeof(Image));
        gameObject.transform.SetParent(nonogramContainer,false);
        gameObject.GetComponent<Image>().sprite = unmarkedTile;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0,0);
    }
}
