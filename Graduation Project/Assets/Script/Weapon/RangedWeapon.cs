using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    //반동
    protected float recoilActionForce;

    //정확도, 속도
    protected float accuracy;
    protected float speed;

    //사격 속도    
    protected float fireCooltime;
    protected float curFireCooltime;

    //재장전
    protected float reloadTime;
    protected bool isReload = false;
    public bool _isReload { get { return isReload; } }

    //총알 관련 변수
    [SerializeField] protected BulletController Bullets = null;
    protected int maxBulletInMagazine;
    protected int curBulletInMagazine;
    public int _curBulletInMagazine { get { return curBulletInMagazine; } }
    protected int maxBulletInBag;
    protected int curBulletInBag;
    public int _curBulletInBag { get { return curBulletInBag; } }

    public void SetWeaponStat(string name, float damage, float range, float speed, float accuracy, float fireCooltime, float reloadTime, float recoilActionForce, int maxBulletInMagazine, int maxBulletInBag)
    {
        Bullets.SetBullet(maxBulletInMagazine, accuracy, range, speed, damage);

        weaponName = name;
        this.damage = damage;
        this.range = range;
        this.speed = speed;
        this.accuracy = accuracy;
        this.fireCooltime = fireCooltime;
        this.reloadTime = reloadTime;
        this.recoilActionForce = recoilActionForce;
        this.maxBulletInMagazine = maxBulletInMagazine;
        this.curBulletInMagazine = this.maxBulletInMagazine;
        this.maxBulletInBag = maxBulletInBag;
        this.curBulletInBag = this.maxBulletInBag;
    }

    private void Update()
    {
        GunFireCooltimeCalc();
    }

    //연사속도 재계산
    private void GunFireCooltimeCalc()
    {
        if (curFireCooltime > 0)
            curFireCooltime -= Time.deltaTime;
    }

    public virtual void Fire()
    {
        if (curFireCooltime > 0 || isReload == true || (curBulletInBag <= 0 && curBulletInMagazine <= 0)) return;

        if (curBulletInMagazine <= 0) { Reload(); return; }

        curBulletInMagazine--;

        curFireCooltime = fireCooltime;
    }

    public virtual void Reload() { if (isReload == true || curBulletInMagazine >= maxBulletInMagazine) return; }

    //재장전 코루틴
    protected IEnumerator ReloadCoroutine()
    {
        if (curBulletInBag > 0)
        {
            isReload = true;

            yield return new WaitForSeconds(reloadTime);

            //총알이있을때 장전
            curBulletInBag += curBulletInMagazine;

            if (curBulletInBag >= maxBulletInMagazine)
            {
                curBulletInMagazine = maxBulletInMagazine;
                curBulletInBag -= maxBulletInMagazine;
            }
            //현재 가지고 있는 총알의 개수가 탄창을 가득 채울수 없다면
            else
            {
                curBulletInMagazine = curBulletInBag;
                curBulletInBag = 0;
            }

            isReload = false;
        }
    }

    //사격 가능여부
    public bool CanFire() { return isReload == false && curFireCooltime <= 0 && (curBulletInBag > 0 || curBulletInMagazine > 0); }

    public void GetBullets(int value)
    {
        if (curBulletInBag + value <= maxBulletInBag)
            curBulletInBag += value;
        else
            curBulletInBag = maxBulletInBag;
    }

    public bool IsTargerPointOfSight(GameObject startPoint, string targetTagName)
    {
        RaycastHit hitInfo;
        
        if (Physics.Raycast(startPoint.transform.position, startPoint.transform.forward + new Vector3(Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy), 0), out hitInfo, range))
        {
            if (hitInfo.collider.gameObject.tag.Contains(targetTagName))
                return true;            
        }
        return false;
    }
}
