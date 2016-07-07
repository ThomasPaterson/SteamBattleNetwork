using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
    public enum StartLocation { User = 0, Target = 1, BackRow = 2, FrontRow = 3, EnemyBackRow = 4}
    public enum Target { Forward, Backward, Up, Down, TargetCharacter, TargetGrid}

    public MovementModifier speed;
    public Vector3 offset;
    public Vector3 projectileOffset;
    public Vector2 gridOffset;
    public MovementModifier[] moveModifiers;
    public StartLocation startlocation;
    public Target targetDirection;

    public Character user { get; protected set; }
    public Skill skillUsed { get; protected set; }

    //Trigger flags
    public bool arrivedAtDestination { get; protected set; }
    public bool removedBySkill { get; protected set; }
    public bool removedByBounds { get; protected set; }
    
	
	protected Vector3 direction;
    private ProjectileEffect[] effects;
    private bool destroyAfterCheck;
    private Vector3 spriteOffset;

    protected float timePassed = 0f;

    void Awake()
    {
        effects = GetComponentsInChildren<ProjectileEffect>();
    }

	public void Init(Character user, Skill skillUsed)
	{
		this.user = user;
		this.skillUsed = skillUsed;

		SetLocation();
	}

	void SetLocation()
	{
        spriteOffset = user.facing == Character.Facing.Right ? offset : new Vector3(offset.x * -1f, offset.y, offset.z);
		GetComponentInChildren<SpriteRenderer>().transform.localPosition = spriteOffset;
        DetermineDirection();
	}

	void FixedUpdate()
	{
        if (!GameStateManager.instance.paused)
        {
            timePassed += Time.fixedDeltaTime * GameStateManager.instance.gameTime;
            HandleMovement();
            HandleSpriteMovement();
            CheckEffects();

            if (destroyAfterCheck)
                Destroy(gameObject);
        }
	}

    protected virtual void HandleMovement()
    {
        GetComponentInChildren<Rigidbody>().MovePosition(speed.ModifyVector(timePassed, transform.position, direction));
    }

    void HandleSpriteMovement()
    {
        Vector3 currentTransform = spriteOffset;

        foreach (MovementModifier mod in moveModifiers)
            currentTransform = mod.ModifyVector(timePassed, currentTransform, direction);

        GetComponentInChildren<SpriteRenderer>().transform.localPosition = currentTransform;
    }



    protected virtual void DetermineDirection()
    {
        if (user.facing == Character.Facing.Left)
            direction = Vector3.left;
        else
            direction = Vector3.right;
    }

    void CheckEffects()
    {
        foreach (ProjectileEffect effect in effects)
            effect.CheckTriggers();
    }

    public void CheckEffects(GameObject toCheck)
    {
        foreach (ProjectileEffect effect in effects)
            effect.CheckTriggers(toCheck);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
            CheckEffects(other.gameObject); 
    }


    void OnTriggerExit(Collider other) 
	{
        if (other.gameObject.layer == LayerMask.NameToLayer("Boundary"))
        {
            removedByBounds = true;
            CheckEffects();
            Destroy(gameObject);
        }
	}



	public static void CreateProjectile(Character character, Skill skillUsed, GameObject projPrefab)
	{
        GameObject proj = Instantiate(projPrefab, GetWorldPosition(character, projPrefab), projPrefab.transform.rotation) as GameObject;
		proj.GetComponent<Projectile>().Init(character, skillUsed);
	}

    public static void CreateProjectile(Character character, Skill skillUsed, GameObject projPrefab, GridLocation loc, bool makeRelative)
    {

        Vector3 startLoc;
        if (makeRelative)
            startLoc = GetRelativeWorldPosition(character, projPrefab, loc);
        else
            startLoc = GetWorldPosition(character, projPrefab);

        GameObject proj = Instantiate(projPrefab, startLoc, projPrefab.transform.rotation) as GameObject;
        proj.GetComponent<Projectile>().Init(character, skillUsed);
    }

    public static void CreateProjectile(Character character, Skill skillUsed, GameObject projPrefab, float value)
    {
        GameObject proj = Instantiate(projPrefab, GetWorldPosition(character, projPrefab), projPrefab.transform.rotation) as GameObject;
        proj.GetComponent<Projectile>().Init(character, skillUsed);
        proj.GetComponent<Projectile>().speed.intensity += value;
    }

    static Vector3 GetWorldPosition(Character character, GameObject projPrefab)
    {
        Vector2 gridLocation = ProjectileHelper.FindStartingLocation(projPrefab.GetComponent<Projectile>().startlocation, character, projPrefab.GetComponent<Projectile>().gridOffset);
        return TransformStartGrid(character, projPrefab, gridLocation);
    }

    static Vector3 GetRelativeWorldPosition(Character character, GameObject projPrefab, GridLocation loc)
    {
        Vector2 gridLocation = ProjectileHelper.FindStartingLocation(character, projPrefab.GetComponent<Projectile>().gridOffset, loc);
        return TransformStartGrid(character, projPrefab, gridLocation);
    }

    static Vector3 TransformStartGrid(Character character, GameObject projPrefab, Vector2 gridLocation)
    {
        GridLocation startLocation = Grid.instance.GetGridLocation(gridLocation, true);
        Vector3 worldPos = startLocation.transform.position + Vector3.up * ProjectileConfig.instance.projectileHeight;
        Vector3 worldOffset = projPrefab.GetComponent<Projectile>().projectileOffset;
        worldOffset = character.facing == Character.Facing.Right ? worldOffset : new Vector3(worldOffset.x * -1f, worldOffset.y, worldOffset.z);
        return worldPos + worldOffset;
    }


    public float GetTimeAlive()
    {
        return timePassed;
    }

    public void SetDestroyFlag()
    {
        destroyAfterCheck = true;
    }

}


