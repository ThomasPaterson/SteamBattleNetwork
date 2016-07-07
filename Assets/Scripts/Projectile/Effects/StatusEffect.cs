using UnityEngine;
using System.Collections;

public class StatusEffect : ProjectileEffect
{
    public StatusData statusToApply;

    public override void TriggerEffect(GridLocation loc)
    {
        if (loc.Occupier != null)
            loc.Occupier.GetComponent<CharacterStatusHandler>().ApplyStatus(statusToApply);
    }

    public override void TriggerEffect(Character character)
    {
        character.GetComponent<CharacterStatusHandler>().ApplyStatus(statusToApply);
    }
}
