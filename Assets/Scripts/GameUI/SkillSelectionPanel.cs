using UnityEngine;
using System.Collections;


public class SkillSelectionPanel : MonoBehaviour
{
    private SkillAssignedButton currentSelection = null;

    void OnEnable()
    {
        GameStateManager.instance.PauseGame(true);
    }

    public void FinishSetup()
    {
        SetupPlayerSkills();
        GameStateManager.instance.PauseGame(false);
        gameObject.SetActive(false);
    }

    void SetupPlayerSkills()
    {
        if (CharacterManager.instance.player == null)
            return;

        SkillAssignedButton[] assignedButtons = GetComponentsInChildren<SkillAssignedButton>();
        Skill[] skills = new Skill[assignedButtons.Length];
        SkillData.FireType[]  fireTypes = new SkillData.FireType[assignedButtons.Length];

        for (int i = 0; i < assignedButtons.Length; i++)
        {
            skills[i] = assignedButtons[i].currentSkill;
            fireTypes[i] = assignedButtons[i].fireType;

            if (skills[i] != null)
                Debug.Log("assigning skill: " + skills[i].skillName + " to: " + fireTypes[i].ToString());
        }

        CharacterManager.instance.player.GetComponent<SkillManager>().LoadSkills(skills, fireTypes);

    }


    public void ChooseCurrentFireType(Skill skillToAssign)
    {
        if (currentSelection != null)
            currentSelection.AssignSkill(skillToAssign);

    }

    public void SelectFireType(SkillAssignedButton selectedButton)
    {
        if (currentSelection != null && currentSelection != selectedButton)
            currentSelection.Deselect();

        currentSelection = selectedButton;

    }

}
