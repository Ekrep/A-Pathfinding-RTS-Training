using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameObject character;//Temasa girdigi karakter icin referans
    public Tile parentNode;
    [SerializeField] private Color baseColor, offsetColor;//Bloklu tile ve normal tile icin renk atamasi
    [SerializeField] public SpriteRenderer tileRenderer;//Renk atamalari icin rederer atamasi
    [SerializeField] private GameObject highlight;//Mouse tile uzerindeyken belli olmasi icin child transparan GameObje
    public List<Tile> neighbourTiles;//Komsu tile listesi
    public int positionOnX;//X eksenindeki pozisyonu
    public int positionOnY;//Y eksenindeki pozisyonu
    public Vector3 tilePos;//Vector icindeki pozisyonu
    public bool isBlocked=true;//Bloklu olup olmadiginin kontrolu
    public bool isCharacterExists;//Tile icinde karakter iceriyor mu
    

  
    public int gValue;//ilk Tile ile mevcut Tile maliyeti
    public int hValue;//sezgisel maliyet(hedef tile'a)


    private void Awake()
    {
        GameEvents.Instance.OnTilingComplete += Instance_onTilingComplete;
    }

    private void Instance_onTilingComplete()
    {
        SetNeighbourTiles();//Tiling bitme eventi geldiginde komsu tile'lari listeye aktarma
    }
    private void OnDisable()
    {
        GameEvents.Instance.OnTilingComplete -= Instance_onTilingComplete;
    }

    public int fValue//toplam yol fonksiyonu
    {
        get
        {
            return gValue + hValue;
        }
    }
   

    private void Start()
    {
        
    }
    private void Update()
    {
        if (isBlocked == false)//Bloklu degilse sag tiklandiginda hedef tile olarak secme
        {
           

            if (Input.GetMouseButtonDown(1))
            {
                SelectedItem.Instance.targetArea = SelectedItem.Instance.onMouseTile;
               
            }
        }
       
        if (isBlocked)//Tile durumuna gore renk degistirme
        {
            tileRenderer.color = offsetColor;
            highlight.SetActive(false);
            
        }
        else
        {
            tileRenderer.color =baseColor;
           
        }
       
    }
   

    void OnMouseEnter()//Mouse UI uzerinde degilse ve bloklu degilse uzerinde oldugu tile'i bir refensta tutma
    {
        if (isBlocked==false&&!IsMouseOverUI())
        {
            highlight.SetActive(true);
            SelectedItem.Instance.onMouseTile = gameObject;
        }
        
        
    }

    void OnMouseExit()//Mouse tile uzerinden ciktiginde referansa null deger atama
    {
        if (isBlocked==false)
        {
            highlight.SetActive(false);
        }
        SelectedItem.Instance.onMouseTile = null;

    }
    private void OnTriggerEnter2D(Collider2D collision)//karakter tile'a girdiginde karakter referansi atama
    {
        if (!isCharacterExists)
        {
            if (collision.gameObject.CompareTag("Character"))
            {
                isCharacterExists = true;
                character = collision.gameObject;
            }
        }
        
        
    }
    private void OnTriggerExit2D(Collider2D collision)//karakter tile'dan ciktiginda referans atama
    {
        if (collision.gameObject==character)
        {
            isCharacterExists = false;
            character = null;
        }
    }
    private bool IsMouseOverUI()//Fare UI uzerinde kontrolu
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void SetNeighbourTiles()//Tile'lar eksenler(x,y) uzerinde 1 birimlik farkla dizildigi icin 1 birimlik farktaki komsularini listeye ekleme
    {
       
        List<GameObject> allTiles = TileMap.Instance.allTileObjects;
        for (int i = 0; i < allTiles.Count; i++)
        {
            if (allTiles[i].GetComponent<Tile>().positionOnY==positionOnY&& allTiles[i].GetComponent<Tile>().positionOnX==positionOnX+1)
            {
                neighbourTiles.Add(allTiles[i].GetComponent<Tile>());
            }
            if (allTiles[i].GetComponent<Tile>().positionOnY == positionOnY && allTiles[i].GetComponent<Tile>().positionOnX == positionOnX - 1)
            {
                neighbourTiles.Add(allTiles[i].GetComponent<Tile>());
            }
            if (allTiles[i].GetComponent<Tile>().positionOnX == positionOnX && allTiles[i].GetComponent<Tile>().positionOnY == positionOnY + 1)
            {
                neighbourTiles.Add(allTiles[i].GetComponent<Tile>());
            }
            if (allTiles[i].GetComponent<Tile>().positionOnX == positionOnX && allTiles[i].GetComponent<Tile>().positionOnY == positionOnY - 1)
            {
                neighbourTiles.Add(allTiles[i].GetComponent<Tile>());
            }
            if (allTiles[i].GetComponent<Tile>().positionOnY == positionOnY+1 && allTiles[i].GetComponent<Tile>().positionOnX == positionOnX + 1)
            {
                neighbourTiles.Add(allTiles[i].GetComponent<Tile>());
            }
            if (allTiles[i].GetComponent<Tile>().positionOnY == positionOnY+1 && allTiles[i].GetComponent<Tile>().positionOnX == positionOnX - 1)
            {
                neighbourTiles.Add(allTiles[i].GetComponent<Tile>());
            }
            if (allTiles[i].GetComponent<Tile>().positionOnY == positionOnY - 1 && allTiles[i].GetComponent<Tile>().positionOnX == positionOnX - 1)
            {
                neighbourTiles.Add(allTiles[i].GetComponent<Tile>());
            }
            if (allTiles[i].GetComponent<Tile>().positionOnY == positionOnY - 1 && allTiles[i].GetComponent<Tile>().positionOnX == positionOnX + 1)
            {
                neighbourTiles.Add(allTiles[i].GetComponent<Tile>());
            }






        }
    }
    
    
}