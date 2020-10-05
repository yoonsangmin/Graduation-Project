using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WeaponKind
{
    AK,
    Sniper,
    Knife
}

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    CrossHair Cross_Hair;
    [SerializeField]
    RangedWeapon AK;

    void Start()
    {
        AK.SetWeaponStat("AK", 30, 100, 30.0f, 0.002f, 0.2f, 3, 0.3f, 0.7f, 30, 20);

    }

    void Update()
    {
        Fire();
        Reload();
        Zoom();
    }

    void Fire()
    {
        if (Input.GetMouseButton(0))
        {
            AK.Fire();
            Cross_Hair.StartFireAnimation();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Cross_Hair.StopFireAnimation();
        }
    }

    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
            AK.Reload();
    }

    void Zoom()
    {
        if (Input.GetMouseButtonDown(1))
            AK.Zoom();
        if (Input.GetMouseButtonUp(1))
            AK.StopZoom();
    }

    //Bullet HUD를 위한 public 함수
    public int GetCurMagazine() { return AK.GetCurMagazine(); }
    public int GetCurBullet() { return AK.GetCurBullet(); }
}
