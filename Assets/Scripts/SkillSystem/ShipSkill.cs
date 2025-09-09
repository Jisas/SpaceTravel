using UnityEngine;

public abstract class ShipSkill
{
    public abstract void SetUp(ShipController controller);
    public abstract void Execute(ShipController controller);
}
