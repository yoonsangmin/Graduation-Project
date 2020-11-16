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
    PlayerRangedWeapon curRangedWeapon;
    CrossHair crossHair;
    [SerializeField]
    PlayerRangedWeapon AK;
    [SerializeField]
    PlayerRangedWeapon Sniper;

    void Start()
    {
        AK.SetWeaponStat("AK", 30.0f, 100.0f, //name, damage, range
                                200.0f, 0.002f, 0.2f, //speed, accuracy, fireCooltime
                                3.0f, 0.5f, 0.7f, //reloadTime, 반동, 줌반동
                                30, 1000); //탄창 최대 개수, 총알 최대 개수

        curRangedWeapon = AK;
    }

    private void Update()
    {
        Zoom();
    }

    public void Fire() { curRangedWeapon.Fire(); }
    public void Reload() { curRangedWeapon.Reload(); }
    public bool CanFire() { return curRangedWeapon.CanFire(); }
    public bool IsReload() { return curRangedWeapon.IsReload(); }

    public void WeaponChange()
    {

    }    

    void Zoom()
    {
        if (Input.GetMouseButtonDown(1))
            curRangedWeapon.Zoom(crossHair);
        if (Input.GetMouseButtonUp(1) || IsReload() == true)
            curRangedWeapon.StopZoom(crossHair);
    }

    //Bullet HUD를 위한 public 함수
    public int GetCurMagazine() { return curRangedWeapon.GetCurMagazine(); }
    public int GetCurBullet() { return curRangedWeapon.GetCurBullet(); }

    public void SetCrossHair(CrossHair crossHair) { this.crossHair = crossHair; }
}
