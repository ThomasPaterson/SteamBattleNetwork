using UnityEngine;
using System.Collections;

public class SpawnProjectileEffect : ProjectileEffect
{
    public ProjectileSpawnInfo[] projectilesToSpawn;


    public override void TriggerEffect(GridLocation loc)
    {
        foreach (ProjectileSpawnInfo spawn in projectilesToSpawn)
            Projectile.CreateProjectile(projectile.user, projectile.skillUsed, spawn.projectile, loc, spawn.makeRelative);
    }

    public override void TriggerEffect(Character character)
    {
        foreach (ProjectileSpawnInfo spawn in projectilesToSpawn)
            Projectile.CreateProjectile(projectile.user, projectile.skillUsed, spawn.projectile, character.currentLocation, spawn.makeRelative);
    }


}

[System.Serializable]
public class ProjectileSpawnInfo
{
    public GameObject projectile;
    public bool makeRelative;
}
