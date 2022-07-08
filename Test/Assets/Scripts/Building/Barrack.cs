using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : Building
{
    Building barrack;
    [SerializeField]private Sprite barrackSprite;
    public List<Character> soliderProducts;//Building'in uretebildigi nesneler
    [HideInInspector]public List<Tile> collidedTiles;//Building yerlestirildiginde temas edilen tile'lar
    public List<Tile> unBlocked;//Bloklanmamýs komsu tile'lari
    private bool placed;//Yerlestirilme kontrolcusu
    private bool canTakeNeighbour=true;//Komsu alabilme kontrolcusu

   

    private void Instance_onBuildingPlaced()//Building yerlestirildiginde gelen event ile komsu tile'lari alip, Building class'na aktarma
    {
       
       
        TakeUnBlockedTiles(collidedTiles, unBlocked, barrack.Isplaced);
        canTakeNeighbour = false;
        SetInfo(barrack.BuildingSprite, barrack.BuildingName, barrack.Products, barrack.Isplaced,barrack.UnblockedTiles);
        
    }

    
    private void OnDestroy()
    {
        GameEvents.Instance.OnBuildingPlaced -= Instance_onBuildingPlaced;
    }
    void Start()
    {
        GameEvents.Instance.OnBuildingPlaced += Instance_onBuildingPlaced;
        barrack = new Building(barrackSprite, "Barrack", 4, 4, soliderProducts,placed,unBlocked);
        gameObject.transform.localScale = new Vector2(barrack.Width, barrack.Height);//Building'in scale ayarlanmasi
        gameObject.transform.position = new Vector2(Grid.Instance.width / 2, Grid.Instance.height / 2);//Harita merkezinde spawnlanmasi
        
        
        
        
    }

    private void Update()
    {
        if (!barrack.Isplaced)//Yerlestirilmediyse sag clickte destroy edilmesi
        {
            if (Input.GetMouseButtonDown(1))
            {
                GameEvents.Instance.BuildingDestroyed();
                Destroy(gameObject);
            }
        }
        
        if (Input.GetMouseButtonDown(0))//Sol click basildiginde yerlestirilebilir oldugunun kontrolu
        {
            BuildControl(collidedTiles);
        }
        SetBuildingPos(gameObject, barrack.Width, barrack.Height, barrack.Isplaced, collidedTiles);
        if (barrack.Isplaced)
        {
            if (canTakeNeighbour)
            {
                GameEvents.Instance.BuildingPlaced();
                
                
            }
            //Yerlestirildikten sonra yanina baska Building yerlestirilirse bloklanan komsu tile'lari listeden cikarma
            for (int i = 0; i < unBlocked.Count; i++) 
             {
                    if (unBlocked[i].isBlocked)
                    {
                        unBlocked.Remove(unBlocked[i]);
                    }
             }
            
        }
        





    }
  
    private void OnTriggerEnter2D(Collider2D collision)//Yerlestirme oncesi temas edilen tile'lari listeye aktarma
    {
        if (collision.gameObject.CompareTag("Tile"))
        {
            
          
          collidedTiles.Add(collision.gameObject.GetComponent<Tile>());
            
               
        }
       
        
    }
    private void OnTriggerExit2D(Collider2D collision)//Yerlestirme oncesi temasi kesilen tile'lari listeden cikarma
    {
        if (collision.gameObject.CompareTag("Tile"))
        {

            
            collidedTiles.Remove(collision.gameObject.GetComponent<Tile>());


        }
       
    }
   
   
    public void BuildControl(List<Tile> collidedTiles)//Temas edilen tile sayisi Building'in alanina esitse yerlestirilebilir
    {

        int count = 0;
        for (int i = 0; i < collidedTiles.Count; i++)
        {
            if (collidedTiles[i].isBlocked == false && collidedTiles[i].isCharacterExists == false)
            {
                count++;
            }
        }
        if (count == barrack.Width*barrack.Height)
        {
            barrack.Isplaced = true;
        }

    }
 


}
