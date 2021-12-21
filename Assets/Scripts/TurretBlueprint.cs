using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TurretBlueprint 
{
    public GameObject prefab;
    public float cost;
    public GameObject upgradePrefab;
    public float upgradeCost;
    public float SellAmount()
    {
        return cost * 0.5f;
    }
}
