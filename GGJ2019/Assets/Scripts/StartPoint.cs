using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class StartPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.name == "Grandma") {
			EventManager.TriggerEvent(GameEvent.START_LEVEL_TIMER, null);
		}
	}
}
