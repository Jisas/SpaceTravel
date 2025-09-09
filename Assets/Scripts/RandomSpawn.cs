using System.Collections;
using UnityEngine;
using System;

public enum RotationType
{
    InternalConst,
    Identity
}

[Serializable]
public class SpawnData
{
    public string poolTag;
    public float spawnStartTime;
    public float spawnInterval;
    public RotationType rotationType;
    public Vector2 XpositionRange;
    public Vector2 ZpositionRange;
    public bool isPlanet;
}

public class RandomSpawn : MonoBehaviour
{
    [SerializeField] private SpawnData[] spawnData;

    private const int yPos = 130;
    private ObjectPooler objectPooler;
    private ListObjecPooler listObjecPooler;

    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        listObjecPooler = ListObjecPooler.Instance;
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        foreach (var spawn in spawnData)
        {
            yield return new WaitForSeconds(spawn.spawnStartTime);
            StartCoroutine(SpawnObject(spawn));
            yield return new WaitForSeconds(spawn.spawnInterval);
        }
    }

    private IEnumerator SpawnObject(SpawnData spawn)
    {
        Vector3 spawnPosition = Position(spawn.XpositionRange.x, spawn.XpositionRange.y, spawn.ZpositionRange.x, spawn.ZpositionRange.y);
        Quaternion spawnRotation = spawn.rotationType == RotationType.Identity ? Quaternion.identity : Rotation();

        if(!spawn.isPlanet) objectPooler.SpawnFromPool(spawn.poolTag, spawnPosition, spawnRotation);
        else
        {
            var randomTemp = UnityEngine.Random.Range(0, 2);

            spawnPosition = randomTemp == 0 ? 
                Position(spawn.XpositionRange.x, spawn.XpositionRange.y, spawn.ZpositionRange.x, spawn.ZpositionRange.y) :
                Position(-spawn.XpositionRange.x, -spawn.XpositionRange.y, spawn.ZpositionRange.x, spawn.ZpositionRange.y);

            listObjecPooler.SpawnFromPool(spawn.poolTag, spawnPosition, spawnRotation);
        }

        // Espera durante el intervalo antes de volver a llamar a SpawnObject
        yield return new WaitForSeconds(spawn.spawnInterval);
        StartCoroutine(SpawnObject(spawn));
    }

    //POSITIONS & ROTATIONS
    public Quaternion Rotation() => Quaternion.Euler(0, 0, -90);
    public Vector3 Position(float minX, float maxX, float minZ, float maxZ)
    {
        var posXGeneration = UnityEngine.Random.Range(minX, maxX);
        var posZGeneration = UnityEngine.Random.Range(minZ, maxZ);
        var spawnPosition = new Vector3(posXGeneration, yPos, posZGeneration);

        return spawnPosition;
    }
}
