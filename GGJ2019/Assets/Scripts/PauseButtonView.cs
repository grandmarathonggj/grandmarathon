using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PauseButtonView : MonoBehaviour
{
    private bool _paused = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClicked()
    {
        _paused = !_paused;

        transform.Find("Pause").gameObject.SetActive(!_paused);
        transform.Find("Resume").gameObject.SetActive(_paused);
        EventManager.TriggerEvent(GameEvent.PAUSE_BUTTON_CLICKED, null);
    }
}