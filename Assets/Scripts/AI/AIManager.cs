using UnityEngine;
using System.Collections;
using System.Linq;

public class AIManager : MonoBehaviour 
{

	public AIBehaviour[] behaviours;
	public float switchDelay = 0.5f;

	private AIBehaviour currentBehaviour = null;


	void Start()
	{
        SetupSkillData();
		StartCoroutine(HandleBehaviour());
	}

    void SetupSkillData()
    {
        foreach (AIBehaviour behaviour in behaviours)
        {
            SkillData skillData = new SkillData(behaviour.sequence[0].skillToUse);

            foreach (EntryCondition entryCondition in behaviour.entryConditions)
                entryCondition.skillData = skillData;
            
        }
    }

	IEnumerator HandleBehaviour()
	{
		while (true)
		{
            if (GameStateManager.instance.paused)
                yield return null;
            else
            {
                if (currentBehaviour == null)
                    ChooseNewBehaviour();
                else
                {
                    if (currentBehaviour.ProcessBehaviour(GetComponent<AICharacter>()))
                        currentBehaviour = null;
                }

                yield return null;    
            }
		}
	}

	void ChooseNewBehaviour()
	{
        
        foreach (AIBehaviour behaviour in behaviours)
        {
            if (behaviour.CheckEntry(GetComponent<AICharacter>()))
            {
                Debug.Log("chose new behaviour");
                currentBehaviour = behaviour;
                currentBehaviour.StartBehaviour();
                break;
            }
        }
	}

}
