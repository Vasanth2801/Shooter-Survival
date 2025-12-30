using UnityEngine;
using System.Collections.Generic;

public class ObjectPoolManager : MonoBehaviour
{
    [System.Serializable]
    public class ObjectPoolItem
    {
        public string objectTag;
        public GameObject prefab;
        public int objectSize;
    }

    public List<ObjectPoolItem> pools;
    public Dictionary<string, Queue<GameObject>> poolsOfDictionary;

    void Start()
    {
        poolsOfDictionary = new Dictionary<string, Queue<GameObject>>();
        
        foreach(ObjectPoolItem item in pools)
        {
            Queue<GameObject> obj = new Queue<GameObject>();

            for(int i = 0; i < item.objectSize; i++)
            {
                GameObject objPool = Instantiate(item.prefab);
                objPool.SetActive(false);
                obj.Enqueue(objPool);
            }
            poolsOfDictionary.Add(item.objectTag, obj);
        }
    }

    public GameObject SpawnObjects(string tag, Vector2 position, Quaternion rotation)
    {
        GameObject objToSpawn = poolsOfDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        poolsOfDictionary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }
}
