using UnityEngine;

public class Maximization : PlayerDecorator
{
    public Maximization(Player player) : base (player, float.PositiveInfinity, Color.white) { }

    public override void Modify(Transform transform, float scaleSpeed, Vector3 originalScale)
    {
        float factorEscala = scaleSpeed * Time.deltaTime;
        transform.localScale = new Vector3(originalScale.x + factorEscala, originalScale.y + factorEscala, originalScale.z + factorEscala);
    }
}
