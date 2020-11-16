using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    //공격 시간
    float attackCooltime;
    float attackStartTime;
    float attackEndTime;

    //체크 변수
    bool isAttack = false;
    bool isSwing = false;

    RaycastHit hitInfo;

    public void SetWeaponStat(string name, float damage, float range, float attackCooltime, float attackStartTime, float attackEndTime)
    {
        weaponName = name;
        this.damage = damage;
        this.range = range;
        this.attackCooltime = attackCooltime;
        this.attackStartTime = attackStartTime;
        this.attackEndTime = attackEndTime;
    }

    public void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            if (isAttack == false)
            {
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    //공격 코루틴
    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        //StartAttackAni("Attack");

        yield return new WaitForSeconds(attackStartTime);
        isSwing = true;

        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(attackEndTime);
        isSwing = false;

        yield return new WaitForSeconds(attackCooltime - attackStartTime - attackEndTime);
        isAttack = false;
    }

    IEnumerator HitCoroutine()
    {
        while (isSwing == true)
        {
            //충돌 - '적' 확인해서 넣어야함
            if (CheckHitObject())
            {
                isSwing = false;
                Debug.Log(hitInfo.transform.name);
            }
            yield return null;
        }
    }

    //맞은 물체 확인
    bool CheckHitObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range))
            return true;
        else
            return false;
    }
}
