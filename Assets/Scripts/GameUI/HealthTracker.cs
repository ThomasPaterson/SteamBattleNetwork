using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HealthTracker : MonoBehaviour 
{
	public Faction faction;

	private List<Character> tracking = new List<Character>();
	private Text text;

	void Awake()
	{
		text = GetComponent<Text>();
	}

	void Update()
	{
		int totalHealth = 0;

		foreach (Character character in tracking)
			totalHealth += character.health;

		text.text = totalHealth.ToString();
	}

	public void AddCharacter(Character character)
	{
		tracking.Add(character);
	}

	public void RemoveCharacter(Character character)
	{
		tracking.Remove(character);
	}

}
