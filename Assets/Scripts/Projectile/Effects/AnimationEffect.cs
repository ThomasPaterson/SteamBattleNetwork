using UnityEngine;
using System.Collections;

public class AnimationEffect : ProjectileEffect
{
    public string animationToPlay;

    private Animator anim;

	void Start ()
    {
        anim = GetComponentInChildren<Animator>();
	}

    public override void TriggerEffect(GridLocation loc)
    {
        if (anim != null)
            anim.SetTrigger(animationToPlay);
    }

    public override void TriggerEffect(Character character)
    {
        if (anim != null)
            anim.SetTrigger(animationToPlay);
    }

    public override void TriggerEffect()
    {
        if (anim != null)
            anim.SetTrigger(animationToPlay);
    }
}
