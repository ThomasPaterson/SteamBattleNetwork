using UnityEngine;
using System.Collections;

public class SpawnCharacterEffect : ProjectileEffect
{
    public GameObject toSpawn;

    public override void TriggerEffect(GridLocation loc)
    {
        CharacterManager.instance.SpawnCharacter(toSpawn, loc, projectile.user);
    }

 

}
