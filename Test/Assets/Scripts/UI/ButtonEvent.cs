using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    public int buttonID;
    public GameObject spawnPrefab;
    private bool buildingPlaced;//Tekrar tekrar button'a tiklandiginda Building spawnlamamasi icin kontrolcu
    private void OnDisable()
    {
        GameEvents.Instance.OnBuildingSelected -= Instance_OnBuildingSelected;
        GameEvents.Instance.OnNullSelected -= Instance_OnNullSelected;
        GameEvents.Instance.OnBuildingDestroyed -= Instance_OnBuildingDestroyed;
        GameEvents.Instance.OnBuildingPlaced -= Instance_OnBuildingPlaced;
    }

    private void Start()
    {
        GameEvents.Instance.OnBuildingSelected += Instance_OnBuildingSelected;
        GameEvents.Instance.OnNullSelected += Instance_OnNullSelected;
        GameEvents.Instance.OnBuildingDestroyed += Instance_OnBuildingDestroyed;
        GameEvents.Instance.OnBuildingPlaced += Instance_OnBuildingPlaced;
        buildingPlaced = false;

        //Button ID'sine gore spawn obje listesindeki elementin eslesip verilerini button'a aktarmasi
        if (buttonID < BuildingSpawnObjects.Instance.buildingSpawnPrefabs.Count)
        {
            spawnPrefab = BuildingSpawnObjects.Instance.buildingSpawnPrefabs[buttonID].buildingPrefab;
            gameObject.GetComponent<Image>().sprite = BuildingSpawnObjects.Instance.buildingSpawnPrefabs[buttonID].buildingSpawnImage;
        }
    }

    private void Instance_OnBuildingPlaced()
    {
        buildingPlaced = false;
    }

    private void Instance_OnBuildingDestroyed()
    {
        buildingPlaced = false;
    }

    private void Instance_OnNullSelected()
    {
            //Button ID'sine gore spawn obje listesindeki elementin eslesip verilerini button'a aktarmasi
            if (buttonID < BuildingSpawnObjects.Instance.buildingSpawnPrefabs.Count)
            {
                spawnPrefab = BuildingSpawnObjects.Instance.buildingSpawnPrefabs[buttonID].buildingPrefab;
                gameObject.GetComponent<Image>().sprite = BuildingSpawnObjects.Instance.buildingSpawnPrefabs[buttonID].buildingSpawnImage;
            }
        
        
        
        
    }

    private void Instance_OnBuildingSelected()
    {
        //Button ID'sine gore spawn obje listesindeki elementin eslesip verilerini button'a aktarmasi
        if (SelectedItem.Instance.selectedItem.GetComponent<Building>().Products.Count>0)
        {
            if (buttonID < SelectedItem.Instance.selectedItem.GetComponent<BuildingProductImage>().spwanObjPrefabs.Count)
            {
                spawnPrefab = SelectedItem.Instance.selectedItem.GetComponent<BuildingProductImage>().spwanObjPrefabs[buttonID];
            }
            if (buttonID < SelectedItem.Instance.selectedItem.GetComponent<BuildingProductImage>().productImages.Count)
            {
                gameObject.GetComponent<Image>().sprite = SelectedItem.Instance.selectedItem.GetComponent<BuildingProductImage>().productImages[buttonID];
            }

        }
        

    }

    public void ClickDown()//Buttona tiklandiginda kontrollerin yapilip objenin spawnlanmasi(Unity EvenSystem) 
    {
        if (SelectedItem.Instance.selectedItem != null && SelectedItem.Instance.selectedItem.CompareTag("Building"))
        {
            
            if (spawnPrefab != null)
            {
                if (SelectedItem.Instance.selectedItem.GetComponent<Building>().UnblockedTiles.Count > 0)
                {
                    Instantiate(spawnPrefab, SelectedItem.Instance.selectedItem.GetComponent<Building>().UnblockedTiles[buttonID].tilePos, Quaternion.identity);
                }
                
            }
        }
        if (SelectedItem.Instance.selectedItem==null)
        {
            if (buildingPlaced==false)
            {
                if (spawnPrefab != null)
                {
                    Instantiate(spawnPrefab);
                    buildingPlaced = true;
                }
            }
            
        }
       
        
    }
   
}
