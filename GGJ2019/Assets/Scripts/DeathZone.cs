using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DefaultNamespace;

public class DeathZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<PlayerController>() != null) {
			EventManager.TriggerEvent(GameEvent.PLAYER_DEATH, null);
		}
	}
}
