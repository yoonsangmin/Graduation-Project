using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using BBUnity.Conditions;

[Condition("MyCondition/canFire")]

public class BB_CanFire : GOCondition
{
    [InParam("WeaponController")]
    public EnemyWeaponController weapon;

    [InParam("Animator")]
    public Animator ani;

    public override bool Check()
    {
        if (weapon.CanFire() == false)
            ani.SetBool("IsFired", false);

        return weapon.CanFire();
    }
}