using UnityEngine;
using System.Collections;

public class PCControls : MonoBehaviour 
{

	void Awake () 
	{
#if !UNITY_EDITOR
		Destroy (gameObject);
#endif
	}
	

	void Update () 
	{
        if (GameStateManager.instance.paused)
            return;

		if (Input.GetKeyDown(KeyCode.W))
			MoveCharacter(Vector3.down);

		if (Input.GetKeyDown(KeyCode.A))
			MoveCharacter(Vector3.left);

		if (Input.GetKeyDown(KeyCode.S))
			MoveCharacter(Vector3.up);

		if (Input.GetKeyDown(KeyCode.D))
			MoveCharacter(Vector3.right);
	}

	void MoveCharacter(Vector3 direction)
	{

		Vector2 dir = new Vector2(direction.x, direction.y);

		GridLocation target = Grid.instance.GetGridLocation(dir + CharacterManager.instance.player.currentLocation.Loc);

		if (target != null)
			CharacterManager.instance.player.MoveToLocation(target);
	}
}
