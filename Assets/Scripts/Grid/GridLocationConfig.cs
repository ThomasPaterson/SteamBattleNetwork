using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridLocationConfig : MonoBehaviour 
{

	public static GridLocationConfig instance;

	public GameObject gridLocationPrefab;
    public Sprite damagedGridSprite;
    public float timeToFlashDamage;


	void Awake()
	{
		instance = this;


	}


}
