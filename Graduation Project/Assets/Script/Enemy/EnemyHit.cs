using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private Enemy enemyMain = null;

    void Start()
    {
        enemyMain = transform.root.GetComponent<Enemy>();
        transform.root.GetComponent<Enemy>().AddRigidBody(GetComponent<Rigidbody>());
    }

    public void HitByBullet(float damage, Vector3 point)
    {
        enemyMain.ReceiveDamage(damage, point);
    }

    public void HitByBarrel(float damage)
    {
        if (enemyMain._haveDamagedByBarrel == true) return;
        enemyMain.ReceiveDamage(damage);
    }
}
