using UnityEngine;
using System.Collections;

public class GunTower : Tower
{
    //1
    public GameObject stonePrefab;
    //2
    protected override void AttackEnemy()
    {
        base.AttackEnemy();
        //3
        GameObject laser = (GameObject)Instantiate(stonePrefab, towerPieceToAim.position, Quaternion.identity);
        laser.GetComponent<Laser>().enemyToFollow = targetEnemy;
        laser.GetComponent<Laser>().damage = attackPower;
    }
}
