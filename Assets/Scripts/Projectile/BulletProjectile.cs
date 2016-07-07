using UnityEngine;
using System.Collections;

public class BulletProjectile : Projectile 
{


    protected override void DetermineDirection()
    {
        switch (targetDirection)
        {
            case Target.Forward:
                direction = Vector3.right;
                break;
            case Target.Backward:
                direction = Vector3.left;
                break;
            case Target.Up:
                direction = Vector3.forward;
                break;
            case Target.Down:
                direction = Vector3.back;
                break;
            default:
                direction = Vector3.right;
                break;

        }

        if (user.facing == Character.Facing.Left)
            direction *= -1f;
    }

}
