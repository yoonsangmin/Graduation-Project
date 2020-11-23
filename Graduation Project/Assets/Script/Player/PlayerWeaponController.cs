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

    [SerializeField]
    PlayerRangedWeapon AK = null;
    [SerializeField]
    PlayerRangedWeapon Sniper = null;

    bool doWeaponChange = false;

    void Awake()
    {
        AK.SetWeaponStat("AK", 30.0f, 60.0f, //name, damage, range
                                150.0f, 0.002f, 0.2f, //speed, accuracy, fireCooltime
                                3.0f, 2.0f, //reloadTime, 반동
                                30, 1000); //탄창 최대 개수, 총알 최대 개수

        Sniper.SetWeaponStat("AK", 90.0f, 120.0f, //name, damage, range
                                150.0f, 0.002f, 1.0f, //speed, accuracy, fireCooltime
                                3.0f, 10.0f, //reloadTime, 반동
                                1, 1000); //탄창 최대 개수, 총알 최대 개수

        curRangedWeapon = AK;
    }

    public void Fire() { if (doWeaponChange == true) return; curRangedWeapon.Fire(); }
    public void Reload() { if (doWeaponChange == true) return; curRangedWeapon.Reload(); }
    public bool CanFire() { return doWeaponChange == false && curRangedWeapon.CanFire(); }
    public bool IsReload() { return curRangedWeapon.IsReload(); }

    public void WeaponChange()
    {
        doWeaponChange = true;
        curRangedWeapon.DoWeaponChange();
        if (curRangedWeapon == AK)
        {
            curRangedWeapon.gameObject.SetActive(false);
            curRangedWeapon = Sniper;
            curRangedWeapon.gameObject.SetActive(true);
        }
        else
        {
            curRangedWeapon.gameObject.SetActive(false);
            curRangedWeapon = AK;
            curRangedWeapon.gameObject.SetActive(true);
        }
    }

    public void Zoom() { curRangedWeapon.Zoom(); }
    public void StopZoom() { curRangedWeapon.StopZoom(); }

    //Bullet HUD를 위한 public 함수
    public int GetCurMagazine() { return curRangedWeapon.GetCurMagazine(); }
    public int GetCurBullet() { return curRangedWeapon.GetCurBullet(); }

    //스나이퍼 나오면 바꿔야됨
    public void SetCrossHair(CrossHair crossHair) { curRangedWeapon.SetCrossHair(crossHair); }

    public void FinishWeaponChange() { doWeaponChange = false; }
}
