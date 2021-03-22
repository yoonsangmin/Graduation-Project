using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] protected MeleeWeaponStat weaponStat = null;
    public MeleeWeaponStat _weaponStat { get { return weaponStat; } }

    protected string targetObjectName;

    //체크 변수
    protected bool isAttack = false;
    public bool _isAttack { get { return isAttack; } }
    public bool CanAttack() { return !_isAttack; }
    
    private bool isSwing = false;      

    public void Attack()
    {
        if (isAttack == false)
            StartCoroutine(AttackCoroutine());
    }

    //공격 코루틴
    private IEnumerator AttackCoroutine()
    {
        isAttack = true;

        yield return new WaitForSeconds(weaponStat._attackStartTime);
        isSwing = true;

        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(weaponStat._attackEndTime);
        isSwing = false;

        yield return new WaitForSeconds(weaponStat._attackCoolTime - weaponStat._attackStartTime - weaponStat._attackEndTime);
        isAttack = false;
    }

    private IEnumerator HitCoroutine()
    {
        while (isSwing == true)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, weaponStat._range))
            {
                if (hitInfo.collider.gameObject.tag == targetObjectName)
                    ProcessHiting(hitInfo);
                isSwing = false;
            }
            yield return null;
        }
    }

    protected virtual void ProcessHiting(RaycastHit hitInfo)
    {
    }
}
