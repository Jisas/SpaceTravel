using UnityEngine;

public class MaximizationX2 : PlayerDecorator
{
    public MaximizationX2(Player player, float duration, Color color) : base(player, duration, color) { }

    public override void Modify(Transform transform, float scaleSpeed, Vector3 originalScale)
    {
        float scaleFactor = (scaleSpeed * 2) * Time.deltaTime;
        transform.localScale = new Vector3(originalScale.x + scaleFactor, originalScale.y + scaleFactor, originalScale.z + scaleFactor);
    }
}
