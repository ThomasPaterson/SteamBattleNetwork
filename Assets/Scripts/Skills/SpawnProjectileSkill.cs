using UnityEngine;
using System.Collections;

public class SpawnProjectileSkill : Skill 
{
	public GameObject projectile;
    public float chargeMultiplier;
    public float maxSpeed;

	public override void UseSkill (Character user)
	{
		base.UseSkill (user);
		Projectile.CreateProjectile(user, this, projectile);
	}

    public override void UseSkill(Character user, float charge)
    {
        base.UseSkill(user);

        if (canCharge)
        {
            float calcSpeed = Mathf.Min(charge * chargeMultiplier, maxSpeed);
            Projectile.CreateProjectile(user, this, projectile, calcSpeed);
        }
            
        else
            Projectile.CreateProjectile(user, this, projectile);
    }
}
