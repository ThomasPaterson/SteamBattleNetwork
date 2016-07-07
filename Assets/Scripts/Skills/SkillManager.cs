using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SkillManager : MonoBehaviour
{
    public Dictionary<SkillData.FireType, SkillData> skillTriggers = new Dictionary<SkillData.FireType, SkillData>();
    public Skill baseSkill;

    private SkillData skillInUse = null; // you can only be using one 
    private Character character;
    private SkillData baseSkillData;

    void Awake()
    {

        character = GetComponent<Character>();
        baseSkillData = baseSkill.CreateSkillData(character, SkillData.FireType.Press);
        skillTriggers.Add(baseSkillData.fireType, baseSkillData);
    }

    void Update()
    {

        foreach (SkillData skillData in skillTriggers.Values)
            skillData.Update(Time.deltaTime * GameStateManager.instance.gameTime);
    }

    public void LoadSkills(Skill[] skillsToLoad, SkillData.FireType[] fireTypes)
    {
        CleanSkills();

        for (int i = 0; i < skillsToLoad.Length; i++)
        {
            if (skillTriggers.ContainsKey(fireTypes[i]))
                Debug.LogError("duplicate fire type");
            else if (skillsToLoad[i] != null)
                skillTriggers.Add(fireTypes[i], skillsToLoad[i].CreateSkillData(character, fireTypes[i]));
        }
    }

    void CleanSkills()
    {
        List<SkillData> toRemove = skillTriggers.Values.ToList<SkillData>();
        toRemove.Remove(baseSkillData);

        foreach (SkillData removed in toRemove)
            skillTriggers.Remove(removed.fireType);
    }

    public void HandleInput(SkillData.FireType fireType, bool finishedInput = true)
    {

        if (skillInUse != null && fireType == SkillData.FireType.UseCharged && skillInUse.charging)
        {
            StartCoroutine(UseSkill(skillInUse));
            return;
        }
        else if (skillInUse != null || !skillTriggers.ContainsKey(fireType))
            return;

        skillInUse = skillTriggers[fireType];

        if (skillInUse.skill.canCharge && !finishedInput && !skillInUse.charging)
            StartCoroutine(ChargeSkill(skillInUse));
        else if (!skillInUse.charging)
            StartCoroutine(UseSkill(skillTriggers[fireType]));
    }

    
    IEnumerator ChargeSkill(SkillData chargingSkill)
    {
        Debug.Log("charging skill: " + chargingSkill.skill.skillName);
        skillInUse = chargingSkill;
        
        chargingSkill.SetupCharging(character);

        while (chargingSkill.charging && skillInUse == chargingSkill)
        {
            chargingSkill.charge += Time.deltaTime * GameStateManager.instance.gameTime;
            yield return null;
        }

        chargingSkill.FinishCharging();
    }

    IEnumerator UseSkill(SkillData usingSkill)
    {
        Debug.Log("using skill: " + usingSkill.skill.skillName);
        skillInUse = usingSkill;

        usingSkill.charging = false;
        character.PlayAnimation(usingSkill.skill.useAnimation);

        yield return new WaitForSeconds(usingSkill.skill.useDelay);

        usingSkill.skill.UseSkill(character, usingSkill.charge);

        usingSkill.charge = 0f;

        skillInUse = null;
    }
	
}
