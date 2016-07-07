using UnityEngine;
using System.Collections;

public class SpawnEffectEffect : ProjectileEffect
{
    public EffectSpawnInfo[] effects;


    public override void TriggerEffect(GridLocation loc)
    {

        foreach (EffectSpawnInfo effect in effects)
        {
            if (loc != null && effect.centerOnTargetHit)
                SpawnEffect(loc.GetTopCenter(), effect);
            else
                SpawnEffect(transform.position, effect);
        }
        
    }

    public override void TriggerEffect(Character character)
    {

        foreach (EffectSpawnInfo effect in effects)
        {
            if (character != null && effect.centerOnTargetHit)
                SpawnEffect(character.transform.position, effect);
            else
                SpawnEffect(transform.position, effect);
        }
    }

    public override void TriggerEffect()
    {
        foreach (EffectSpawnInfo effect in effects)
            SpawnEffect(transform.position, effect);

    }

    void SpawnEffect(Vector3 pos, EffectSpawnInfo effect)
    {
        Instantiate(effect.effect, pos + effect.offset, effect.effect.transform.rotation);
    }
    


}

[System.Serializable]
public class EffectSpawnInfo
{
    public GameObject effect;
    public Vector3 offset;
    public bool centerOnTargetHit;
}