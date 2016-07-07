using UnityEngine;
using System.Collections;

[System.Serializable]
public class EntryCondition 
{

    public enum Type { Health, Ammo, DistanceFromTarget, Cooldown, OnRow}
    public enum ValueCheck { GreaterThan, Equal, LessThan, NotEqual}

    public Type type;
    public ValueCheck valueCheck;
    public SkillData skillData;
    public float value;
    public Faction.Alleigance targetType;


    public bool CheckCondition(Character character, GridLocation target = null)
    {
        switch(type)
        {
            case Type.Health:
                return CheckValue(character.health / character.maxHealth);
            case Type.Ammo:
                if (skillData == null)
                    return false;
                else
                    return CheckValue(skillData.ammo);
            case Type.DistanceFromTarget:
                if (target == null)
                    return false;
                else
                    return CheckValue(Vector2.Distance(character.currentLocation.Loc, target.Loc));
            case Type.Cooldown:
                if (skillData == null)
                    return false;
                else
                    return CheckValue(skillData.cooldown);
            case Type.OnRow:
                if (target == null)
                    return false;
                else
                    return CheckValue(character.currentLocation.Loc.y - target.Loc.y);
            default:
                return false;
        }
    }

    bool CheckValue(float toCheck)
    {
        switch (valueCheck)
        {
            case ValueCheck.Equal:
                return Mathf.Approximately(toCheck, value);
            case ValueCheck.GreaterThan:
                return toCheck > value;
            case ValueCheck.LessThan:
                return toCheck < value;
            case ValueCheck.NotEqual:
                return !Mathf.Approximately(toCheck, value);
            default:
                return false;
        }
    }

}
