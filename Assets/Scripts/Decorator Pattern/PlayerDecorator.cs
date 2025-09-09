using UnityEngine;

public abstract class PlayerDecorator : Player
{
    protected Player player;

    public PlayerDecorator(Player player, float duration, Color color)
    {
        this.player = player;
        Duration = duration;
        ModifierColor = color;
    }

    public override void Modify(Transform transform, float scaleSpeed, Vector3 originalScale)
    {
        player.Modify(transform, scaleSpeed, originalScale);
    }
}
