using UnityEngine;
using System.Collections;

public class CharacterConfig : MonoBehaviour 
{

	public static CharacterConfig instance;

	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	void Awake()
	{
		instance = this;
	}
}
