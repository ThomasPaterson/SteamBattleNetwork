using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShieldSkill : Skill
{
    public GameObject shieldObject;
    public float maxHealth;
    public float minSizePercent;
    public float degradeRate;
    public float rechargeRate;
    public AudioClip shieldHit;
    public AudioClip shieldBreak;

    public override SkillData CreateSkillData(Character character, SkillData.FireType fireType)
    {
        return new ShieldSkillData(this, fireType);
    }
}
