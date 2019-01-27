using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaSoundController : MonoBehaviour {

	public AudioClip[] GruntSounds;
    public AudioClip hmmSound;
	public AudioClip DeathSound;
	private AudioSource _audioSource;

	// Use this for initialization
	void Start () {
		_audioSource = transform.Find("Audio").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayRandomGrunt() {
		int i = Random.Range(0, GruntSounds.Length);
        _audioSource.pitch = 1;
        _audioSource.volume = 1f;
		_audioSource.clip = GruntSounds[i];
		_audioSource.loop = false;
		_audioSource.Play();
	}

    public void PlayHmm()
    {
        _audioSource.clip = hmmSound;
        _audioSource.pitch = 0.75f;
        _audioSource.volume = 2f;
        _audioSource.loop = false;
        _audioSource.Play();
    }
	public void PlayDeath() {
		_audioSource.clip = DeathSound;
        _audioSource.pitch = 1;
        _audioSource.volume = 1f;
		_audioSource.loop = false;
		_audioSource.Play();
	}
}
