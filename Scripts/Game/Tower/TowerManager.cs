using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public struct TowerCost
{
    public TowerType TowerType;
    public int Cost;
}

public class TowerManager : MonoBehaviour
{
    //1
    public static TowerManager Instance;

    //2
    public GameObject gunTowerPrefab;

    //3
    public List<TowerCost> TowerCosts = new List<TowerCost>();
    //4
    void Awake()
    {
        Instance = this;
    }
    //5
    public void CreateNewTower(GameObject slotToFill, TowerType towerType)
    {
        switch (towerType)
        {
            case TowerType.Stone:
                Instantiate(gunTowerPrefab, slotToFill.transform.position, Quaternion.identity);
                slotToFill.gameObject.SetActive(false);
                break;
        }
    }
    //6
    public int GetTowerPrice(TowerType towerType)
    {
        return (from towerCost in TowerCosts where towerCost.TowerType == towerType select towerCost.Cost).FirstOrDefault();
    }
}
