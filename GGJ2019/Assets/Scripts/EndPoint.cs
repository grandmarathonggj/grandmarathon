using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour {
	private Level _level;
	private bool _isPlayerInside;
	private GameObject _player;
	private Camera _mainCamera;
	private GameObject _bed;
	private bool _celebrationPlaying = false;

	private void Awake()
	{
		_mainCamera = Camera.main;
		_bed = transform.Find("Celebration").gameObject;
	}

	// Use this for initialization
	void Start () {
		_isPlayerInside = false;
		_level = GameObject.FindObjectOfType<Level>();
	}
	
	// Update is called once per frame
	void Update () {
		if (_isPlayerInside) {
			if (_player.GetComponent<CustomPhysics>().grounded) {
				Debug.Log("You win");
				_level.TriggerWinScene(_bed);
				_player.SetActive(false);
				_isPlayerInside = false;
				_celebrationPlaying = true;
			}
		}
	}

	private void OnTriggerEnter(Collider other) {
		Debug.Log("Player entered win zone");
		var otherGO = other.gameObject;
		if (otherGO.GetComponent<PlayerController>() != null) {
			_player = otherGO;
			_isPlayerInside = true;
			
			// Celebration Logic
//			_bed.GetComponent<Animator>().SetTrigger("Win");
		}		
	}

	private void OnTriggerExit(Collider other) {
		Debug.Log("Player didn't win");
		if (other.gameObject.GetComponent<PlayerController>() != null) {
			_isPlayerInside = false;
		}
	}

	public bool CelebrationPlaying
	{
		get { return _celebrationPlaying; }
	}

	public GameObject Bed
	{
		get { return _bed; }
	}
}
