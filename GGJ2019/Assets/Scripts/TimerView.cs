﻿using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    public Text timerText;

	// Use this for initialization
	void Start()
    {
        timerText = GetComponent<Text>();
		TimeSpan offset = TimeSpan.FromSeconds(32400);
		timerText.text = string.Format("{0:hh:mm:ss tt}", new DateTime(2019, 01, 01) + offset);

		EventManager.StartListening(GameEvent.LEVEL_TIMER_TICK,
            new Action<EventParam>(delegate(EventParam param)
            {
                var timeInSecond = ((TimerEventParams) param).currentTime;
                TimeSpan t = TimeSpan.FromSeconds(timeInSecond);
				DateTime dt = new DateTime(2019, 01, 01);

                timerText.text = string.Format("{0:hh:mm:ss tt}", dt + t);
            }));
        EventManager.StartListening(GameEvent.LEVEL_TIMER_END,
            new Action<EventParam>(delegate(EventParam param) { timerText.text = "Time's up!"; }));


    }

    // Update is called once per frame
    void Update()
    {
    }
}