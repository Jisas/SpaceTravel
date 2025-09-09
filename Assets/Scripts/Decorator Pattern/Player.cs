using UnityEngine;

public abstract class Player
{
    public float Duration { get; protected set; }
    public Color ModifierColor { get; protected set; }
    public abstract void Modify(Transform transform, float scaleSpeed, Vector3 originalScale);
}