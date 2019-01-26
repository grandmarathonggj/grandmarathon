using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        EventManager.StartListening(GameEvent.LEVEL_COMPLETED, new Action<EventParam>(delegate(EventParam param)
        {
            var intTextLerp = GetComponentInChildren<IntTextLerp>();
            intTextLerp.StartLerp(((LevelCompletedParams) param).score);
        }));
    }

    // Update is called once per frame
    void Update()
    {
    }
}