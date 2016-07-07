using UnityEngine;
using System.Collections;

public class StatusConfig : MonoBehaviour
{

    public static StatusConfig instance;

    public StatusEffectMatch[] statusEffects;
    public float statusTickRate;

    void Awake()
    {
        instance = this;
    }

    public GameObject CreateEffect(Character character, StatusData statusData)
    {
        foreach (StatusEffectMatch match in statusEffects)
        {
            if (match.type == statusData.type)
            {
                GameObject effect = (GameObject) Instantiate(match.effect, character.transform.position, match.effect.transform.rotation);
                effect.AddComponent<StatusEffectTracker>().Init(character);
                return effect;
            }
        }

        return null;
    }

	
}


[System.Serializable]
public class StatusEffectMatch
{
    public StatusData.Type type;
    public GameObject effect;
}