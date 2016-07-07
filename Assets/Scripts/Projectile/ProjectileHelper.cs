using UnityEngine;
using System.Collections.Generic;

public class ProjectileHelper
{

    public static Vector2 FindStartingLocation(Projectile.StartLocation startLocation, Character user, Vector2 gridOffset, GridLocation target = null)
    {
        Vector2 trueOffset = user.facing == Character.Facing.Right ? gridOffset : new Vector2(gridOffset.x * -1f, gridOffset.y);

        switch (startLocation)
        {
            case Projectile.StartLocation.User:
                return (user.currentLocation.Loc + trueOffset);
            case Projectile.StartLocation.BackRow:
                if (user.facing == Character.Facing.Right)
                    return new Vector2(0 + gridOffset.x, user.currentLocation.Loc.y + gridOffset.y);
                else
                    return new Vector2(Grid.instance.width - 1 + gridOffset.x, user.currentLocation.Loc.y + gridOffset.y);
            case Projectile.StartLocation.FrontRow:
                return GetFrontRow(user, user.currentLocation);
            case Projectile.StartLocation.EnemyBackRow:
                if (user.facing == Character.Facing.Right)
                    return new Vector2(Grid.instance.width - 1 + gridOffset.x, user.currentLocation.Loc.y + gridOffset.y);
                else
                    return new Vector2(0 + gridOffset.x, user.currentLocation.Loc.y + gridOffset.y);
            case Projectile.StartLocation.Target:
                if (target == null)
                    return trueOffset;
                else
                    return target.Loc + trueOffset;
            default:
                return trueOffset;
        }
        
    }

    public static Vector2 FindStartingLocation(Character user, Vector2 gridOffset, GridLocation start)
    {
        Vector2 trueOffset = user.facing == Character.Facing.Right ? gridOffset : new Vector2(gridOffset.x * -1f, gridOffset.y);
        return trueOffset + start.Loc;
    }

    public static Vector2 GetFrontRow(Character user, GridLocation startLoc, bool needUnoccupied = false)
    {
        Vector2 direction = user.GetForwardDirection();

        List<GridLocation> potential = new List<GridLocation>();
        potential.Add(Grid.instance.GetGridLocation(startLoc.Loc + direction));

        while (potential[potential.Count - 1] != null && user.DetermineAllegiance(potential[potential.Count-1].faction) != Faction.Alleigance.Enemy)
            potential.Add(Grid.instance.GetGridLocation(potential[potential.Count - 1].Loc + direction));

        potential.RemoveAt(potential.Count - 1);

        for (int i = potential.Count - 1; i >= 0; i--)
            if (potential[i] != null && (!needUnoccupied || potential[i].Occupier == null))
                return potential[i].Loc;
        

        return startLoc.Loc;
    }

    
}
