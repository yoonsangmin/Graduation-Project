using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{    
    public Ranged_Weapon AK;

    void Start()
    {
        AK.SetWeaponStat("AK", 30, 100, 0, 0.2f, 3, 0.3f, 0.7f, 30, 20);
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
            AK.Fire();
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
}
