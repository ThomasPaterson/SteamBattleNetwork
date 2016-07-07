using UnityEngine;
using System.Collections;

public class TargetProjectile : Projectile 
{
    public GameObject target;
    public float trackRate;

    protected override void DetermineDirection()
    {

        if (target == null)
        {
            direction = Vector3.right;

            if (user.facing == Character.Facing.Left)
                direction *= -1f;
        }
        else
            direction = CalcuateDirectionToTarget();
        
    }

    protected override void HandleMovement()
    {
        direction = Track();
        GetComponentInChildren<Rigidbody>().MovePosition(speed.ModifyVector(timePassed, transform.position, direction));
    }

    Vector3 CalcuateDirectionToTarget()
    {
        Vector3 newDirection = target.transform.position - transform.position;
        newDirection.y = 0f;
        newDirection = direction.normalized;

        return newDirection;
    }

    Vector3 Track()
    {
        if (trackRate == 0f)
            return direction;

        Vector3 potentialDirection = CalcuateDirectionToTarget();
        potentialDirection *= Mathf.Clamp((Time.fixedDeltaTime * trackRate), 0f, 1f);
        Vector3 oldDirection = direction * Mathf.Clamp(1f - (Time.fixedDeltaTime * trackRate), 0f, 1f);

        return oldDirection + potentialDirection;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
            arrivedAtDestination = true;

        base.OnTriggerEnter(other);
    }

}
