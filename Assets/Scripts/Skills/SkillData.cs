using UnityEngine;
using System.Collections;

public class SkillData
{
    public enum FireType { SwipeForward, SwipeBackward, Press, UseCharged, HeldDown, SwipeDown, SwipeUp }

    public FireType fireType;
    public Skill skill;
    public float charge;
    public float ammo;
    public float cooldown;
    public bool charging;

    public SkillData()
    {
    }


    public SkillData(Skill skill)
    {
        this.skill = skill;
    }

    public SkillData(Skill skill, FireType fireType)
    {
        this.skill = skill;
        this.fireType = fireType;
    }

    public virtual void Update(float timePassed)
    {
    }

    public virtual void SetupCharging(Character character)
    {
        charging = true;
        character.PlayAnimation(skill.chargeAnimation);
    }

    public virtual void FinishCharging()
    {
        charging = false;
    }


    
}
