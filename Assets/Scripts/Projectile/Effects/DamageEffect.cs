using UnityEngine;
using System.Collections;

public class DamageEffect : ProjectileEffect
{
   
    public override void TriggerEffect(GridLocation loc)
    {
        if (loc.Occupier != null)
            loc.Occupier.TakeDamage(projectile.skillUsed.value);
    }

    public override void TriggerEffect(Character character)
    {
        character.TakeDamage(projectile.skillUsed.value);
    }
    


}
