using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    //Eventler
    public static GameEvents Instance;

    private void Awake()
    {
        Instance = this;
    }

    
    public event Action OnBuildingPlaced;
    public event Action OnTilingComplete;
    public event Action OnBuildingSelected;
    public event Action OnCharacterSelected;
    public event Action OnNullSelected;
    public event Action OnBuildingDestroyed;

    public void BuildingDestroyed()
    {
        if (OnBuildingDestroyed!=null)
        {
            OnBuildingDestroyed();
        }
    }
    public void NullSelected()
    {
        if (OnNullSelected != null)
        {
            OnNullSelected();
        }

    }
    public void CharacterSelected()
    {
        if (OnCharacterSelected != null)
        {
            OnCharacterSelected();
        }

    }
    public void BuildingSelected()
    {
        if (OnBuildingSelected != null)
        {
            OnBuildingSelected();
        }

    }
    public void TilingComplete()
    {
        if (OnTilingComplete!=null)
        {
            OnTilingComplete();
        }

    }
    public void BuildingPlaced()
    {
        if (OnBuildingPlaced!=null)
        {
            OnBuildingPlaced();
        }
    }
}
