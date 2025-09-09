using UnityEngine;

public class EvasiveManeuver : ShipSkill
{
    public override void SetUp(ShipController controller) {}
    public override void Execute(ShipController controller)
    {
        var currentObject = controller.gameObject;
        var targetRotation = Quaternion.Euler(0, 0, -90);

        currentObject.transform.localRotation = Quaternion.Slerp(controller.transform.localRotation, targetRotation, Time.deltaTime * 10);
    }
}
