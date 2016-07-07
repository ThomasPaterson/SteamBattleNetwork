using UnityEngine;
using System.Collections;

[System.Serializable]
public class StatusData
{
	public enum Type {Invincibility, Fire}

	public Type type;
	public float duration;
    public float value;

}
