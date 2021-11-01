using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObjectUI : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform dragImage;

    public void OnDrag(PointerEventData eventData)
    {
        var a = GetComponent<RectTransform>();
        Canvas canvass = a.parent.GetComponent<Canvas>();
    a.anchoredPosition += eventData.delta/canvass.scaleFactor;
    }
}
