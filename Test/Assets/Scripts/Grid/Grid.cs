using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid Instance;
    [SerializeField] public int width, height; //tilemap uzunlugu icin x ekseninde ve y ekseninde spawnlanacak tile

    [SerializeField] private Tile tilePrefab;//spawnlacanak tile prefabi

    [SerializeField] private GameObject tileMap;//olusan butun tiller'i bir GameObjenin icinde tutmak icin


    
    public Dictionary<Vector2, Tile> tiles;
    public List<Vector2> tilePos;
    
    
    



    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
       

    }
    void Start()
    {
        GenerateGrid();
    }



    void GenerateGrid()//Grid'in olusturuldugu ve olusan Tile'a atamalarin yapildigi kisim
    {
        tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

               

                spawnedTile.tilePos = new Vector3(x, y,0);
                spawnedTile.positionOnX = x;
                spawnedTile.positionOnY = y;
                TileMap.Instance.allTiles.Add(spawnedTile);
                TileMap.Instance.allTileObjects.Add(spawnedTile.gameObject);
                TileMap.Instance.allTilesPos.Add(spawnedTile.tilePos);
                spawnedTile.transform.SetParent(tileMap.transform);
                
                 
                tiles[new Vector2(x, y)] = spawnedTile;
                
            }
        }
        GameEvents.Instance.TilingComplete();//Tiling islemi bittikten sonra cagrilan event


        
    }

   

 

  
}
