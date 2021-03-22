using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

[Action("MyActions / MeleeAttack")]

public class BB_MeleeAttack : BasePrimitiveAction
{
    [InParam("Weapon")]
    public MeleeWeapon weapon;

    [InParam("Animator")]
    public Animator ani;

    public override TaskStatus OnUpdate()
    {
        if (weapon == null || ani == null)
            return TaskStatus.FAILED;

        weapon.Attack();
        ani.SetBool("IsAttack", true);

        return TaskStatus.COMPLETED;
    }
}