using UnityEngine;
using System.Collections;

public class FlashGridEffect : ProjectileEffect
{

    public float timeToFlash;

    public override void TriggerEffect(GridLocation loc)
    {
        if (loc != null)
            loc.GetComponent<LocationAppearance>().Flash(timeToFlash);

    }

    public override void TriggerEffect(Character character)
    {
        if (character != null)
            character.currentLocation.GetComponent<LocationAppearance>().Flash(timeToFlash);
    }

}
