using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Character
{

    Character warrior;
    public Sprite wariorImage;
    public Tile onTile;//karakterin mevcut bulundugu tile
    public Tile characterTargetTile;//hedef tile
    private bool firstOrdered=false;//ilk spawnlandiginda baska karakterin hedefini almamasi icin kontrolcu

    

    private void Start()
    {
        warrior = new Character(wariorImage, gameObject, "Warrior", 10, 5, 1f, 10, onTile, characterTargetTile);
        SetInfo(warrior.CharacterImage, warrior.CharacterName, warrior.AttackDamage, warrior.Health, warrior.Defence, warrior.MovementSpeed);//Olusturulan karakterin infosunu Character class'na yazma
        
        
       
    }
    private void Update()
    {
        
        warrior.CharacterPosition = onTile;
        if ( SelectedItem.Instance.selectedItem == gameObject)//Sag tiklandiginde hedef tile'i belirleme
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (SelectedItem.Instance.onMouseTile!=null)
                {
                    warrior.TargetTile = SelectedItem.Instance.onMouseTile.GetComponent<Tile>();
                    firstOrdered = true;
                }
                
            }
        }
       
        if (firstOrdered==true)
        {
                //Hareket fonsiyonunu cagirma
                CharacterMovement(warrior.CharacterObj, warrior.MovementSpeed, warrior.CharacterPosition, warrior.TargetTile);
               
        }
       






    }
   
    private void OnTriggerStay2D(Collider2D collision)//Bulundugu tile
    {
        if (collision.gameObject.CompareTag("Tile") && !collision.gameObject.GetComponent<Tile>().isBlocked)
        {
            if (gameObject.transform.position == collision.gameObject.transform.position)
            {
                onTile = collision.gameObject.GetComponent<Tile>();
            }

        }
    }


   
   

    
    



}
