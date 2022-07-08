using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSpike : Building
{
    Building powerSpike;
    [SerializeField] private Sprite powerSpikeSprite;
    public List<Character> soliderProducts;
    [HideInInspector] public List<Tile> collidedTiles;
    [HideInInspector] public List<Tile> unBlocked;
    private bool placed;
    private bool canTakeNeighbour = true;

    private void Awake()
    {
        
    }

    private void Instance_onBuildingPlaced()
    {

        TakeUnBlockedTiles(collidedTiles, unBlocked, powerSpike.Isplaced);
        canTakeNeighbour = false;
        SetInfo(powerSpike.BuildingSprite, powerSpike.BuildingName, powerSpike.Products, powerSpike.Isplaced,powerSpike.UnblockedTiles);
    }
    private void OnDestroy()
    {
        GameEvents.Instance.OnBuildingPlaced -= Instance_onBuildingPlaced;
    }

    void Start()
    {
        GameEvents.Instance.OnBuildingPlaced += Instance_onBuildingPlaced;
        powerSpike = new Building(powerSpikeSprite, "PowerSpike", 2, 3, soliderProducts, placed,unBlocked);
        gameObject.transform.localScale = new Vector2(powerSpike.Width, powerSpike.Height);
        gameObject.transform.position = new Vector2(Grid.Instance.width / 2, Grid.Instance.height / 2);




    }

    private void Update()
    {
        if (!powerSpike.Isplaced)
        {
            if (Input.GetMouseButtonDown(1))
            {
                GameEvents.Instance.BuildingDestroyed();
                Destroy(gameObject);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            BuildControl(collidedTiles);
        }
        SetBuildingPos(gameObject, powerSpike.Width, powerSpike.Height, powerSpike.Isplaced, collidedTiles);
        if (powerSpike.Isplaced)
        {
            if (canTakeNeighbour)
            {
                GameEvents.Instance.BuildingPlaced();
               

            }
        }





    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tile"))
        {


            collidedTiles.Add(collision.gameObject.GetComponent<Tile>());


        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tile"))
        {


            collidedTiles.Remove(collision.gameObject.GetComponent<Tile>());


        }
    }


    public void BuildControl(List<Tile> collidedTiles)
    {

        int count = 0;
        for (int i = 0; i < collidedTiles.Count; i++)
        {
            if (collidedTiles[i].isBlocked == false && collidedTiles[i].isCharacterExists == false)
            {
                count++;
            }
        }
        if (count == powerSpike.Width * powerSpike.Height)
        {
            powerSpike.Isplaced = true;
        }
    }



}
