using UnityEngine;

public class Miniaturization : PlayerDecorator
{
    public Miniaturization(Player player, float duration, Color color) : base(player, duration, color) { }

    public override void Modify(Transform transform, float scaleSpeed, Vector3 originalScale)
    {
        float scaleFactor = scaleSpeed * Time.deltaTime;
        transform.localScale = new Vector3(originalScale.x - scaleFactor, originalScale.y - scaleFactor, originalScale.z - scaleFactor);
    }
}
