using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

	//Prefabs
	public GameObject PlayerPrefab;
	public GameObject StartPrefab;
	public GameObject EndPrefab;
	public GameObject HudPrefab;

	//References
	private GameObject _startPosition;
	private GameObject _endPosition;
	private GameObject _startGO;
	private GameObject _endGO;
	private GameObject _hud;
	private GameObject _canvas;
	private MeshRenderer _dayMat;
	private MeshRenderer _nightMat;

	void Start () {
		//_canvas = GameObject.Find("Canvas");
		//_startPosition = transform.Find("StartPosition").gameObject;
		//_endPosition = transform.Find("EndPosition").gameObject;

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
			new Action<EventParam>(delegate (EventParam param) {
				var timeInSecond = ((TimerEventParams)param).currentTime;
				UpdateSkyColor(timeInSecond);
			}));
	}
	
	void Update () {
		
	}

	void UpdateSkyColor(float timeInSeconds) {
		float angle = (timeInSeconds / 86400) * 2 * Mathf.PI;
		float sin = Mathf.Sin(angle);
		if (Mathf.Abs(sin) < 0.5f) {
			float dayAlpha = 0.5f + sin;
			float nightAlpha = 0.5f - sin;
			_nightMat.materials[0].color = new Color(1, 1, 1, nightAlpha);
		}
		else if (angle < Mathf.PI) _nightMat.materials[0].color = new Color(1, 1, 1, 0);
		else _nightMat.materials[0].color = new Color(1, 1, 1, 1);
	}

	public void TriggerWinScene() {
		Debug.Log("You WIN!");

	}

	public void TriggerRestartScene() {
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

	}
}
