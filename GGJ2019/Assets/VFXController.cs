﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour {

    public GameObject collidingVFX;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}

    public void TriggerCollide(Vector3 location){
        GameObject particle = ((GameObject)Instantiate(collidingVFX));
        particle.transform.position = location;
    }
}