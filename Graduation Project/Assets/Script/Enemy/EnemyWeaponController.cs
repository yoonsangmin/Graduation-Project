using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : WeaponController
{
    EnemyRangedWeapon curRangedWeapon;

    [SerializeField]
    EnemyRangedWeapon rangeWeapon;

    void Start()
    {
        rangeWeapon.SetWeaponStat("AK", 10.0f, 50.0f, //name, damage, range
                                                200.0f, 0.005f, 2.0f, //speed, accuracy, fireCooltime
                                                1.0f, 0.0f, 0.0f, //reloadTime, 반동, 줌반동
                                                30, 1000); //탄창 최대 개수, 총알 최대 개수

        curRangedWeapon = rangeWeapon;
    }

    public void Fire(Vector3 target) { curRangedWeapon.Fire(target); }
    public void Reload() { curRangedWeapon.Reload(); }
    public bool CanFire() { return curRangedWeapon.CanFire(); }
    public bool IsReload() { return curRangedWeapon.IsReload(); }
}
