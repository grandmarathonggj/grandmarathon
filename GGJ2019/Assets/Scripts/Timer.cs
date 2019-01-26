using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class Timer : MonoBehaviour
{
    public int currentTick = 0;
    public bool continueTimer = true;
    public int timeScale = 60;
    public int tickLimit = 24;

    // Use this for initialization
    void Start()
    {
        EventManager.StartListening(GameEvent.START_LEVEL_TIMER, new Action<EventParam>(StartTimer));
    }

    // Update is called once per frame
    void Update()
    {
    }

    void StartTimer(EventParam eventParam)

    {
        currentTick = 0;
        StartCoroutine(_coTimer());
    }

    private IEnumerator _coTimer()
    {
        while (continueTimer)
        {
            yield return new WaitForSeconds(1);
            currentTick++;

            EventManager.TriggerEvent(GameEvent.LEVEL_TIMER_TICK, new TimerEventParams(currentTick * timeScale));

            if (currentTick > tickLimit)
            {
                continueTimer = false;
                EventManager.TriggerEvent(GameEvent.LEVEL_TIMER_END, new TimerEventParams(currentTick * timeScale));

                
            }
        }
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