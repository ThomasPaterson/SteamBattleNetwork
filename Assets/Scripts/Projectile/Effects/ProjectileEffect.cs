using UnityEngine;
using System.Collections;

public class ProjectileEffect : MonoBehaviour
{
    public ProjectileTrigger[] triggers;
    public bool destroyAfterTrigger;
    public float timeUntilActivation;

    protected Projectile projectile;

    private bool activated = false;

    void Awake()
    {
        projectile = GetComponentInChildren<Projectile>();

        if (timeUntilActivation > 0f)
            StartCoroutine(ActivateAfterDelay());
        else
            activated = true;
    }

    public virtual void TriggerEffect(GridLocation loc) { }

    public virtual void TriggerEffect(Character character) { }
    
    public virtual void TriggerEffect() { }

    IEnumerator ActivateAfterDelay()
    {

        float currentTime = timeUntilActivation;

        while (currentTime > 0f)
        {
            currentTime -= Time.deltaTime * GameStateManager.instance.gameTime;
            yield return null;
        }

        activated = true;

        CheckCurrentCollisions();
    }

    public void CheckTriggers()
    {
        if (!activated)
            return;

        if (ProjectileTrigger.CheckTriggers(triggers, projectile))
        {
            TriggerEffect();

            if (destroyAfterTrigger)
                projectile.SetDestroyFlag();
        } 
    }

    public void CheckTriggers(GameObject toCheck)
    {
        if (!activated)
            return;

        if (ProjectileTrigger.CheckTriggers(triggers, projectile, toCheck))
        {
            if (toCheck.GetComponent<GridLocation>())
                TriggerEffect(toCheck.GetComponent<GridLocation>());
            else if (toCheck.GetComponent<Character>())
                TriggerEffect(toCheck.GetComponent<Character>());
            else
                TriggerEffect();

            if (destroyAfterTrigger)
                projectile.SetDestroyFlag();
        }
        
    }


    void CheckCurrentCollisions()
    {

        foreach (Collider col in Physics.OverlapSphere(projectile.transform.position, projectile.GetComponent<Collider>().bounds.size.x, LayerConfig.instance.projectileCollision.value))
            projectile.CheckEffects(col.gameObject);
    }


}
