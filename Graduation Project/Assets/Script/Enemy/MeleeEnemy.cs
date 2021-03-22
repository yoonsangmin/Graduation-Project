using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private EnemyMeleeWeapon weapon = null;

    protected override void AnimatorSetting()
    {
        base.AnimatorSetting();
        ani.SetBool("IsAttack", weapon._isAttack);
    }
}