using UnityEngine;

[CreateAssetMenu(menuName = "SO/Data/CelestrialBodiesDatabase")]
public class CelestialBodiesData : ScriptableObject
{
    public SpaceObjects[] spaceObjects;

    public SpaceObjects FindObjectInDatabase(int id)
    {
        foreach (SpaceObjects spaceObj in spaceObjects)
        {
            if (spaceObj.objID == id)
            {
                return spaceObj;
            }
        }
        return null;
    }
}

[System.Serializable]
public class SpaceObjects
{
    public string name;
    public int objID;
    public int objAmount;
    public ObjectType objType;

    [System.Serializable]
    public enum ObjectType
    {
        Orb,
        Ring,
        Asteroid,
        Planet,
        BlackHole,
        SkillUsed,
        LvlSkill
    }
}
