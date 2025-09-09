using System.Collections.Generic;
using UnityEngine;
using MyBox;

[CreateAssetMenu(menuName = "SO/Data/ShipsDatabase")]
public class ShipsDatabase : ScriptableObject
{
    public List<Ship> ships = new();

    public Ship FindShipInDatabase(int id)
    {
        foreach (Ship ship in ships)
        {
            var shipID = ships.IndexOf(ship);
            if (shipID == id) return ship;
        }
        return null;
    }
    public Ship FindShipInUse() => ships[GameManager.Instance.GetPlayerData().shipInUseId];
}


[System.Serializable]
public class Ship
{
    [Header("GENERAL")]
    public string name;
    public int cost;
    public ShipType type = ShipType.Exploration;
    public GameObject shipPrefab;
    public bool uniqueShip;

    [Header("STATS")]
    public Stats stats;

    [Header("SKILL")]
    [ConditionalField("type", false, ShipType.Combat), Range(0.3f, 1)] public float fireRate = 0.4f;
    [ConditionalField("type", false, ShipType.Exploration)] public float evasiveSpeed = 2;
    public SkillData skillGeneralData;
}

public enum ShipType
{
    Combat,
    Exploration
}

[System.Serializable]
public struct Stats
{
    public float speed;
    public float strength;

}

[System.Serializable]
public struct SkillData
{
    public float duration;
    public float cooldown;
}