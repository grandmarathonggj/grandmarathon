using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaSoundController : MonoBehaviour {

	public AudioClip[] GruntSounds;
	public AudioClip DeathSound;
	private AudioSource _audioSource;

	// Use this for initialization
	void Start () {
		_audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayRandomGrunt() {
		int i = Random.Range(0, GruntSounds.Length);
		_audioSource.clip = GruntSounds[i];
		_audioSource.loop = false;
		_audioSource.Play();
	}

	public void PlayDeath() {
		_audioSource.clip = DeathSound;
		_audioSource.loop = false;
		_audioSource.Play();
	}
}
