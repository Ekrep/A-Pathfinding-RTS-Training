using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedItem : MonoBehaviour
{
    public static SelectedItem Instance;
    public GameObject selectedItem;
    public GameObject targetArea;
    public GameObject onMouseTile;



    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
       
        SelectionProcess();
        
    }
    private void SelectionProcess()//Mouse pozisyonuna gore raycast atilarak obje secme
    {
        if (Input.GetMouseButton(0) && !IsMouseOverUI())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hit)
            {

                if (selectedItem != hit.transform.gameObject)
                {
                    

                    if (hit.transform.gameObject.CompareTag("Character"))
                    {
                        selectedItem = hit.transform.gameObject;
                        GameEvents.Instance.CharacterSelected();
                    }
                    if (hit.transform.gameObject.CompareTag("Building"))
                    {
                        
                        
                        if (hit.transform.GetComponent<Building>().Isplaced == true)
                        {
                            selectedItem = hit.transform.gameObject;
                            GameEvents.Instance.BuildingSelected();

                        }

                    }
                    if (!hit.transform.gameObject.CompareTag("Character") && !hit.transform.gameObject.CompareTag("Building"))
                    {
                        selectedItem = null;
                        GameEvents.Instance.NullSelected();
                    }

                }

            }


        }
    }
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    
    
}
