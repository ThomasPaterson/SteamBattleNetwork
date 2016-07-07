using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Grid : MonoBehaviour 
{
	private static readonly Vector3[] Directions = { Vector3.left, Vector3.up, Vector3.right, Vector3.down };

	public enum RelativeDirection {Towards, Away}

	public static Grid instance;

	public int width;
	public int height;
	public float gridXGap;
	public float gridZGap;
	public GridLocation[,] gridLocations;
	public GridData gridData;

	private BoxCollider boundary;

	IEnumerator Start()
	{
		ConstructGrid();

		yield return null;

		SetupBoundary();

	}

	public void ConstructGrid()
	{
		this.width = gridData.width;
		this.height = gridData.height;
		gridLocations = new GridLocation[width,height];
		
		
		for (int x = 0; x < width; x++)
			for (int y = 0; y < height; y++)
				AddGridLocation(x, y, gridData.GetGridLocationFaction(x,y));

		instance = this;
	}


	void AddGridLocation(int x, int y, Faction faction)
	{
		GameObject loc = (GameObject) Instantiate(GridLocationConfig.instance.gridLocationPrefab, CalculateLocation(x, y), GridLocationConfig.instance.gridLocationPrefab.transform.rotation);
		gridLocations[x, y] = loc.GetComponent<GridLocation>();

		loc.transform.parent = transform;
		loc.GetComponent<GridLocation>().Init(x, y, faction);

	}

	Vector3 CalculateLocation(int x, int y)
	{
		Vector3 location = transform.position;
		float xChange = x - (width/2f);
		float yChange = y - (height/2f);
		location += new Vector3(xChange * gridXGap, 0f, yChange * gridZGap);

		return location;
	}

	public GridLocation GetGridLocation(int x, int y, bool enforceInGrid = false)
	{
        if (x >= 0 && x < width && y >= 0 && y < height)
            return gridLocations[x, y];
        else if (enforceInGrid)
            return gridLocations[Mathf.Clamp(x, 0, width - 1), Mathf.Clamp(y, 0, height - 1)];
        else
            return null;
	}

	public GridLocation GetGridLocation(Vector2 vectorLoc, bool enforceInGrid = false)
	{
		return GetGridLocation((int)vectorLoc.x, (int)vectorLoc.y, enforceInGrid);
	}

	public GridLocation[] GetAllGridLocations()
	{
		GridLocation[] locations = new GridLocation[width * height];

		for (int x = 0; x < width; x++)
			for (int y = 0; y < height; y++)
				locations[x % width + y * width] = GetGridLocation(x, y);

		return locations;
	}

    public GridLocation GetRandomEmpty(Faction factionToTarget)
    {
        List<GridLocation> locations = GetAllGridLocations().ToList();
        GridLocation result = null;

        while (locations.Count > 0 && result == null)
        {
            GridLocation random = locations[Mathf.FloorToInt(Random.Range(0f, locations.Count))];

            if (random.Occupier == null && random.faction == factionToTarget)
                result = random;
            else
                locations.Remove(result);
        }

        return result;
    }



	//avoids memory assignent, for quick gridsearch
	public void GetAdjacent(GridLocation target, GridLocation[] adjacent)
	{
		if (target == null)
			return;
		
		for (int i = 0; i < Directions.Length; i++)
			adjacent[i] = GetGridLocation((int)(target.Loc.x + Directions[i].x), (int)(target.Loc.y + Directions[i].y));
	}

	void SetupBoundary()
	{
		boundary = gameObject.AddComponent<BoxCollider>();
		
		Bounds newBounds = boundary.bounds;
		
		Transform lowest = gridLocations[0, 0].transform;
		Transform highest = gridLocations[width-1, height-1].transform;
		
		newBounds.Encapsulate(lowest.position - new Vector3(2f * lowest.GetComponent<BoxCollider>().size.x, 0f, -3f * lowest.GetComponent<BoxCollider>().size.z));
		newBounds.Encapsulate(highest.position + new Vector3(2f * highest.GetComponent<BoxCollider>().size.x, 0f,-2f * highest.GetComponent<BoxCollider>().size.z));
		newBounds.Encapsulate(Vector3.up);
		
		boundary.center = newBounds.center;
		boundary.size = newBounds.size;
		boundary.gameObject.layer = LayerMask.NameToLayer("Boundary");
		boundary.isTrigger = true;
	}

}
