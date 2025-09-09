using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListObjecPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject[] prefabs;
    }

    public static ListObjecPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.prefabs.Length; i++)
            {
                GameObject obj = Instantiate(pool.prefabs[i]);
                var rb = obj.AddComponent<Rigidbody>();
                rb.useGravity = false;
                rb.isKinematic = true;
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Pool with tag " + tag + " doesn't excist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        MeshRenderer objectToSpawnMesh = objectToSpawn.GetComponent<MeshRenderer>();

        objectToSpawn.SetActive(true);
        objectToSpawnMesh.enabled = true;
        objectToSpawn.transform.SetPositionAndRotation(position, rotation);

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
