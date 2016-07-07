using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveSkill : Skill 
{
	public enum MoveType{Walk, Teleport}


	public MoveType moveType;
    public bool useRow;
    public Vector2 preferredOffset;
    public int preferredRow;


	public void UseSkill(Character user, GridLocation targeted)
	{
		if (targeted != null)
			user.MoveToLocation(targeted);
	}

	public override GridLocation FindValidTarget (Character user)
	{
        Debug.Log("choosing target");

        GridLocation target = ((AICharacter)user).GetTarget(this);
        target = Grid.instance.GetGridLocation(target.Loc - preferredOffset * user.GetForwardDirection().x, true);

        if (target == null)
            return null;

        List<GridLocation> available = Faction.GetValidLocations(user, Faction.Alleigance.Empty, user.faction, true);

		if (available.Count == 0)
			return null;


        if (moveType == MoveType.Walk)
            return FindClosest(available, user.currentLocation, target);
        else
            return FindClosest(available, target, target);
	}

    GridLocation FindClosest(List<GridLocation> available, GridLocation starting, GridLocation target)
    {
        GridLocation winner = null;
        float bestScore = float.MaxValue;

        foreach (GridLocation loc in available)
        {
            if (Vector2.Distance(loc.Loc, starting.Loc) <= 1f)
            {
                if (Vector2.Distance(loc.Loc, target.Loc) < bestScore)
                {
                    winner = loc;
                    bestScore = Vector2.Distance(loc.Loc, target.Loc);
                }
            }
        }

        return winner;
    }

}
