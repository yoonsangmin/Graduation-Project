using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    //반동
    float recoilActionForce;
    float recoilActionZoomForce;

    //정확도, 속도
    float accuracy;
    float speed;

    //사격 속도    
    float fireCooltime;
    float curFireCooltime;

    //재장전
    float reloadTime;
    bool isReload = false;

    //총알 관련 변수
    [SerializeField]
    BulletController Bullet;
    int maxBulletInMagazine;
    int curBulletInMagazine;
    int maxBulletInBag;
    int curBulletInBag;

    //줌모드
    bool isZoomMode = false;
    Vector3 weaponOriginPos;
    [SerializeField]
    Vector3 zoomOriginPos = new Vector3(0.1f, 0.05f, -0.1f);

    void Start()
    {
        weaponOriginPos = transform.localPosition;
    }

    void Update()
    {
        GunFireCooltimeCalc();
    }

    public void SetWeaponStat(string name, float damage, float range, float speed, float accuracy, float fireCooltime, float reloadTime, float recoilActionForce, float recoilActionZoomForce, int maxBulletInMagazine, int maxBulletInBag)
    {
        Bullet.SetBullet(maxBulletInMagazine, accuracy, range, speed, damage);

        weaponName = name;
        this.damage = damage;
        this.range = range;
        this.speed = speed;
        this.accuracy = accuracy;
        this.fireCooltime = fireCooltime;
        this.reloadTime = reloadTime;
        this.recoilActionForce = recoilActionForce;
        this.recoilActionZoomForce = recoilActionZoomForce;
        this.maxBulletInMagazine = maxBulletInMagazine;
        this.curBulletInMagazine = this.maxBulletInMagazine;
        this.maxBulletInBag = maxBulletInBag;
        this.curBulletInBag = this.maxBulletInBag;
    }

    //사격
    public void Fire()
    {
        if (curFireCooltime > 0 || isReload == true) return;

        if (curBulletInMagazine > 0)
        {
            curBulletInMagazine--;
            curFireCooltime = fireCooltime;

            flash.Play();
            Bullet.Fire();

            StopAllCoroutines();
            StartCoroutine(RecoilActionCoroutine());
        }
        else
        {
            Reload();
        }
    }

    //연사속도 재계산
    void GunFireCooltimeCalc()
    {
        if (curFireCooltime > 0)
            curFireCooltime -= Time.deltaTime;
    }

    //재장전
    public void Reload()
    {
        if (isReload == true || curBulletInMagazine >= maxBulletInMagazine) return;

        StopZoom();
        StartCoroutine(ReloadCoroutine());
    }

    //재장전 코루틴
    IEnumerator ReloadCoroutine()
    {
        if (curBulletInBag > 0)
        {
            isReload = true;

            //총알이있을때 장전
            curBulletInBag += curBulletInMagazine;

            yield return new WaitForSeconds(reloadTime);

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
        else
        {
            //총알이 없을 때
        }
    }

    //줌 모드
    public void Zoom()
    {
        if (isReload == true) return;

        isZoomMode = !isZoomMode;

        StopAllCoroutines();

        if (isZoomMode == true)
            StartCoroutine(ZoomCoroutine(zoomOriginPos));
        else
            StartCoroutine(ZoomCoroutine(weaponOriginPos));
    }

    //줌모드 취소
    public void StopZoom()
    {
        if (isZoomMode == true)
            Zoom();
    }

    //줌모드 코루틴
    IEnumerator ZoomCoroutine(Vector3 goalPos)
    {
        while (transform.localPosition != goalPos)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, goalPos, 0.2f);
            yield return null;
        }
    }

    //반동
    //velocity를 통해 반동 조절 해보기
    IEnumerator RecoilActionCoroutine()
    {
        Vector3 recoil_back = new Vector3(weaponOriginPos.x, weaponOriginPos.y, recoilActionForce);
        Vector3 retro_action_recoil_back = new Vector3(zoomOriginPos.x, zoomOriginPos.y, recoilActionZoomForce);

        if (isZoomMode == false)
        {
            transform.localPosition = weaponOriginPos;

            //반동시작
            while (transform.localPosition.z <= recoilActionForce - 0.02f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, recoil_back, 0.4f);
                yield return null;
            }

            // 원위치
            while (transform.localPosition != weaponOriginPos)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, weaponOriginPos, 0.1f);
                yield return null;
            }
        }
        else
        {
            transform.localPosition = zoomOriginPos;

            //반동시작
            while (transform.localPosition.z <= recoilActionForce - 0.02f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, retro_action_recoil_back, 0.4f);
                yield return null;
            }

            //원 위치
            while (transform.localPosition != zoomOriginPos)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, zoomOriginPos, 0.1f);
                yield return null;
            }
        }
    }


    //Bullet HUD를 위한 public 함수
    public int GetCurMagazine() { return curBulletInMagazine; }
    public int GetCurBullet() { return curBulletInBag; }


    public bool IsReload() { return isReload; }
}
