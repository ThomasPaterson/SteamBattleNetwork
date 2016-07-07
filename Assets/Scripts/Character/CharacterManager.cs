using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour 
{

	public static CharacterManager instance;

	public Character player;
	public HealthTracker playerTracker;
	public HealthTracker enemyTracker;
    public SkillSelectionPanel skillSelectionPanel;
    public Faction playerFaction;
    public Faction enemyFaction;
    public Dictionary<Faction, List<Character>> characters = new Dictionary<Faction, List<Character>>();

	void Awake()
	{
		instance = this;
        skillSelectionPanel.gameObject.SetActive(false);
	}

	IEnumerator Start()
	{

		while (Grid.instance == null)
			yield return null;

		yield return null; //wait for one frame for positions to set

		player = SetupCharacter(CharacterConfig.instance.playerPrefab);
		SetupCharacter(CharacterConfig.instance.enemyPrefab);
		SetupCharacter(CharacterConfig.instance.enemyPrefab);
        SetupCharacter(CharacterConfig.instance.enemyPrefab);

        StartCoroutine(SpawnWaveAfterDelay(15f));
        StartCoroutine(SpawnWaveAfterDelay(30f));
    }

	Character SetupCharacter(GameObject prefab)
	{

		GameObject character = Instantiate(prefab) as GameObject;

		List<GridLocation> available = Faction.GetValidLocations(character.GetComponent<Character>(), Faction.Alleigance.Empty, character.GetComponent<Character>().faction);
		GridLocation locToUse = available[Mathf.FloorToInt(Random.Range(0, available.Count))];

		character.GetComponent<Character>().MoveToLocation(locToUse);

		return character.GetComponent<Character>();
	}

    public void SpawnCharacter(GameObject prefab, GridLocation loc, Character spawner)
    {
        if (loc.Occupier != null)
            return;

        GameObject character = (GameObject)Instantiate(prefab, loc.GetTopCenter(), prefab.transform.rotation);
        character.GetComponent<Character>().faction = spawner.faction;
        character.GetComponent<Character>().facing = spawner.facing;
        character.GetComponent<Character>().MoveToLocation(loc);
    }

	public HealthTracker GetTracker(Faction faction)
	{
		if (faction.factionName == "Player")
			return playerTracker;
		else
			return enemyTracker;
	}

    public Faction GetControllingFaction()
    {
        if (player == null)
            return null;
        else
            return player.faction;
    }

    public void SetupSkillPanel()
    {
        if (player != null)
            skillSelectionPanel.gameObject.SetActive(true);
    }

    public void AddCharacter(Character character)
    {
        if (!characters.ContainsKey(character.faction))
            characters.Add(character.faction, new List<Character>());

        characters[character.faction].Add(character);
    }

    public void RemoveCharacter(Character character)
    {
        characters[character.faction].Remove(character);
    }

    public List<Character> GetCharactersFromFaction(Faction faction)
    {
        if (characters.ContainsKey(faction))
            return characters[faction];
        else
            return null;
    }

    public Faction GetOpposingFaction(Faction faction)
    {
        if (faction == playerFaction)
            return enemyFaction;
        else
            return playerFaction;
    }

 
    IEnumerator SpawnWaveAfterDelay(float delay)
    {
        float timePassed = 0f;

        while (timePassed < delay)
        {
            timePassed += Time.deltaTime * GameStateManager.instance.gameTime;
            yield return null;
        }

        SetupCharacter(CharacterConfig.instance.enemyPrefab);
        SetupCharacter(CharacterConfig.instance.enemyPrefab);
        SetupCharacter(CharacterConfig.instance.enemyPrefab);
    }
}
