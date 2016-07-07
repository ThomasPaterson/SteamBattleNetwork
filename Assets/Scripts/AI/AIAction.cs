using UnityEngine;
using System.Collections;

[System.Serializable]
public class AIAction 
{
    public float entryDelay;
    public string entryAnim;
    public float leaveDelay;
    public string leaveAnim;
    public int numToRepeat = 1;
    public Skill skillToUse;

    private bool usedEntryAnimation;
    private bool usedLeaveAnimation;
    private bool usedAction;


    public bool ProcessAction(AICharacter character, float timePassed)
    {
        Debug.Log("handling action: " + skillToUse.name);
        if (!usedEntryAnimation)
        {
            character.PlayAnimation(entryAnim);
            usedEntryAnimation = true;
        }

        if (!usedAction && timePassed > entryDelay)
        {
            character.UseSkill(skillToUse);
            usedAction = true;
        }

        if (!usedLeaveAnimation && usedAction && timePassed > entryDelay)
        {
            character.PlayAnimation(leaveAnim);
            usedLeaveAnimation = true;
        }

        return timePassed > (entryDelay + leaveDelay) && usedAction;
    }

    public void Setup()
    {
        usedEntryAnimation = false;
        usedLeaveAnimation = false;
        usedAction = false;
    }

}
