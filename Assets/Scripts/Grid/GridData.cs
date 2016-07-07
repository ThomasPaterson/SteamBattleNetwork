using UnityEngine;
using System.Collections;

public class GridData : ScriptableObject 
{

	public int width;
	public int height;

	public int playerWidth;
	public Faction playerFaction;
	public Faction enemyFaction;


	public Faction GetGridLocationFaction(int x, int y)
	{
		if (x < playerWidth)
			return playerFaction;
		else
			return enemyFaction;
	}


}

