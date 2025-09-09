using System.Collections.Generic;
using UnityEngine;

public class CombatManeuver : ShipSkill
{
    public float fireRate; 
    private float lastFireTime = 0f;
    private List<Transform> firePoints;

    public override void SetUp(ShipController controller)
    {
        fireRate = controller.CurrentShip.fireRate;

        firePoints = new();
        var skillParent = controller.gameObject.transform.GetChild(0).Find("SkillTarget");
        if (skillParent != null && skillParent.childCount > 0) 
        { 
            for (int i = 0; i < skillParent.childCount; i++)
                firePoints.Add(skillParent.GetChild(i).transform);
        }

        controller.OnStopShoot += () => StopVFX(firePoints);
    }

    public override void Execute(ShipController controller)
    {      
        if (Time.time - lastFireTime >= fireRate)
        {
            foreach (Transform point in firePoints)
            {
                controller.Pooler.SpawnFromPool("Bullet", point.position, point.rotation);
                ParticleSystem vfx = point.GetComponentInChildren<ParticleSystem>();
                vfx.Play();
            }

            lastFireTime = Time.time;
        }
    }

    private void StopVFX(List<Transform> firePoints)
    {
        foreach (Transform point in firePoints)
        {
            ParticleSystem vfx = point.GetComponentInChildren<ParticleSystem>();
            vfx.Stop();
        }
    }
}
