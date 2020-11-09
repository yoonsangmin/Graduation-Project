using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WeaponKind
{
    AK,
    Sniper,
    Knife
}

public class PlayerWeaponController : WeaponController
{
    CrossHair crossHair;
    [SerializeField]
    RangedWeapon AK;
    [SerializeField]
    RangedWeapon Sniper;

    void Start()
    {
        AK.SetWeaponStat("AK", 30.0f, 100.0f, //name, damage, range
                                100.0f, 0.002f, 0.2f, //speed, accuracy, fireCooltime
                                3.0f, 0.5f, 0.7f, //reloadTime, 반동, 줌반동
                                30, 1000); //탄창 최대 개수, 총알 최대 개수

        curRangedWeapon = AK;
    }
    
    public void WeaponChange()
    {

    }

    //void Zoom()
    //{
    //    if (Input.GetMouseButtonDown(1))
    //        curRangedWeapon.Zoom();
    //    if (Input.GetMouseButtonUp(1))
    //        curRangedWeapon.StopZoom();
    //}

    //Bullet HUD를 위한 public 함수
    public int GetCurMagazine() { return curRangedWeapon.GetCurMagazine(); }
    public int GetCurBullet() { return curRangedWeapon.GetCurBullet(); }

    public void SetCrossHair(CrossHair crossHair) { this.crossHair = crossHair; }
}
