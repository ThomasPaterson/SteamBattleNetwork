using UnityEngine;
using System.Collections;

public class AudioEffect : ProjectileEffect
{

    public AudioClip sound;

    public override void TriggerEffect(GridLocation loc)
    {
        SoundManager.instance.PlaySound(sound);
    }

    public override void TriggerEffect(Character character)
    {
        SoundManager.instance.PlaySound(sound);
    }

    public override void TriggerEffect()
    {
        SoundManager.instance.PlaySound(sound);
    }



}
