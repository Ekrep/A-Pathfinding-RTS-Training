using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //ObjectPool
    public static ObjectPool Instance;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public List<PoolObjects> pools;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (PoolObjects pool in pools)
        {
            Queue<GameObject> objects = new Queue<GameObject>();

            for (int i = 0; i < pool.sizeOfPool; i++)
            {
                GameObject obj=Instantiate(pool.spawnObjPrefab);
                obj.SetActive(false);
                objects.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objects);
        }
    }

    public GameObject SpawnFromPool(string tag,Vector3 pos, Quaternion rot,Transform parentObj)
    {
        if (poolDictionary.ContainsKey(tag)==false)
        {
            return null;
        }
        GameObject spawningObj=poolDictionary[tag].Dequeue();
        spawningObj.SetActive(true);
        spawningObj.transform.rotation = rot;
        //spawningObj.transform.position = pos;
        spawningObj.transform.SetParent(parentObj);
       

        poolDictionary[tag].Enqueue(spawningObj);
        return spawningObj;
    }
    

    [System.Serializable]
    public class PoolObjects
    {
        public string tag;
        public GameObject spawnObjPrefab;
        public int sizeOfPool;

    }
   
}
