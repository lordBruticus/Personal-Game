using System;
using UnityEngine;
using System.Collections;

public class AddTowerWindow : MonoBehaviour
{
    //1
    public GameObject towerSlotToAddTowerTo;
    //2
    public void AddTower(string towerTypeAsString)
    {
        //3
        TowerType type = (TowerType)Enum.Parse(typeof(TowerType), towerTypeAsString, true);
        //4
        if (TowerManager.Instance.GetTowerPrice(type) <= GameManager.Instance.money)
        {
            //5
            GameManager.Instance.money -= TowerManager.Instance.GetTowerPrice(type);
            //6
            TowerManager.Instance.CreateNewTower(towerSlotToAddTowerTo, type);
            gameObject.SetActive(false);
        }
    }
}
