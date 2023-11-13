using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine;

public class GameButton : Image, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    protected Action _onClick;

    private Vector3 _startSize;

    protected override void Start()
    {
        base.Start();

        _startSize = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        color = new Color(0.8f, 0.8f, 0.8f, 1);

        transform.localScale /= 1.1f;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        color = new Color(1f, 1f, 1f, 1f);

        transform.localScale = _startSize;
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        color = new Color(0.6f, 0.6f, 0.6f, 1);

        transform.localScale /= 1.2f;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        color = new Color(1f, 1f, 1f, 1f);

        transform.localScale = _startSize;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        transform.localScale = _startSize;

        _onClick.Invoke();
    }
}
