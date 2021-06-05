using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour
{
    public GameObject overlayFull;
    public GameObject overlayHalf;
    public float speed;

    private RectTransform overlayFullRectTransform;
    private RectTransform overlayHalfRectTransform;

    private IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("TitleScreen");
    }

    void Start() {
        overlayFullRectTransform = overlayFull.GetComponent<RectTransform>();
        overlayHalfRectTransform = overlayHalf.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (overlayFullRectTransform.localPosition.x >= 200) {
            overlayFull.SetActive(false);
            overlayHalf.SetActive(false);
            // overlayHalf.transform.Translate(-speed, 0, 0);
            StartCoroutine(StartCountdown());
        } else {
            overlayFull.transform.Translate(speed, 0, 0);
            overlayHalf.transform.Translate(speed, 0, 0);
        }

        // if (overlayHalf.transform.position.x <= -160) {
        //     SceneManager.LoadScene("TitleScreen");
        // }
    }
}
