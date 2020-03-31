using UnityEngine;
using System.Collections;

public class Laser : FollowingProjectile
{
    //1
    public float damage;
    //2
    protected override void OnHitEnemy()
    {
        //3
        enemyToFollow.TakeDamage(damage);
        Destroy(gameObject);
    }
}
