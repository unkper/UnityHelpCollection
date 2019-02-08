using System;
using System.Collections.Generic;
using UnityEngine;
using Timers;

public class TestScript:MonoBehaviour
{
    NormalTime timer = new NormalTime(3.0f);
    void Start()
    {
        timer.Timing();
    }

    void Update()
    {
        Debug.Log(timer.GetLeft+"         "+timer.finish);
        timer.Update();
        if (timer.finish)
            timer.Timing();
    }


}
