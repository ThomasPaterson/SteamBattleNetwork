using UnityEngine;
using System.Collections;

public class ShieldSkillData : SkillData
{

    private GameObject shield;
    private Character target;
    private Vector3 maxSize;
    private float currentHealth;


    public ShieldSkillData(ShieldSkill skill, FireType fireType)
    {
        this.skill = skill;
        this.fireType = fireType;

        currentHealth = skill.maxHealth;
        shield = MonoBehaviour.Instantiate(skill.shieldObject) as GameObject;
        maxSize = shield.transform.localScale;
        shield.SetActive(false);
    }

    public override void Update(float timePassed)
    {
        if (charging)
        {
            shield.transform.position = target.GetComponentInChildren<SpriteRenderer>().transform.position;
            currentHealth = Mathf.Max(0f, currentHealth - timePassed * ((ShieldSkill)skill).degradeRate);
            ShowCurrentHealth();
        }
        else
            currentHealth = Mathf.Min(currentHealth + timePassed * ((ShieldSkill)skill).rechargeRate, ((ShieldSkill)skill).maxHealth);
        
    }

    public override void SetupCharging(Character character)
    {
        charging = true;
        this.target = character;
        target.PlayAnimation(skill.chargeAnimation);
        target.shield = this;
        shield.SetActive(true);
    }

    public override void FinishCharging()
    {
        shield.SetActive(false);
        charging = false;
        target.shield = null;
    }

    void DeactivateShield()
    {
        shield.SetActive(false);
        target.shield = null;
        SoundManager.instance.PlaySound(((ShieldSkill)skill).shieldBreak);
        Debug.Log("broke shield");
    }

    void ShowCurrentHealth()
    {
        float changePercent = (1f- currentHealth / ((ShieldSkill)skill).maxHealth) * (1f -((ShieldSkill)skill).minSizePercent);
        Vector3 newScale = maxSize * (1f - changePercent);
        shield.transform.localScale = newScale;
         
    }

    public bool TakeHit(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0f);

        if (currentHealth <= 0f)
        {
            DeactivateShield();
            return false;
        }
        else
        {
            SoundManager.instance.PlaySound(((ShieldSkill)skill).shieldHit);
            return true;
        }
            



    }

}
