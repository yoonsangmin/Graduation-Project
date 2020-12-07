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

    //총알 관련 변수
    [SerializeField]
    protected BulletController Bullets = null;
    protected int maxBulletInMagazine;
    protected int curBulletInMagazine;
    protected int maxBulletInBag;
    protected int curBulletInBag;

    void Update()
    {
        GunFireCooltimeCalc();
    }

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

    //연사속도 재계산
    void GunFireCooltimeCalc()
    {
        if (curFireCooltime > 0)
            curFireCooltime -= Time.deltaTime;
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
    public bool IsReload() { return isReload; }
}
