using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AICharacter : Character
{

    public GridLocation GetTarget(Skill skill)
    {
        return GetTarget(skill.aiTargetType);
    }


    public GridLocation GetTarget(Faction.Alleigance targetType)
    {
        switch (targetType)
        {
            case Faction.Alleigance.Enemy:
                return FindClosest(CharacterManager.instance.GetCharactersFromFaction(CharacterManager.instance.GetOpposingFaction(faction)));
            case Faction.Alleigance.Friendly:
                return FindClosest(CharacterManager.instance.GetCharactersFromFaction(faction));
            case Faction.Alleigance.Empty:
                return Grid.instance.GetRandomEmpty(CharacterManager.instance.GetOpposingFaction(faction));
            default:
                return currentLocation;
        }
    }

    public override void UseSkill(Skill skillToUse)
    {
        if (skillToUse is MoveSkill)
        {
            GridLocation target = skillToUse.FindValidTarget(this);
            if (target == null)
                Debug.Log("no valid move square");
            else
                Debug.Log("moving to: " + target.Loc.ToString());

            ((MoveSkill)skillToUse).UseSkill(this, target);

        }
        else
            skillToUse.UseSkill(this);
    }

    GridLocation FindClosest(List<Character> characters)
    {
        float bestScore = float.MaxValue;
        GridLocation bestLoc = null;

        foreach (Character character in characters)
        {
            if (character == this)
                break;

            if (Vector2.Distance(character.currentLocation.Loc, currentLocation.Loc) < bestScore)
            {
                bestScore = Vector2.Distance(character.currentLocation.Loc, currentLocation.Loc);
                bestLoc = character.currentLocation;
            }
        }

        return bestLoc;
    }
}
