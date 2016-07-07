using UnityEngine;
using System.Collections;

public class SoundConfig : MonoBehaviour 
{

	public static SoundConfig instance;

	public AudioClip battleMusic;
	public AudioClip tookDamage;
	public AudioClip shoot;
	public AudioClip move;
	
	void Awake()
	{
		instance = this;
	}
}
