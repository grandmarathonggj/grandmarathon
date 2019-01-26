using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour {
	private Level _level;
	// Use this for initialization
	void Start () {
		_level = GameObject.FindObjectOfType<Level>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "Player") {
			_level.TriggerWinScene();
		}
	}
}
