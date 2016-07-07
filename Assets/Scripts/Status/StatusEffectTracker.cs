using UnityEngine;
using System.Collections;

public class StatusEffectTracker : MonoBehaviour
{
    private Character character;

    public void Init(Character character)
    {
        this.character = character;
    }

    void Update()
    {

        if (character == null)
            Destroy(gameObject);
        else
            transform.position = character.transform.position;
    }
	
}
