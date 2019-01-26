using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour {
	private Level _level;
	private bool _isPlayerInside;
	private GameObject _player;
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
				_level.TriggerWinScene();
				_isPlayerInside = false;
			}
		}
	}

	private void OnTriggerEnter(Collider other) {
		Debug.Log("Player entered win zone");
		var otherGO = other.gameObject;
		if (otherGO.GetComponent<PlayerController>() != null) {
			_player = otherGO;
			_isPlayerInside = true;
		}
	}

	private void OnTriggerExit(Collider other) {
		Debug.Log("Player didn't win");
		if (other.gameObject.GetComponent<PlayerController>() != null) {
			_isPlayerInside = false;
		}
	}
}
