using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

abstract public class TouchHandler
{

    protected GameObject currentFocus = null;
    protected bool dragging = false;

    public virtual void StartPress(GameObject focus)
    {
        if (currentFocus == null)
        {
            currentFocus = focus;
            dragging = false;
        }
    }

    public abstract void HandleDrag(GameObject focus, PointerEventData eventData);

    public abstract void ReleasePress(GameObject focus, PointerEventData eventData);

    public virtual bool UseJoystick(float timeSinceLastFrame) { return false; }

    public virtual void CheckHeldDown(float timeSinceLast) { }

    protected bool SufficientDistance(Vector2 position, Vector2 startPos)
    {
        return Vector2.Distance(position, startPos) >= TouchConfig.instance.dragSensitivity;
    }

    protected float ClampInput(float currentInput)
    {
        if (Mathf.Abs(currentInput) > TouchConfig.instance.angleCutoff)
            return Mathf.Ceil(Mathf.Abs(currentInput)) * Mathf.Sign(currentInput);
        else
            return 0f;
    }

    public virtual void HandlePause()
    {
        currentFocus = null;
        dragging = false;
    }
}
