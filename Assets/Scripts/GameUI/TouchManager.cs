using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    public static TouchManager instance;

    private TouchHandler moveHandler;
    private TouchHandler actionHandler;

    void Awake()
    {
        instance = this;
        moveHandler = new MoveTouchHandler();
        actionHandler = new ActionTouchHandler();
    }

    void Update()
    {
        actionHandler.CheckHeldDown(Time.deltaTime * GameStateManager.instance.gameTime);

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.LoadLevel(0);
    }

    public IEnumerator HandleJoystick(TouchHandler toWatch)
    {
        yield return null;

        while (toWatch.UseJoystick(Time.deltaTime * GameStateManager.instance.gameTime))
            yield return null;
    }

    public void HandlePointerDown(GameObject focus)
    {
        if (CharacterManager.instance.player == null || GameStateManager.instance.paused)
            return;

        GetTouchHandler(focus).StartPress(focus);
    }

    public void HandleOnDrag(GameObject focus, PointerEventData eventData)
    {
        if (CharacterManager.instance.player == null || GameStateManager.instance.paused)
            return;

        GetTouchHandler(focus).HandleDrag(focus, eventData);
    }

    public void HandleOnPointerUp(GameObject focus, PointerEventData eventData)
    {
        if (CharacterManager.instance.player == null || GameStateManager.instance.paused)
            return;

        GetTouchHandler(focus).ReleasePress(focus, eventData);
    }

    TouchHandler GetTouchHandler(GameObject focus)
    {
        if (focus.GetComponent<GridLocation>() != null)
        {
            if (focus.GetComponent<GridLocation>().faction == CharacterManager.instance.player.faction)
                return moveHandler;
            else
                return actionHandler;
        }
        else
        {
            if (focus.GetComponent<BackgroundTouchHandler>().faction == CharacterManager.instance.player.faction)
                return moveHandler;
            else
                return actionHandler;
        }

        
    }

    public static SkillData.FireType DetermineFireType(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - eventData.pressPosition;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (eventData.position.x > eventData.pressPosition.x)
                return SkillData.FireType.SwipeForward;
            else
                return SkillData.FireType.SwipeBackward;
        }
        else
        {
            if (eventData.position.y > eventData.pressPosition.y)
                return SkillData.FireType.SwipeUp;
            else
                return SkillData.FireType.SwipeDown;
        }
        
    }

    public void HandlePause()
    {
        StopAllCoroutines();

        moveHandler.HandlePause();
        actionHandler.HandlePause();
    }

           
	
}
