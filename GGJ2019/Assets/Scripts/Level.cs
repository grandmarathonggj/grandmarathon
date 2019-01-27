using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    //Prefabs
    public GameObject PlayerPrefab;
    public GameObject StartPrefab;
    public GameObject EndPrefab;
    public GameObject HudPrefab;

    //References
    private GameObject _startPosition;
    private GameObject _endPosition;
    private GameObject _startGO;
    private GameObject _playerGO;
    private GameObject _endGO;
    private GameObject _hud;
    private GameObject _canvas;
    private MeshRenderer _dayMat;
    private MeshRenderer _nightMat;

    private Timer _timer;
    private int _starCount;

    public Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;

        _timer = GetComponent<Timer>();
        //_canvas = GameObject.Find("Canvas");
        _startPosition = transform.Find("StartPosition").gameObject;
        _endPosition = transform.Find("EndPosition").gameObject;
        if (PlayerPrefab != null)
        {
            _playerGO = GameObject.Instantiate(PlayerPrefab, _startPosition.transform.position,
                _startPosition.transform.rotation);
        }


        //if (StartPrefab != null) {
        //	_startGO = GameObject.Instantiate(StartPrefab, _startPosition.transform.position, _startPosition.transform.rotation, transform);
        //}
        //else Debug.LogError("StartPrefab is missing!");
        //if (EndPrefab != null) {
        //	_endGO = GameObject.Instantiate(EndPrefab, _endPosition.transform.position, _endPosition.transform.rotation, transform);
        //}
        //else Debug.LogError("EndPrefab is missing!");
        //if (HudPrefab != null) {
        //	_hud = GameObject.Instantiate(HudPrefab);
        //}
        //else Debug.LogError("HudPrefab is missing!");

        _dayMat = GameObject.Find("SkyCamera/Plane").GetComponent<MeshRenderer>();
        _nightMat = GameObject.Find("SkyCamera/Plane2").GetComponent<MeshRenderer>();

        EventManager.StartListening(GameEvent.LEVEL_TIMER_TICK,
            new Action<EventParam>(delegate(EventParam param)
            {
                var timeInSecond = ((TimerEventParams) param).currentTime;
                UpdateSkyColor(timeInSecond);
            }));

        EventManager.TriggerEvent(GameEvent.START_LEVEL_TIMER, null);
        EventManager.StartListening(GameEvent.PLAYER_DEATH,
            new Action<EventParam>(delegate(EventParam param) { StartCoroutine(PlayerDeath()); }));
        EventManager.StartListening(GameEvent.PICKUP,
            new Action<EventParam>(delegate(EventParam param) { _starCount++; }));

        EventManager.StartListening(GameEvent.NEXT_LEVEL,
            new Action<EventParam>(delegate(EventParam param) { TriggerNextScene(); }));
        EventManager.StartListening(GameEvent.RETRY_LEVEL,
            new Action<EventParam>(delegate(EventParam param) { TriggerRestartScene(); }));
        EventManager.StartListening(GameEvent.LEVEL_TIMER_END,
            new Action<EventParam>(delegate(EventParam param)
            {
                EventManager.TriggerEvent(GameEvent.LEVEL_COMPLETED, new LevelCompletedParams(false, 0, 0));
            }));
    }

    void Update()
    {
    }

    public IEnumerator PlayerDeath()
    {
        //TODO: playanimation
        //After animation:
        Debug.Log("Player died");
        _playerGO.GetComponent<GrandmaSoundController>().PlayDeath();
        yield return new WaitForSeconds(3);
        TriggerRestartScene();
    }


    public void RespawnPlayer()
    {
        var temp = _playerGO;
        if (PlayerPrefab != null)
        {
            _playerGO = GameObject.Instantiate(PlayerPrefab, _startPosition.transform.position,
                _startPosition.transform.rotation);
        }

        GameObject.DestroyImmediate(temp);
    }

    void UpdateSkyColor(float timeInSeconds)
    {
        float offset = 6f / 24f;
        float angle = ((timeInSeconds / 86400f) - offset) * 2 * Mathf.PI;
        if (angle > 2 * Mathf.PI) angle -= 2 * Mathf.PI;
        float sin = Mathf.Sin(angle);
        if (Mathf.Abs(sin) < 0.5f)
        {
            float dayAlpha = 0.5f + sin;
            float nightAlpha = 0.5f - sin;
            _nightMat.materials[0].color = new Color(1, 1, 1, nightAlpha);
        }
        else if (0 < angle && angle < Mathf.PI) _nightMat.materials[0].color = new Color(1, 1, 1, 0);
        else _nightMat.materials[0].color = new Color(1, 1, 1, 1);
    }

    public void TriggerWinScene(GameObject celebrationPrefab)
    {
        GameObject.FindObjectOfType<PlayerController>().enabled = false;
        celebrationPrefab.GetComponent<Animator>().SetTrigger("Win");
        float timeElasped = _timer.currentTick - _timer.startTimeOffset;
        int timePoints = Mathf.RoundToInt(50000f - timeElasped) + 10000;
//		int stars = 0;
//		int totalPoints = timePoints;
//		if (totalPoints > 40000f) {
//			stars = 3;
//		}
//		else if (totalPoints > 30000f) {
//			stars = 2;
//		}
//		else if (totalPoints > 15000f) {
//			stars = 1;
//		}
        EventManager.TriggerEvent(GameEvent.LEVEL_COMPLETED, new LevelCompletedParams(true, timePoints, _starCount));
    }

    public void TriggerRestartScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void TriggerNextScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}