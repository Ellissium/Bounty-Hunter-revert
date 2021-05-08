using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuSelect : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.TryGetComponent<Button>(out _) == true && gameObject.GetComponent<Button>().interactable == true)
            AudioManager.instance.Play("MenuSelect");
        if (gameObject.TryGetComponent<Slider>(out _) == true && gameObject.GetComponent<Slider>().interactable == true)
            AudioManager.instance.Play("MenuSelect");
        if (gameObject.TryGetComponent<Dropdown>(out _) == true && gameObject.GetComponent<Dropdown>().interactable == true)
            AudioManager.instance.Play("MenuSelect");
    }
}
