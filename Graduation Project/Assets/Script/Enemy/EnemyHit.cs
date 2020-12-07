using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField]
    Enemy enemyMain = null;

    void Start()
    {
        enemyMain.AddRigidBody(GetComponent<Rigidbody>());
    }

    public void HitByBullet(float damage, Vector3 point)
    {
        enemyMain.ReceiveDamage(damage, point);
    }

    public void HitByBarrel(float damage)
    {
        if (enemyMain.HaveDamagedByBarrel() == true) return;
        enemyMain.ReceiveDamage(damage);
    }
}
