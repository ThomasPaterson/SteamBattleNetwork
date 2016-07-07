using UnityEngine;
using System.Collections;

public class LocationAppearance : MonoBehaviour 
{
    private Faction faction;
    private bool flashing = false;
    private float flashCountdown = 0f;

	public void SetFaction(Faction faction)
	{
        this.faction = faction;
		GetComponent<SpriteRenderer>().sprite = faction.gridLocationSprite;
	}

	public void SetOccupied(Character character)
	{

	}

    public void Flash()
    {
        Flash(GridLocationConfig.instance.timeToFlashDamage);
        
    }

    public void Flash(float timeToFlash)
    {
        flashCountdown = Mathf.Max(flashCountdown, timeToFlash);

        if (!flashing)
            StartCoroutine(HandleFlash());
    }

    IEnumerator HandleFlash()
    {
        flashing = true;

        while (flashCountdown > 0f)
        {
            if (Random.value > 0.5f)
                GetComponent<SpriteRenderer>().sprite = faction.gridLocationSprite;
            else
                GetComponent<SpriteRenderer>().sprite = GridLocationConfig.instance.damagedGridSprite;

            flashCountdown -= Time.deltaTime * GameStateManager.instance.gameTime;

            yield return null;
        }

        GetComponent<SpriteRenderer>().sprite = faction.gridLocationSprite;
        flashing = false;
    }
}
