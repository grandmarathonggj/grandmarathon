using System.Collections;
using System.Collections.Generic;
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

	void Start () {
		_canvas = GameObject.Find("Canvas");
		_startPosition = transform.Find("StartPosition").gameObject;
		_endPosition = transform.Find("EndPosition").gameObject;

		if (StartPrefab != null) {
			_startGO = GameObject.Instantiate(StartPrefab, _startPosition.transform.position, _startPosition.transform.rotation, transform);
		}
		else Debug.LogError("StartPrefab is missing!");
		if (EndPrefab != null) {
			_endGO = GameObject.Instantiate(EndPrefab, _endPosition.transform.position, _endPosition.transform.rotation, transform);
		}
		else Debug.LogError("EndPrefab is missing!");
		if (HudPrefab != null) {
			_hud = GameObject.Instantiate(HudPrefab);
		}
		else Debug.LogError("HudPrefab is missing!");
	}
	
	void Update () {
		
	}

	public void TriggerWinScene() {


	}

	public void TriggerRestartScene() {
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

	}
}
