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
   protected RangedWeapon curRangedWeapon;
    
    public void Fire(bool isPlayer) { curRangedWeapon.Fire(isPlayer); }
    public void Reload() { curRangedWeapon.Reload(); }        
    public bool CanFire() { return curRangedWeapon.CanFire(); }
    public bool IsReload() { return curRangedWeapon.IsReload(); }   
}
