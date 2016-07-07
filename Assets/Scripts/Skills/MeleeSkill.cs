using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeSkill : Skill
{
    public GameObject effect;
    public int minDamage;
    public int baseRange;
    public int maxCharge;
    public float chargeStep;

    public override void UseSkill(Character user)
    {
        UseSkill(user, 0f);
    }

    public override void UseSkill(Character user, float charge)
    {
        base.UseSkill(user);
        float chargeUsed = DetermineChargeUsed(charge);

        List<GridLocation> effected = FindEffected(user, chargeUsed, user.facing == Character.Facing.Right ? 1f : -1f);
        

        foreach (GridLocation toEffect in effected)
        {
            if (toEffect.Occupier != null && toEffect.Occupier.faction != user.faction)
                toEffect.Occupier.TakeDamage(DetermineDamage(chargeUsed));

            toEffect.Appearance.Flash();
        }
    }

    int DetermineDamage(float chargeUsed)
    {
        if (chargeUsed == 0)
            return minDamage;
        else
            return value;
    }

    float DetermineChargeUsed(float charge)
    {
        return Mathf.Min(Mathf.FloorToInt(charge / chargeStep), maxCharge);
    }

    List<GridLocation> FindEffected(Character user, float chargeUsed, float sign)
    {

        float numEffected = baseRange + chargeUsed;

        List<GridLocation> effected = new List<GridLocation>();

        for (float i = 1; i <= numEffected; i++)
        {
            GridLocation loc = Grid.instance.GetGridLocation((int)(user.currentLocation.Loc.x + i * sign), (int)user.currentLocation.Loc.y);

            if (loc == null)
                break;
            else
                effected.Add(loc);
        }

        return effected;

    }
}
