using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MoveTouchHandler : TouchHandler
{

    private int pointerId;
    private Vector2 originalPosition;
    private float countdown;

    public MoveTouchHandler() { }

    public override void HandleDrag(GameObject focus, PointerEventData eventData)
    {
        if (!dragging && SufficientDistance(eventData.position, eventData.pressPosition))
        {
            dragging = true;
            SetupJoystick(eventData.pointerId, eventData.pressPosition);
            TouchManager.instance.StartCoroutine(TouchManager.instance.HandleJoystick(this));
        }
    }

    public override void ReleasePress(GameObject focus, PointerEventData eventData)
    {
        if (!dragging)
        {

            if (focus.GetComponent<GridLocation>() != null)
            {
                HandlePressMove(focus.GetComponent<GridLocation>());
                SoundManager.instance.PlaySound(SoundConfig.instance.move);
            }
           
        }

        dragging = false;
        currentFocus = null;
    }

    void SetupJoystick(int pointerId, Vector2 originalPosition)
    {
        this.pointerId = pointerId;
        this.originalPosition = originalPosition;
        countdown = 0f;
    }

    public override bool UseJoystick(float timeSinceLastFrame)
    {
        if (!dragging)
            return false;

        Vector2 position = DetermineTouchPosition();

        if (SufficientDistance(position, originalPosition) && countdown <= 0f)
        {
            HandleSwipeMove(position, originalPosition);
            countdown = TouchConfig.instance.moveDragTimeDelay;
        }
        else
            countdown -= timeSinceLastFrame;

        return true;
    }

    Vector2 DetermineTouchPosition()
    {
        if (Application.platform == RuntimePlatform.Android)
            return Input.GetTouch(pointerId).position;
        else
            return Input.mousePosition;
    }

    void HandleSwipeMove(Vector2 position, Vector2 originalPosition)
    {
        Vector2 direction = (position - originalPosition).normalized;

        direction = RemoveDiagonal(direction);
        direction.y = -1f * direction.y;

        GridLocation target = Grid.instance.GetGridLocation(direction + CharacterManager.instance.player.currentLocation.Loc);

        CharacterManager.instance.player.MoveToLocation(target);
    }

    void HandlePressMove(GridLocation loc)
    {
        if (loc == CharacterManager.instance.player.currentLocation)
            return;

        Vector2 direction = (loc.Loc - CharacterManager.instance.player.currentLocation.Loc).normalized;

        direction = RemoveDiagonal(direction);

        GridLocation target = Grid.instance.GetGridLocation(direction + CharacterManager.instance.player.currentLocation.Loc);

        CharacterManager.instance.player.MoveToLocation(target);
    }

    Vector2 RemoveDiagonal(Vector2 direction)
    {
        if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
            return new Vector2(0f, Mathf.Sign(direction.y));
        else
            return new Vector2(Mathf.Sign(direction.x), 0f);
    }
}
