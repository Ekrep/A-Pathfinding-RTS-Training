using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private GameObject character;//karakter GameObjesi
    private Sprite characterImage;//karakterin Image'si
    private string characterName;//adi
    private int health;//can
    private int attackDamage;//saldiri gucu
    private int defence;//defans
    private float movementSpeed;//hareket hizi
    private List<Tile> characterPath;
    private Tile characterPosition;
    private Tile targetTile;


    #region get set
    public Tile TargetTile
    {
        get
        {
            return targetTile;
        }
        set
        {
            targetTile = value;
        }
    }
    public GameObject CharacterObj
    {
        get
        {
            return character;
        }
        set
        {
            character = value;
        }
    }
    public Sprite CharacterImage
    {
        get
        {
            return characterImage;
        }
        set
        {
            characterImage = value;
        }
    }
    public string CharacterName
    {
        get
        {
            return characterName;
        }
        set
        {
            characterName = value;
        }
    }
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }
    public int Defence
    {
        get
        {
            return defence;
        }
        set
        {
            defence = value;
        }
    }
    public int AttackDamage
    {
        get
        {
            return attackDamage;
        }
        set
        {
            attackDamage = value;
        }
    }
    public float MovementSpeed
    {
        get
        {
            return movementSpeed;
        }
        set
        {
            movementSpeed = value;
        }
    }
    public List<Tile> CharacterPath
    {
        get
        {
            return characterPath;
        }
        set
        {
            characterPath = value;
        }
    }
    public Tile CharacterPosition
    {
        get
        {
            return characterPosition;
        }
        set
        {
            characterPosition = value;
        }
    }

    #endregion
    public Character() { }
    public Character(Sprite characterImage,GameObject characterObj, string name, int health, int attackDamage, float movementSpeed, int defence, Tile position, Tile target)//constructre
    {
        CharacterImage = characterImage;
        CharacterObj = characterObj;
        CharacterName = name;
        Health = health;
        AttackDamage = attackDamage;
        MovementSpeed = movementSpeed;
        Defence = defence;
        CharacterPosition = position;
        TargetTile = target;
    }
    public void SetInfo(Sprite image,string name, int attackDamage, int health, int defence, float movementSpeed)
    {
        CharacterImage = image;
        CharacterName = name;
        AttackDamage = attackDamage;
        Health = health;
        Defence = defence;
        MovementSpeed = movementSpeed;
    }

    public void CharacterMovement(GameObject character,float movementSpeed,Tile characterCurrentTile,Tile targetTile)//Path Finding ile karakter hareketi
    {
        List<Tile> path;
        if (characterCurrentTile!=targetTile)
        {
            if (targetTile != null)
            {
                path = PathFinding.Instance.FindPath(characterCurrentTile, targetTile);//bu kismi her tile degistiginde hesapliyor,hareket ederken yol ustunde engel olusursa etrafýndan dolasmak amacli
               
               if (path != null && path.Count > 0)
               {
                character.transform.position = Vector2.MoveTowards(character.transform.position, path[0].tilePos, movementSpeed * Time.deltaTime);
               



               }

                

               

            }

        }
        else
        {
            character.transform.position = Vector2.MoveTowards(character.transform.position, targetTile.gameObject.GetComponent<Tile>().tilePos, movementSpeed * Time.deltaTime);
        }    
    }
}

