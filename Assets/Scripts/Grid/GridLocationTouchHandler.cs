using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GridLocationTouchHandler : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
	private GridLocation gridLoc;

	void Awake()
	{
		gridLoc = GetComponent<GridLocation>();
	}


	public void OnPointerDown(PointerEventData eventData)
	{
        TouchManager.instance.HandlePointerDown(gridLoc.gameObject);
	}

	public void OnDrag(PointerEventData eventData)
	{
        TouchManager.instance.HandleOnDrag(gridLoc.gameObject, eventData);
    }

	public void OnPointerUp(PointerEventData eventData)
	{
        TouchManager.instance.HandleOnPointerUp(gridLoc.gameObject, eventData);
    }
}
