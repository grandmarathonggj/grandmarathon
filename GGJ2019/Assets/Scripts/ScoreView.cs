using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    private bool success;

    // Use this for initialization
    void Start()
    {
        EventManager.StartListening(GameEvent.LEVEL_COMPLETED, new Action<EventParam>(delegate(EventParam param)
        {
//            transform.GetChild(0).gameObject.SetActive(true);
            
            GetComponent<Animator>().SetTrigger("Open");
            var levelCompletedParams = (LevelCompletedParams) param;
            var starView = GetComponentInChildren<StarView>();
            starView.SetStars(levelCompletedParams.star);
            var intTextLerp = GetComponentInChildren<IntTextLerp>();
            intTextLerp.StartLerp(levelCompletedParams.score);
            success = levelCompletedParams.success;

            if (success)
            {
                GetComponentInChildren<Button>().transform.GetComponentInChildren<Text>().text = "Next";
            }
            else
            {
                GetComponentInChildren<Button>().transform.GetComponentInChildren<Text>().text = "Retry";
            }
        }));
    }

    public void NextLevelOrRetry()
    {
        if (success)
        {
            EventManager.TriggerEvent(GameEvent.NEXT_LEVEL, null);
        }
        else
        {
            EventManager.TriggerEvent(GameEvent.RETRY_LEVEL, null);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}