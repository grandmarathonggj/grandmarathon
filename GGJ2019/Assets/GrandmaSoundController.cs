using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaSoundController : MonoBehaviour {

    private AudioSource elasticAS;
    public AudioClip elastic;

    public float chargeAmount = 0;

    void Awake () {
        elasticAS = GetComponent<AudioSource>();
	}

    private void Start()
    {
        elasticAS.loop = true;
        elasticAS.clip = elastic;
        elasticAS.Play();
    }


	// Update is called once per frame
	void Update () {
        if(Mathf.Approximately(chargeAmount, 0)){
            elasticAS.pitch = 0;
            elasticAS.volume = 0;
        }else{
            elasticAS.pitch = chargeAmount * 1.2f;
            elasticAS.volume = chargeAmount * 2f;
        }
	}
}
