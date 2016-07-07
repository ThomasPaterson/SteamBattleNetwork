using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Faction : ScriptableObject
{
	public enum Alleigance {Friendly, Unaligned, Empty, Enemy}

	public string factionName;
	public Color color;
	public Sprite icon;
	public Sprite gridLocationSprite;


	public static List<GridLocation> GetValidLocations(Character user, Alleigance target, Faction allowedFaction, bool removeOccupied = false)
	{
		List<GridLocation> available = new List<GridLocation>();

		foreach (GridLocation location in Grid.instance.gridLocations)
			if (location.DetermineAllegiance(user.faction, false) == target && location.faction == allowedFaction && (!removeOccupied || location.Occupier == null))
				available.Add(location);

		return available;
	}

}
