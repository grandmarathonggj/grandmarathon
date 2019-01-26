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
		Debug.Log("Entered");
		if (other.gameObject.GetComponent<PlayerController>() != null) {
			Debug.Log("You WIN 1!");
			_level.TriggerWinScene();
		}
	}
}
