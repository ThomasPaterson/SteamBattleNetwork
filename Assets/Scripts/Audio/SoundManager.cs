using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour 
{

	public static SoundManager instance;

	public float volume = 1f;

	private AudioSource audioSource;
	private AudioClip currentMusic = null;

	void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		instance = this;
	}

	void Start()
	{
		PlayMusic(SoundConfig.instance.battleMusic);
	}

	public void PlaySound(AudioClip audioClip)
	{
		if (audioClip != null)
			AudioSource.PlayClipAtPoint(audioClip, Vector3.zero, volume);
	}

	public void PlayMusic(AudioClip musicClip)
	{
		if (currentMusic != musicClip)
		{
			currentMusic = musicClip;
			audioSource.clip = musicClip;
			audioSource.Play ();
		}
	}
}
