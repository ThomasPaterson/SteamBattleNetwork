using UnityEngine;
using UnityEngine.EventSystems;

public class ActionTouchHandler : TouchHandler
{
    private bool heldDown = false;
    private float countdownToHeld = 0f;

    public ActionTouchHandler()
    {
    }


    public override void StartPress(GameObject focus)
    {
        base.StartPress(focus);

        countdownToHeld = TouchConfig.instance.timeUntilHeldDown;
    }

    public override void CheckHeldDown(float timeSinceLast)
    {
        if (dragging || heldDown || currentFocus == null)
            return ;

        countdownToHeld -= timeSinceLast;

        if (countdownToHeld <= 0)
            SetHeldDown();
    }

    void SetHeldDown()
    {
        heldDown = true;
        CharacterManager.instance.player.GetComponent<SkillManager>().HandleInput(SkillData.FireType.HeldDown, false);
    }

    public override void HandleDrag(GameObject focus, PointerEventData eventData)
    {
        if (!heldDown && !dragging && SufficientDistance(eventData.position, eventData.pressPosition))
        {
            SkillData.FireType fireType = TouchManager.DetermineFireType(eventData);
            dragging = true;
            CharacterManager.instance.player.GetComponent<SkillManager>().HandleInput(fireType, false);
        }
    }

    public override void ReleasePress(GameObject focus, PointerEventData eventData)
    {
        if (!dragging && !heldDown)
            CharacterManager.instance.player.GetComponent<SkillManager>().HandleInput(SkillData.FireType.Press, true);
        else
            CharacterManager.instance.player.GetComponent<SkillManager>().HandleInput(SkillData.FireType.UseCharged, true);


        dragging = false;
        heldDown = false;
        currentFocus = null;
    }


}
