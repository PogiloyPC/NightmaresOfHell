using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine;

public class GameButton : Image, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    protected Action _onClick;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        color = new Color(0.8f, 0.8f, 0.8f, 1);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        color = new Color(1f, 1f, 1f, 1f);
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        color = new Color(0.6f, 0.6f, 0.6f, 1);
    }
    
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        color = new Color(1f, 1f, 1f, 1f);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        _onClick.Invoke();
    }
}
