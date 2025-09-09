using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<Pool> pools;
    public Dictionary <string, Queue<GameObject>> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary <string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);

                if (obj.GetComponent<VelocityTracker>() == null && !obj.CompareTag("Projectile"))
                    obj.AddComponent<VelocityTracker>();

                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Pool with tag " + tag + " doesn't excist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        MeshRenderer objectToSpawnMesh = objectToSpawn.GetComponent<MeshRenderer>();
        SphereCollider objectToSpawnCollider = objectToSpawn.GetComponent<SphereCollider>();

        if(objectToSpawn.CompareTag("Enemy"))
        {
            objectToSpawn.SetActive(true);
            objectToSpawnMesh.enabled = true;
            objectToSpawn.transform.SetPositionAndRotation(position, rotation);   
            if (objectToSpawnCollider != null) objectToSpawnCollider.enabled = true;
        }
        else if (objectToSpawn.CompareTag("Projectile"))
        {
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.SetPositionAndRotation(position, rotation);
            var Collider = objectToSpawn.GetComponentInChildren<Collider>();
            Collider.enabled = true;
        }
        else
        {
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.SetPositionAndRotation(position, rotation);
            if(objectToSpawnCollider != null) objectToSpawnCollider.enabled = true;
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
