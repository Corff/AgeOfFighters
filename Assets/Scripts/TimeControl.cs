using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    public float time;
    public bool timeUp;
    public bool countDown;
    public string name;
    public float countdownMultiplier = 1;

    private void Update()
    {
        if (time > 0 && countDown)
        {
            time -= Time.deltaTime * countdownMultiplier;
        }
        if (time < 0)
        {
            time = 0;
        }
        if (time <= 0)
        {
            timeUp = true;
        }
        else if (time > 0)
        {
            timeUp = false;
        }
    }
}
