using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillAssignedButton : MonoBehaviour
{
    public Text fireText;
    public Text skillText;
    public SkillData.FireType fireType;
    public Skill currentSkill { get; private set; }
    public bool startSelected = false;
    public GameObject selectionBox;

    void OnEnable()
    {
        AssignSkill(null);

        if (startSelected)
            Select();
        else
            Deselect();
    }


    void Awake()
    {
        fireText.text = fireType.ToString();
        skillText.text = "";
    }


    public void AssignSkill(Skill skillToAssign)
    {
        skillText.text = skillToAssign == null ? "" : skillToAssign.skillName;
        currentSkill = skillToAssign;

    }

    public void Select()
    {
        selectionBox.SetActive(true);
        GetComponentInParent<SkillSelectionPanel>().SelectFireType(this);
    }

    public void Deselect()
    {
        selectionBox.SetActive(false);

    }
}
