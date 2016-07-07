using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterStatusHandler))]
public class Character : MonoBehaviour 
{
	public enum Facing {Left, Right}

	public Faction faction;
	public Facing facing;
	public Skill currentSkill;
	public int health;
   
    public ShieldSkillData shield = null;
	public SpriteMovement sMovement {get; private set;}
	public GridLocation currentLocation {get; private set;}
    public int maxHealth{ get; private set;}

    private Animator animator;

	void Awake()
	{
		sMovement = GetComponentInChildren<SpriteMovement>();
		CharacterManager.instance.GetTracker(faction).AddCharacter(this);
        animator = GetComponentInChildren<Animator>();
        maxHealth = health;
	}

    void Start()
    {
        CharacterManager.instance.AddCharacter(this);
    }

    void OnDestroy()
    {
        if (CharacterManager.instance != null)
            CharacterManager.instance.RemoveCharacter(this);
    }

	public Faction.Alleigance DetermineAllegiance(Faction targeter)
	{
		if (faction == targeter)
			return Faction.Alleigance.Friendly;
		else
			return Faction.Alleigance.Enemy;
	}

	public void MoveToLocation(GridLocation location)
	{
		if (location == null || location.faction != faction)
			return;

		if (currentLocation != null)
			currentLocation.CharacterLeave();

		currentLocation = location;
		currentLocation.CharacterEnter(this);
		transform.position = currentLocation.transform.position;

		if (CharacterManager.instance.player == this)
			SoundManager.instance.PlaySound(SoundConfig.instance.move);

        if (animator != null)
            animator.SetTrigger("Move");

	}

	public void TakeDamage(int damage)
	{
        if (shield != null && shield.TakeHit(damage))
            return;

		health -= damage;

		SoundManager.instance.PlaySound(SoundConfig.instance.tookDamage);

        if (health <= 0)
            DestroyCharacter();
	}

    public virtual void DestroyCharacter()
    {
        StartCoroutine(DestroyAtEndOfFrame());
    }

    IEnumerator DestroyAtEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }


	public void UseSkill()
	{
		currentSkill.UseSkill(this);
	}

	public virtual void UseSkill(Skill skillToUse)
	{
		skillToUse.UseSkill(this);
	}


	void OnDisable()
	{
		Debug.Log("Character destroyed");

		if (currentLocation != null)
			currentLocation.CharacterLeave();

		CharacterManager.instance.GetTracker(faction).RemoveCharacter(this);
	}

    public void PlayAnimation(string animation)
    {
        if (!string.IsNullOrEmpty(animation) && animator != null)
        {
            Debug.Log("playing animation: " + animation);
            animator.SetTrigger(animation);
        }
    }

    public Vector2 GetForwardDirection()
    {
        return facing == Facing.Right ? Vector2.right : Vector2.left;
    }

}
