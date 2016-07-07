using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BackgroundTouchHandler : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public Faction faction;
    public LayerMask passOnLayer;

    public void OnPointerDown(PointerEventData eventData)
    {
        TouchManager.instance.HandlePointerDown(gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        TouchManager.instance.HandleOnDrag(gameObject, eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GridLocationTouchHandler gridHandler = CheckSendThrough(eventData);

        if (!gridHandler)
            TouchManager.instance.HandleOnPointerUp(gameObject, eventData);
        else
            gridHandler.OnPointerUp(eventData);
    }

    GridLocationTouchHandler CheckSendThrough(PointerEventData eventData)
    {
        if (faction != CharacterManager.instance.GetControllingFaction())
            return null;

        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, passOnLayer))
        {
            Debug.Log("hit grid loc: " + hit.transform.gameObject.name);
            return hit.transform.GetComponent<GridLocationTouchHandler>();
        }
        else
            return null;
    }

}
