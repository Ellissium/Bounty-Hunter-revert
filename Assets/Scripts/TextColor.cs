using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class TextColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Color mainTextColor;
    [SerializeField] private Color highlightedTextColor;
    [SerializeField] private Color pressedTextColor;

    private bool activeText = true;

    void Start()
    {
        mainTextColor.a = 1f;
        highlightedTextColor.a = 1f;
        pressedTextColor.a = 1f;

        gameObject.GetComponentInChildren<Text>().color = mainTextColor;
    }

    public void SetActiveText()
    {
        activeText = true;
    }

    public void SetNotActiveText()
    {
        activeText = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(activeText)
        gameObject.GetComponentInChildren<Text>().color = highlightedTextColor;
    }

    public void OnPointerDown(PointerEventData eventData) 
    {
        if (activeText)
        gameObject.GetComponentInChildren<Text>().color = pressedTextColor;
    }

    public void OnPointerUp(PointerEventData eventData) 
    {
        if (activeText)
        gameObject.GetComponentInChildren<Text>().color = mainTextColor;
        activeText = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (activeText)
        gameObject.GetComponentInChildren<Text>().color = mainTextColor;
    }
}
