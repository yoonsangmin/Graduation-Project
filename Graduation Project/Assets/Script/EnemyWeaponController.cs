using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : WeaponController
{
    [SerializeField]
    RangedWeapon rangeWeapon;

    void Start()
    {        
        rangeWeapon.SetWeaponStat("AK", 10.0f, 50.0f, //name, damage, range
                                                100.0f, 0.005f, 2.0f, //speed, accuracy, fireCooltime
                                                1.0f, 0.0f, 0.0f, //reloadTime, 반동, 줌반동
                                                30, 1000); //탄창 최대 개수, 총알 최대 개수

        curRangedWeapon = rangeWeapon;
    }

    public void LookAtTarget(Transform target)
    {
        Vector3 temp = new Vector3(target.position.x, target.position.y + 1.0f, target.position.z);        
        curRangedWeapon.transform.LookAt(temp);
    }
}
