using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObjectUI : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform dragImage;
    public Type type;
    public int Amount;

    void Start()
    {
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
     
    public void OnDrag(PointerEventData eventData)
    {
        var a = GetComponent<RectTransform>();
        Canvas canvass = a.parent.GetComponent<Canvas>();
        a.anchoredPosition += eventData.delta/canvass.scaleFactor;
    }

    public Type getType()
    {
        return this.type;
    }

    public void setAmount(int a)
    {
        Amount = a;
    }

    public void setType(Type input)
    {
        type = input;
    }

    public int getAmount()
    {
        return Amount;
    }
}
