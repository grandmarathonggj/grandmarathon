using System;
using System.Collections;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class IntTextLerp : MonoBehaviour
{
    public int current = 0;
    public int target = 1000000;
    public int start = 0;
    public float duration = 3;
    public float lerp = 0;
    public float currentTime = 0;
    public TextMeshProUGUI scoreText;
    private bool shouldAnimate = false;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();


        EventManager.StartListening(GameEvent.ANIMATE_SCORE,
            new Action<EventParam>(delegate(EventParam param) { StartLerp(((ScoreParam) param).score); }));

//            StartCoroutine(coLerpText());
    }


//        private IEnumerator coLerpText()
//        {
//            
//        }


    public void StartLerp(int target, int start = 0)
    {
        this.target = target;
        this.start = start;
        current = 0;
        lerp = 0;
        currentTime = 0;
        shouldAnimate = true;
    }

    private void Update()
    {
        if (shouldAnimate)
        {
            if (lerp <= 1)
            {
                currentTime += Time.deltaTime;

                lerp = currentTime / duration;
                current = (int) Mathf.Lerp(start, target, lerp);
                scoreText.text = current.ToString();
            }
        }
    }
}


class ScoreParam : EventParam
{
    public int score;

    public ScoreParam(int score)
    {
        this.score = score;
    }
}