using UnityEngine;
using System.Collections;

public class SpriteMovement : MonoBehaviour 
{
	public Vector3 centerOffset;
	public float speed;
	public bool moving;

	private Vector3 target;
	private MovePath<Vector3> currentMove;
	private float moveLeft;
	private float speedMod = 1f;

	public void SetLocation(Vector3 location)
	{
		transform.position = location + centerOffset;
	}

	IEnumerator MoveToPathNode ()
	{
		moving = true;

		while(moving)
		{
			moveLeft = Speed();
			MoveToTarget();
			yield return null;
		}

		Debug.Log ("finished move");
	}

	//move down path
	//instantly if move is teleport
	//finishes if it reaches the last node
	void MoveToTarget()
	{
		
		if (DistanceToTarget() < moveLeft)
			MoveToNode ();

		MoveOnPath ();
	}

	//moves to node, then recurses into moving again if it has move left
	void MoveToNode()
	{
		moveLeft -= DistanceToTarget ();
		
		transform.position = target;
		
		if (!currentMove.HasNext())
			moving = false;
		else
			target = currentMove.MoveToNext() + centerOffset;

	}
	
	void MoveOnPath()
	{
		if (moveLeft > 0) 
			transform.position = Vector3.MoveTowards(transform.position, target, moveLeft);
	}
	
	public void SetMove(MovePath<Vector3> movePath, float speedMod = 1f)
	{
		currentMove = movePath;
		target = currentMove.MoveToNext () + centerOffset;
		this.speedMod = speedMod;

		if (!moving)
			StartCoroutine (MoveToPathNode ());
	}


	
	float DistanceToTarget()
	{
		return Mathf.Abs(Vector3.Distance(transform.position, target));
	}
	
	float Speed()
	{
		return speed * Time.deltaTime * speedMod * GameStateManager.instance.gameTime;
	}
}
