using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using BBUnity.Conditions;

[Condition("MyCondition/canMeleeAttack")]

public class BB_CanMeleeAttack : GOCondition
{
    [InParam("Weapon")]
    public MeleeWeapon weapon;

    public override bool Check()
    {
        return weapon.CanAttack();
    }
}