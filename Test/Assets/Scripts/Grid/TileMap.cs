using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public static TileMap Instance;
    public List<Vector2> allTilesPos;
    public List<GameObject> allTileObjects;
    public List<Tile> allTiles;
    
    

    private void Awake()
    {
        Instance = this;
    }








}
