using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private Sprite buildingSprite;
    private string buildingName;
    private int width;
    private int height;
    private List<Character> products;
    private List<Tile> unBlockedTiles;
    private Tile placedTile;
    private bool isPlaced;

    #region get set
    public Sprite BuildingSprite
    {
        get
        {
            return buildingSprite;
        }
        set
        {
            buildingSprite = value;
        }
    }
    public string BuildingName
    {
        get
        {
            return buildingName;
        }
        set
        {
            buildingName = value;
        }
    }
    public int Width
    {
        get
        {
            return width;
        }
        set
        {
            width = value;
        }
    }
    public int Height
    {
        get
        {
            return height;
        }
        set
        {
            height = value;
        }
    }
    public List<Character> Products
    {
        get
        {
            return products;
        }
        set
        {
            products = value;
        }
    }
    public Tile PlacedTile
    {
        get
        {
            return placedTile;
        }
        set
        {
            placedTile = value;
        }
    }
    public bool Isplaced
    {
        get
        {
            return isPlaced;
        }
        set
        {
            isPlaced = value;
        }
    }
    public List<Tile> UnblockedTiles
    {
        get
        {
            return unBlockedTiles;
        }
        set
        {
            unBlockedTiles = value;
        }
    }
    #endregion

    public Building() { }

    public Building(Sprite buildingSprite, string buildingName, int width, int height, List<Character> products,bool isPlaced,List<Tile>unBlockedTiles)
    {
        BuildingSprite = buildingSprite;
        BuildingName = buildingName;
        Width = width;
        Height = height;
        Products = products;
        Isplaced = isPlaced;
        UnblockedTiles = unBlockedTiles;
    }
    public void SetInfo(Sprite image, string name,List<Character> produts,bool isPlaced,List<Tile>unBlockedTiles)
    {
        BuildingSprite = image;
        BuildingName = name;
        Products = produts;
        Isplaced = isPlaced;
        UnblockedTiles = unBlockedTiles;
    }
    public void SetBuildingPos(GameObject building,int buildingWidth,int buildingHeight,bool isPlaced,List<Tile> collidedTiles)//Mouse uzerinde duran tile'a gore yerlestirilmemis Building'in yerini ayarlama
    {
        if (!isPlaced)
        {
            if (SelectedItem.Instance.onMouseTile != null)
            {
                building.transform.position = new Vector3(SelectedItem.Instance.onMouseTile.GetComponent<Tile>().positionOnX + buildingWidth - (buildingWidth * 0.5f + 0.5f), SelectedItem.Instance.onMouseTile.GetComponent<Tile>().positionOnY + buildingHeight - (buildingHeight * 0.5f + 0.5f),-1);
                if (SelectedItem.Instance.onMouseTile.GetComponent<Tile>().positionOnX > Grid.Instance.width / 2)
                {
                    building.transform.position = new Vector3(SelectedItem.Instance.onMouseTile.GetComponent<Tile>().positionOnX + 1 - (buildingWidth * 0.5f + 0.5f), building.transform.position.y,-1);
                }
                if (SelectedItem.Instance.onMouseTile.GetComponent<Tile>().positionOnY > Grid.Instance.height / 2)
                {
                    building.transform.position = new Vector3(building.transform.position.x, SelectedItem.Instance.onMouseTile.GetComponent<Tile>().positionOnY + 1 - (buildingHeight * 0.5f + 0.5f),-1);
                }
            }
        }
        else
        {
            for (int i = 0; i < collidedTiles.Count; i++)
            {
                collidedTiles[i].isBlocked = true;
            }
        }
        
       
  
    }
    //Yerlestirildiginde temas edilen tile'larin komsularinin block kontrolune göre Building etrafindaki bos tile'lari listeye ekleme
    public void TakeUnBlockedTiles(List<Tile> collidedTiles,List<Tile>unBlockedTiles,bool isPlaced)
    {
        if (isPlaced == true)
        {
            for (int i = 0; i < collidedTiles.Count; i++)
            {
                for (int j = 0; j < collidedTiles[i].neighbourTiles.Count; j++)
                {
                    if (collidedTiles[i].neighbourTiles[j].isBlocked == false && !unBlockedTiles.Contains(collidedTiles[i].neighbourTiles[j]))
                    {
                        unBlockedTiles.Add(collidedTiles[i].neighbourTiles[j]);
                    }
                }
            }
        }
    }
    


    
    }

