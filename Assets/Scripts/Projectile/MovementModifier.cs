using UnityEngine;

[System.Serializable]
public class MovementModifier
{
    public enum ModType { Speed, xPos, yPos, zPos, xRot, yRot, zRot}

    public ModType modType;
    public float intensity;
    public AnimationCurve curve;

    public Vector3 ModifyVector(float time, Vector3 existingVector, Vector3 direction)
    {
        switch (modType)
        {
            case ModType.Speed:
                return existingVector + direction * curve.Evaluate(time) * intensity;
            case ModType.xPos:
                return existingVector + Vector3.right * curve.Evaluate(time) * Mathf.Sign(direction.x) * intensity;
            case ModType.yPos:
                return existingVector + Vector3.up * curve.Evaluate(time) * intensity;
            case ModType.zPos:
                return existingVector + Vector3.forward * curve.Evaluate(time) * intensity;
            case ModType.xRot:
                return existingVector  + Vector3.right * curve.Evaluate(time) * intensity;
            case ModType.yRot:
                return existingVector + Vector3.up * curve.Evaluate(time) * intensity;
            case ModType.zRot:
                return existingVector + Vector3.forward * curve.Evaluate(time) * intensity;
            default:
                return existingVector;
        }
    }
	
}
