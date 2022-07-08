using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    public GameObject productPanel;
    public GameObject buildingPanel;
    public Image selectedItemImage;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI health;
    public TextMeshProUGUI defence;
    public TextMeshProUGUI attack;
    public TextMeshProUGUI movementSpeed;
   

    

    private void Instance_onNullSelected()
    {
        SetNullInfoUI();
    }

    private void Instance_onBuildingSelected()
    {
        SetBuildingInfoUI();
      
       
       
    }

    private void Instance_onCharacterSelected()
    {
        SetCharaterInfoUI();
        
    }
    private void OnDestroy()
    {
        GameEvents.Instance.OnCharacterSelected -= Instance_onCharacterSelected;
        GameEvents.Instance.OnBuildingSelected -= Instance_onBuildingSelected;
        GameEvents.Instance.OnNullSelected -= Instance_onNullSelected;
    }

    void Start()
    {
        GameEvents.Instance.OnCharacterSelected += Instance_onCharacterSelected;
        GameEvents.Instance.OnBuildingSelected += Instance_onBuildingSelected;
        GameEvents.Instance.OnNullSelected += Instance_onNullSelected;
        selectedItemImage.GetComponent<CanvasGroup>().alpha = 0;
    }

   
    private void SetCharaterInfoUI()//(Karakter secildiginde)Yapilan secimlere gore Info Paneline verilerin aktarilmasi ve panel acik kapali kontrolu
    {
        selectedItemImage.GetComponent<CanvasGroup>().alpha = 1;
        buildingPanel.GetComponent<CanvasGroup>().alpha = 0;
        buildingPanel.GetComponent<CanvasGroup>().interactable=false;
        buildingPanel.transform.localScale = Vector3.zero;
        productPanel.SetActive(false);
        Character character;
        character = SelectedItem.Instance.selectedItem.GetComponent<Character>();
        selectedItemImage.enabled = true;
        selectedItemName.enabled = true;
        health.enabled = true;
        defence.enabled = true;
        attack.enabled = true;
        movementSpeed.enabled = true;
        selectedItemImage.sprite = character.CharacterImage;
        health.text = character.Health.ToString();
        defence.text = character.Defence.ToString();
        attack.text = character.AttackDamage.ToString();
        movementSpeed.text = character.MovementSpeed.ToString();
        selectedItemName.text = character.CharacterName;
    }
    private void SetBuildingInfoUI()//(Building secildiginde)Yapilan secimlere gore Info Paneline verilerin aktarilmasi ve panel acik kapali kontrolu
    {
        selectedItemImage.GetComponent<CanvasGroup>().alpha = 1;
        productPanel.SetActive(true);
        Building building;
        building = SelectedItem.Instance.selectedItem.GetComponent<Building>();
        if (building.Products.Count>0)
        {
            buildingPanel.GetComponent<CanvasGroup>().alpha = 1;
            buildingPanel.GetComponent<CanvasGroup>().interactable = true;
            buildingPanel.transform.localScale = Vector3.one;
        }
        else
        {
            buildingPanel.GetComponent<CanvasGroup>().alpha = 0;
            buildingPanel.GetComponent<CanvasGroup>().interactable = false;
            buildingPanel.transform.localScale = Vector3.zero;
        }
        selectedItemImage.enabled = true;
        selectedItemName.enabled = true;
        selectedItemImage.sprite = building.BuildingSprite;
        selectedItemName.text = building.BuildingName;
        health.enabled = false;
        defence.enabled = false;
        attack.enabled = false;
        movementSpeed.enabled = false;
        if (building.Products.Count>0)//Object Pool'dan InfoPanele Building productlarin cekilip Image olarak yerlestirilmesi
        {
            for (int i = 0; i < building.Products.Count; i++)
            {
                GameObject productImage;
                productImage = ObjectPool.Instance.SpawnFromPool("BarrackProductPanel", productPanel.transform.position, Quaternion.identity, productPanel.transform);
                productImage.GetComponent<Image>().sprite = building.gameObject.GetComponent<BuildingProductImage>().productImages[i];

            }

        }
        else
        {
            productPanel.SetActive(false);
        }
        

    }
    private void SetNullInfoUI()
    {
        selectedItemImage.GetComponent<CanvasGroup>().alpha = 1;
        buildingPanel.GetComponent<CanvasGroup>().alpha = 1;
        buildingPanel.GetComponent<CanvasGroup>().interactable = true;
        buildingPanel.transform.localScale = Vector3.one;
        selectedItemImage.enabled = false;
        selectedItemName.enabled = false;
        health.enabled = false;
        defence.enabled = false;
        attack.enabled = false;
        movementSpeed.enabled = false;
        productPanel.SetActive(false);



    }//(Secim olmadiginda)Yapilan secimlere gore Info Paneline verilerin aktarilmasi ve panel acik kapali kontrolu
}
