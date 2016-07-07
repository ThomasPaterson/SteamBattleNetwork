using UnityEngine;
using System.Collections;

public class Status
{

    public StatusData.Type type;
    public float value;
    public Color colorToTurn;

    private float startingDuration;
    private float currentDuration;
    private GameObject effect;

    public Status(StatusData data, GameObject effect)
    {
        this.type = data.type;
        this.value = data.value;
        this.startingDuration = data.duration;
        this.currentDuration = data.duration;
        this.effect = effect;
    }

    public bool Process(Character character)
    {
        currentDuration -= 1;
        HandleEffect(character);
        return currentDuration <= 0f;
    }

    public void HandleEffect(Character character)
    {
        switch (type)
        {
            case StatusData.Type.Fire:
                character.TakeDamage((int)value);
                break;
            default:
                break;
        }
    }

    public void Cleanup()
    {
        if (effect != null)
            GameObject.Destroy(effect);
    }

    public float GetPercentDone()
    {
        return currentDuration / startingDuration;
    }



}
