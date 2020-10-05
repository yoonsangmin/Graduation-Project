using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    //공격 시간
    float attack_cooltime;
    float attack_start;
    float attack_end;

    //체크 변수
    bool is_attack = false;
    bool is_swing = false;

    RaycastHit hit_info;

    public void SetWeaponStat(string name, float damage, float range, float attack_cooltime, float attack_start, float attack_end)
    {
        weapon_name = name;
        this.damage = damage;
        this.range = range;
        this.attack_cooltime = attack_cooltime;
        this.attack_start = attack_start;
        this.attack_end = attack_end;
    }

    public void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            if (is_attack == false)
            {
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    //공격 코루틴
    IEnumerator AttackCoroutine()
    {
        is_attack = true;
        //StartAttackAni("Attack");

        yield return new WaitForSeconds(attack_start);
        is_swing = true;

        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(attack_end);
        is_swing = false;

        yield return new WaitForSeconds(attack_cooltime - attack_start - attack_end);
        is_attack = false;
    }

    IEnumerator HitCoroutine()
    {
        while (is_swing == true)
        {
            //충돌 - '적' 확인해서 넣어야함
            if (CheckHitObject())
            {
                is_swing = false;
                Debug.Log(hit_info.transform.name);
            }
            yield return null;
        }
    }

    //맞은 물체 확인
    bool CheckHitObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit_info, range))
            return true;
        else
            return false;
    }
}
