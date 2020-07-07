using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour {

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip scream_clip, die_clip;

    [SerializeField]
    private AudioClip[] attack_clips;
	// Use this for initialization
	void Awake () {

        audioSource = GetComponent<AudioSource>();
		
	}

    public void Play_ScreamSound()
    {
        audioSource.clip = scream_clip;
        audioSource.Play();
    }
	
    public void Play_AttackSound()
    {
        audioSource.clip = attack_clips[Random.Range(0, attack_clips.Length)];
        audioSource.Play();
    }

    public void Play_DieSound()
    {
        audioSource.clip = die_clip;
        audioSource.Play();
    }
}
