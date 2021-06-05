using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public float startScalingDelay;
    public float growSpeed;

    private Vector3 baseScale;
    private bool startScaling = false;

    private IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(startScalingDelay);
        startScaling = true;
    }

    void Start() {
        baseScale = gameObject.transform.localScale;
        gameObject.transform.localScale = Vector3.zero;
        StartCoroutine(StartCountdown());
    }

    void Update() {
        if (startScaling &&
            gameObject.transform.localScale.x < baseScale.x &&
            gameObject.transform.localScale.y < baseScale.y &&
            gameObject.transform.localScale.z < baseScale.z) {
                gameObject.transform.localScale += new Vector3(growSpeed, growSpeed, growSpeed);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("TitleScreen/quit_hover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("TitleScreen/quit");
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Application.Quit();
    }
}
