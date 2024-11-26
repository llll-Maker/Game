using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveBag : MonoBehaviour,IDragHandler
{
    public Canvas canvas;
    RectTransform currentRect;
    public void OnDrag(PointerEventData eventData)
    {
        currentRect.anchoredPosition += eventData.delta;//位置加上鼠标的移动
    }

    private void Awake()
    {
        currentRect = GetComponent<RectTransform>();
    }
}
