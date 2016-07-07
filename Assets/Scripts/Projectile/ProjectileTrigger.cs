using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ProjectileTrigger
{
    public enum TriggerType {
        Grid, //hits a grid location, uses faction
        Character, //hits a character, uses faction
        Timed, //after the value, goes off
        Destination, //reaches the destination it is set to do via projectile
        Removed, //destroyed by some means
        OutOfBounds //went out of bounds 
    }

    public enum LogicType { And, Or}

    public TriggerType triggerType;
    public List<Faction.Alleigance> triggeredAllegiances;
    public float value = -1f;
    public LogicType logicType = LogicType.And;

    public float currentHit = 0f;

    public bool Triggered(GridLocation location, Character owner)
    {
        Faction.Alleigance allegiance = location.DetermineAllegiance(owner.faction);
        if (triggeredAllegiances.Contains(allegiance))
        {
            currentHit++;

            if (currentHit >= value)
                return true;
        }

        return false;
    }

    public bool Triggered(Character characterHit, Character owner)
    {
        Faction.Alleigance allegiance = characterHit.DetermineAllegiance(owner.faction);
        if (triggeredAllegiances.Contains(allegiance))
        {
            currentHit++;

            if (currentHit > value)
                return true;
        }

        return false;
    }

    public bool CheckTime(Projectile proj)
    {
        return proj.GetTimeAlive() > value;
    }

    public bool CheckArrived(Projectile proj)
    {
        return proj.arrivedAtDestination;
    }

    public bool CheckRemoved(Projectile proj)
    {
        return proj.removedBySkill;
    }

    public bool CheckOutOfBounds(Projectile proj)
    {
        return proj.removedByBounds;
    }

    public static bool CheckTriggers(ProjectileTrigger[] triggers, Projectile proj, GameObject collider = null)
    {
        bool success = true;

        foreach (ProjectileTrigger trigger in triggers)
        {
            //only keep going if chance of changing result (so either an OR, or a AND while still successful
            if (trigger.logicType == LogicType.Or || success)
            {
                bool triggerSuccess = CheckTrigger(trigger, proj, collider);

                if (triggerSuccess && trigger.logicType == LogicType.Or)
                    return true;
                else if (!triggerSuccess && trigger.logicType == LogicType.And)
                    success = false;
            }
        }

        return success;

    }

    public static bool CheckTrigger(ProjectileTrigger trigger, Projectile proj, GameObject collider = null)
    {
        switch (trigger.triggerType)
        {
            case TriggerType.Character:
                if (collider != null && collider.GetComponent<Character>() != null)
                    return trigger.Triggered(collider.GetComponent<Character>(), proj.user);
                else
                    return false;
            case TriggerType.Grid:
                if (collider != null && collider.GetComponent<GridLocation>() != null)
                    return trigger.Triggered(collider.GetComponent<GridLocation>(), proj.user);
                else
                    return false;
            case TriggerType.Timed:
                    return trigger.CheckTime(proj);
            case TriggerType.Destination:
                return trigger.CheckArrived(proj);
            case TriggerType.OutOfBounds:
                return trigger.CheckOutOfBounds(proj);    
            default:
                return false;
        }
    }


}
