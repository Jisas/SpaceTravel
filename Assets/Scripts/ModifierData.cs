using UnityEngine;

public class ModifierData : MonoBehaviour
{
    [SerializeField] private float duration = 1.0f;
    [SerializeField] private Color color;

    public float Duration { get => duration; }
    public Color ModifierColor { get => color; }
}