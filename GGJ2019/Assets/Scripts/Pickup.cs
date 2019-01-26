using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Pickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	private void OnTriggerEnter(Collider other) {
		Debug.Log("Enter Pickup");
		if (other.gameObject.GetComponent<PlayerController>() != null) {
			Debug.Log("Do pickup");
			EventManager.TriggerEvent(GameEvent.PICKUP, new EventParam());
			transform.gameObject.SetActive(false);
		}
	}
}
