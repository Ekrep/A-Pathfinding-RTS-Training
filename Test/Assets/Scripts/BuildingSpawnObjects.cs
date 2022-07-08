using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawnObjects : MonoBehaviour
{//Spawnlanabilir Buildinglerin Image'sini ve prefabini listede tutup UI'da buttona aktarmak 
    public static BuildingSpawnObjects Instance;
    public List<SpawnPrefabs> buildingSpawnPrefabs;

    private void Awake()
    {
        Instance = this;
    }
   [System.Serializable]
    public class SpawnPrefabs
    {
        public Sprite buildingSpawnImage;
        public GameObject buildingPrefab;
    }
}
