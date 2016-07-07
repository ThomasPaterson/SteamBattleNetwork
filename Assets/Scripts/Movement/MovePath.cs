using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovePath<T>
{
	public T Start {get {return this.start;}}
	public T End {get {return this.end;}}

	private List<T> path;
	private T start;
	private T end;
	private int current;

	public MovePath()
	{
		path = new List<T>();
		current = 0;
	}

	public MovePath(T start, T end)
	{
		path = new List<T>();
		path.Add(start);
		path.Add (end);
		this.start = start;
		this.end = end;
	}

	public MovePath(List<T> path)
	{
		this.path = new List<T>(path);

		if (path.Count > 0)
		{
			start = path[0];
			end = path[path.Count-1];
		}
	}

	public MovePath(T[] path)
	{
		this.path = new List<T>(path);
		
		if (path.Length > 0)
		{
			start = path[0];
			end = path[path.Length-1];
		}
	}

	public void AddNode(T location)
	{
		if (location == null)
			return;

		path.Add(location);
		end = location;

		if (start == null)
			start = location;
	}

	public List<T> GetAllNodes()
	{
		return path;
	}

	public T GetCurrent()
	{
		return path[current];
	}

	public bool HasNext()
	{
		return current < path.Count-1;
	}

	public T MoveToNext()
	{

		if (HasNext())
		{
			current++;
			return path[current];
		}
		else
		{
			Debug.LogError("Tried to go past the end");
			return End;
		}
			

	}

	public void ExpandPath(MovePath<T> extension)
	{
		path.AddRange(extension.GetAllNodes());
	}


}
