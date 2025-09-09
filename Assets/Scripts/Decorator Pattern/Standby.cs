using UnityEngine;

public class Standby : PlayerDecorator
{
    public Standby(Player player, float duration, Color color) : base(player, duration, color) { }

    public override void Modify(Transform transform, float scaleSpeed, Vector3 originalScale)
    {
        transform.localScale = transform.localScale;
    }
}


