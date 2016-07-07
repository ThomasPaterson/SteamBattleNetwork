using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillOptionButton : MonoBehaviour
{
    public Skill assignedSkill;


    void Awake()
    {
        GetComponentInChildren<Text>().text = assignedSkill.skillName;
    }

    public void ChooseSkill()
    {
        GetComponentInParent<SkillSelectionPanel>().ChooseCurrentFireType(assignedSkill);

    }
	
}
