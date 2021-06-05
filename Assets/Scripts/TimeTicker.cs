using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeTicker : MonoBehaviour
{
    public class OnTickEventArgs : EventArgs
    {
        public int tick;
    }

    public GameObject beatDebug;
    public GameObject beatDebugOff;
    public GameObject debugEnabler;
    public bool debugEnabled = false;
    public static event EventHandler<OnTickEventArgs> OnTick;
    private int tick;
    private float timer;

    public float[] beats;
    public float newStart = 0.167f;
    private Queue<float> beatList;

    public bool beatOccurring = false;

    private IEnumerator StartCountdown(float countdownValue = 1)
    {
        float currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            //Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(0.85f);
            currCountdownValue--;
        }
        // beatOccurring = false;
    }

    private IEnumerator RhythmSwitcher()
    {
        float rhythm = 0.167f;
        while (true)
        {
            beatOccurring = false;
            yield return new WaitForSeconds(0.04f);
            beatOccurring = true;
            yield return new WaitForSeconds(0.127f);

        }
    }

    void Awake()
    {
        tick = 0;
        beatList = new Queue<float>(beats);
        StartCoroutine(RhythmSwitcher());
    }
    public void ToggleDebug()
    {
        debugEnabled = !debugEnabled;
    }
    void Update()
    {
        Debug.LogFormat("TimeTicker: {0}", beatOccurring);
        timer += Time.deltaTime;
        if (timer >= beatList.Peek())
        {
            timer -= beatList.Peek();
            tick++;
            beatList.Dequeue();
            if (beatList.Count == 0)
            {
                beats[0] = newStart;
                beatList = new Queue<float>(beats);
                
            }

            if (OnTick != null)
            {
                // beatOccurring = true;
                StartCoroutine(StartCountdown());
                OnTick(this, new OnTickEventArgs { tick = tick });
                //yield return new WaitForSeconds(0.2f);

            }
        }
        if (beatDebug != null && beatDebugOff != null) {
            if (beatOccurring && debugEnabled)
            {
                beatDebugOff.SetActive(false);
                beatDebug.SetActive(true);
            }
            else
            {
                if (debugEnabled)
                {
                    beatDebug.SetActive(false);
                    beatDebugOff.SetActive(true);
                }       
            }
            if(!debugEnabled)
            {
                beatDebugOff.SetActive(false);
                beatDebug.SetActive(false);
            }
        }
    }

    private void OnDestroy()
    {
        OnTick = null;
    }
}
