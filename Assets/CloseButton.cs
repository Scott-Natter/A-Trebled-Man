using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject controlsScreen;

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("TitleScreen/close_hover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("TitleScreen/close");
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("TitleScreen/close");
        controlsScreen.SetActive(false);
        gameObject.SetActive(false);
    }
}
