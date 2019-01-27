using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class Timer : MonoBehaviour
{
	public bool Loop;
    public float currentTick;
	public float startTimeOffset;
    public int timeScale;
    public int tickLimit;
	public int roundToNearest;
	private bool started = false;
    private bool continueTimer = false;

    // Use this for initialization
    void Start()
    {
		EventManager.StartListening(GameEvent.START_LEVEL_TIMER, new Action<EventParam>(StartTimer));
    }

    // Update is called once per frame
    void Update()
    {
		if (started && (Loop || continueTimer)) {
			currentTick += Time.deltaTime * timeScale;

			EventManager.TriggerEvent(GameEvent.LEVEL_TIMER_TICK, new TimerEventParams(Mathf.FloorToInt(currentTick  / roundToNearest) * roundToNearest));

			if (currentTick > tickLimit + startTimeOffset) {
				currentTick -= tickLimit;
				continueTimer = false;
				if (!Loop) EventManager.TriggerEvent(GameEvent.LEVEL_TIMER_END, new TimerEventParams(Mathf.FloorToInt(currentTick  / roundToNearest) * roundToNearest));
			}
		}
	}

    void StartTimer(EventParam eventParam)

    {
		started = true;
        currentTick = startTimeOffset;
		continueTimer = true;

	}
}


class TimerEventParams : EventParam
{
    public int currentTime = 0;

    public TimerEventParams(int currentTime)
    {
        this.currentTime = currentTime;
    }
}