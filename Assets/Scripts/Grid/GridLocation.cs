using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridLocation : MonoBehaviour
{
	public Vector2 Loc {get; private set;}
	//for debug
	public LocationAppearance Appearance {get; private set;}
	public Character Occupier;
	public Faction faction;

	public void Init(int x, int y, Faction faction)
	{
		Loc = new Vector2((float)x, (float)y);
		Appearance = GetComponent<LocationAppearance>();
		this.faction = faction;
		Appearance.SetFaction(faction);
	}


	public void CharacterEnter(Character character)
	{
		if (Occupier == null)
			Occupier = character;
		else
			Debug.LogError("Character tried to enter occupied square");

		Appearance.SetOccupied(character);

	}

	public void CharacterLeave()
	{
		if (Occupier != null)
			Occupier = null;

		Appearance.SetOccupied(null);
	}

	public Faction.Alleigance DetermineAllegiance(Faction targeter, bool ignoreEmpty = true)
	{

        if (ignoreEmpty)
        {
            if (faction.factionName == targeter.factionName)
                return Faction.Alleigance.Friendly;
            else
                return Faction.Alleigance.Enemy;
        }
        else if (Occupier == null)
			return Faction.Alleigance.Empty;
		else
			return Occupier.DetermineAllegiance(targeter);
	}

	public GridLocation GetGridLocation()
	{
		return this;
	}

    public Vector3 GetTopCenter()
    {
        Bounds b = GetComponentInChildren<Collider>().bounds;

        return b.center + Vector3.up * b.extents.y;
    }

}
