using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowGraph : MonoBehaviour {

    [SerializeField] Sprite circleSprite;
    RectTransform graphContainer;

    void Start() 
    {
        StartCoroutine(WaitForListInitialisation());
    }

    GameObject CreateCircle(Vector2 anchoredPosition) 
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(0.29f, 0.29f, 0.31f, 1f);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(12, 12); // circle size
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    void ShowGraph(List<int> valueList) 
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = valueList.Max();//100f;
        float xSize = 50f;

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++) 
        {
            float xPosition = i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null) {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;
        }
    }

    void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB) 
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(0.29f, 0.29f, 0.31f, 1f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 12f); // line size
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));
    }

    static float GetAngleFromVectorFloat(Vector3 dir) 
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    IEnumerator WaitForListInitialisation()
    {
        yield return new WaitForSeconds(0.4f);
        
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        //List<int> valueList = new List<int>() { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33 };
        List<int> valueList = GameObject.FindObjectOfType<ScoreHandler>().scoresByMonths.Values.ToList();
        ShowGraph(valueList);
        var previousParentIndex = transform.parent.GetSiblingIndex();
        gameObject.transform.SetParent(transform.parent.parent);
        transform.SetSiblingIndex(previousParentIndex-1);

        yield break;
    }
}
