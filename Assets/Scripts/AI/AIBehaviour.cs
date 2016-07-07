using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AIBehaviour  
{
    public EntryCondition[] entryConditions;
    public AIAction[] sequence;

    private int currentSequence;
    private float timeOnSequence;

    public bool ProcessBehaviour(AICharacter character)
    {
        Debug.Log("processing behaviour");
        if (Finished())
            return true;
        else
            HandleAction(character);

        return false;
    }

    public bool CheckEntry(AICharacter character)
    {

        foreach (EntryCondition condition in entryConditions)
            if (!condition.CheckCondition(character, character.GetTarget(condition.targetType)))
                return false;

        return true;
    }

    void HandleAction(AICharacter character)
    {
        timeOnSequence += GameStateManager.instance.gameTime * Time.deltaTime;
        Debug.Log("handling action");

        if (sequence[currentSequence].ProcessAction(character, timeOnSequence))
            ChangeSequence();
        
    }

    public void StartBehaviour()
    {
        timeOnSequence = 0f;
        currentSequence = 0;
        sequence[currentSequence].Setup();
    }

    void ChangeSequence()
    {
        currentSequence++;
        timeOnSequence = 0f;

        if (!Finished())
            sequence[currentSequence].Setup();
    }

    bool Finished()
    {
        return currentSequence >= sequence.Length;
    }



}
