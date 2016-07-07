using UnityEngine;
using System.Collections;

public class Skill : ScriptableObject 
{
    

    public string skillName;
    public bool canCharge;
	public int value;
	public Sprite icon;
	public int ammo;
	public AudioClip playOnUse;
    public float useDelay;
    public string chargeAnimation;
    public string useAnimation;
    public Faction.Alleigance aiTargetType; //for AI to decide what to target

    public virtual void UseSkill(Character user)
    {
        SoundManager.instance.PlaySound(playOnUse);
    }

    public virtual void UseSkill(Character user, float charge)
	{
		SoundManager.instance.PlaySound(playOnUse);
	}

	public virtual GridLocation FindValidTarget(Character user)
	{
		return null;
	}

    public virtual SkillData CreateSkillData(Character character, SkillData.FireType fireType)
    {
        return new SkillData(this, fireType);
    }

}
