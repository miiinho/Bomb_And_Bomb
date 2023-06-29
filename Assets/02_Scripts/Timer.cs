using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int time = 180;
    public Text timer;

    private void Start()
    {
        StartCoroutine(TimerCoroution());
    }

    IEnumerator TimerCoroution()
    {
        time -= 1;

        timer.text = (time / 60).ToString("D2") + ":" + (time % 60).ToString("D2");

        yield return new WaitForSeconds(1f);

        StartCoroutine(TimerCoroution());
    }
}
