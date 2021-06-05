using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{

    public GameObject logo;
    private Vector3 logoBaseScale;
    public float logoGrowSpeed;
    private bool startScaling = false;
    public float startScalingDelay;

    private IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(startScalingDelay);
        startScaling = true;
    }

    void Awake() {
        logoBaseScale = logo.transform.localScale;
        logo.transform.localScale = new Vector3(0, 0, 0);
    }

    void Start() {
        StartCoroutine(StartCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        if (startScaling &&
            logo.transform.localScale.x <= logoBaseScale.x &&
            logo.transform.localScale.y <= logoBaseScale.y &&
            logo.transform.localScale.z <= logoBaseScale.z) {
            logo.transform.localScale += new Vector3(logoGrowSpeed, logoGrowSpeed, logoGrowSpeed);
        }
    }
}
