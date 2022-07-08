using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Character
{
    Character mage;
    public Sprite mageImage;
    public Tile onTile;
    public Tile characterTargetTile;
    private bool firstOrdered = false;
    

    private void Start()
    {
       
        
        mage = new Character(mageImage,gameObject, "Mage", 15, 5, 3f, 10, onTile, characterTargetTile);
        
        SetInfo(mage.CharacterImage,mage.CharacterName, mage.AttackDamage, mage.Health, mage.Defence, mage.MovementSpeed);
        Debug.Log(mage.MovementSpeed + "Defence");
        

    }
    private void Update()
    {
       
        mage.CharacterPosition = onTile;
        if (SelectedItem.Instance.selectedItem == gameObject)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (SelectedItem.Instance.onMouseTile != null)
                {
                    mage.TargetTile = SelectedItem.Instance.onMouseTile.GetComponent<Tile>();
                    firstOrdered = true;
                }

            }
        }

        if (firstOrdered == true)
        {
            Debug.Log("girdim warior");
            CharacterMovement(mage.CharacterObj, mage.MovementSpeed, mage.CharacterPosition, mage.TargetTile);

        }







    }

    private void OnTriggerStay2D(Collider2D collision)
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
