using UnityEngine;
using System.Collections;

public class TouchConfig : MonoBehaviour 
{

	public static TouchConfig instance;

	public float moveDragTimeDelay;
	public float dragSensitivity;
    public float angleCutoff = 0.4f;
    public float timeUntilHeldDown = 1f;

	void Awake()
	{
		instance = this;
	}
}
