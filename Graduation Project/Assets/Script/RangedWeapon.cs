using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    //반동
    float recoil_action_force;
    float recoil_action_zoom_force;

    //정확도, 속도
    float accuracy;
    float speed;

    //사격 속도    
    float fire_cooltime;
    float cur_fire_cooltime;

    //재장전
    float reload_time;
    bool is_reload = false;

    //총알 관련 변수
    [SerializeField]
    BulletController Bullet;
    int max_bullet_in_magazine;
    int cur_bullet_in_magazine;
    int max_bullet_in_bag;
    int cur_bullet_in_bag;

    //줌모드
    bool is_zoom_mode = false;
    Vector3 weapon_origin_pos;
    [SerializeField]
    Vector3 zoom_origin_pos = new Vector3(0.1f, 0.05f, -0.1f);

    void Start()
    {
        weapon_origin_pos = transform.localPosition;
    }

    void Update()
    {
        GunFireCooltimeCalc();
    }

    public void SetWeaponStat(string name, float damage, float range, float speed, float accuracy, float fire_cooltime, float reload_time, float recoil_action_force, float recoil_action_zoom_force, int max_bullet_in_magazine, int max_bullet_in_bag)
    {
        Bullet.SetBullet(max_bullet_in_magazine, accuracy, range, speed, damage);

        weapon_name = name;
        this.damage = damage;
        this.range = range;
        this.speed = speed;
        this.accuracy = accuracy;
        this.fire_cooltime = fire_cooltime;
        this.reload_time = reload_time;
        this.recoil_action_force = recoil_action_force;
        this.recoil_action_zoom_force = recoil_action_zoom_force;
        this.max_bullet_in_magazine = max_bullet_in_magazine;
        this.cur_bullet_in_magazine = this.max_bullet_in_magazine;
        this.max_bullet_in_bag = max_bullet_in_bag;
        this.cur_bullet_in_bag = this.max_bullet_in_bag;        
    }

    //사격
    public void Fire()
    {
        if (cur_fire_cooltime > 0 || is_reload == true) return;

        if (cur_bullet_in_magazine > 0)
        {
            cur_bullet_in_magazine--;
            cur_fire_cooltime = fire_cooltime;

            Flash.Play();
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
        if (cur_fire_cooltime > 0)
            cur_fire_cooltime -= Time.deltaTime;
    }

    //재장전
    public void Reload()
    {
        if (is_reload == true || cur_bullet_in_magazine >= max_bullet_in_magazine) return;

        StopZoom();
        StartCoroutine(ReloadCoroutine());
    }

    //재장전 코루틴
    IEnumerator ReloadCoroutine()
    {
        if (cur_bullet_in_magazine > 0)
        {
            is_reload = true;

            //총알이있을때 장전
            cur_bullet_in_bag += cur_bullet_in_magazine;

            yield return new WaitForSeconds(reload_time);

            if (cur_bullet_in_bag >= max_bullet_in_magazine)
            {
                cur_bullet_in_magazine = max_bullet_in_magazine;
                cur_bullet_in_bag -= max_bullet_in_magazine;
            }
            //현재 가지고 있는 총알의 개수가 탄창을 가득 채울수 없다면
            else
            {
                cur_bullet_in_magazine = cur_bullet_in_bag;
                cur_bullet_in_bag = 0;
            }
            is_reload = false;
        }
        else
        {
            //총알이 없을 때
        }
    }

    //줌 모드
    public void Zoom()
    {
        if (is_reload == true) return;

        is_zoom_mode = !is_zoom_mode;

        StopAllCoroutines();

        if (is_zoom_mode == true)
            StartCoroutine(ZoomCoroutine(zoom_origin_pos));
        else
            StartCoroutine(ZoomCoroutine(weapon_origin_pos));
    }

    //줌모드 취소
    public void StopZoom()
    {
        if (is_zoom_mode == true)
            Zoom();
    }

    //줌모드 코루틴
    IEnumerator ZoomCoroutine(Vector3 goal_pos)
    {
        while (transform.localPosition != goal_pos)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, goal_pos, 0.2f);
            yield return null;
        }
    }

    //반동
    //velocity를 통해 반동 조절 해보기
    IEnumerator RecoilActionCoroutine()
    {
        Vector3 recoil_back = new Vector3(weapon_origin_pos.x, weapon_origin_pos.y, recoil_action_force);
        Vector3 retro_action_recoil_back = new Vector3(zoom_origin_pos.x, zoom_origin_pos.y, recoil_action_zoom_force);

        if (is_zoom_mode == false)
        {
            transform.localPosition = weapon_origin_pos;

            //반동시작
            while (transform.localPosition.z <= recoil_action_force - 0.02f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, recoil_back, 0.4f);
                yield return null;
            }

            // 원위치
            while (transform.localPosition != weapon_origin_pos)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, weapon_origin_pos, 0.1f);
                yield return null;
            }
        }
        else
        {
            transform.localPosition = zoom_origin_pos;

            //반동시작
            while (transform.localPosition.z <= recoil_action_zoom_force - 0.02f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, retro_action_recoil_back, 0.4f);
                yield return null;
            }

            //원 위치
            while (transform.localPosition != zoom_origin_pos)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, zoom_origin_pos, 0.1f);
                yield return null;
            }
        }
    }


    //Bullet HUD를 위한 public 함수
    public int GetCurMagazine() { return cur_bullet_in_magazine; }
    public int GetCurBullet() { return cur_bullet_in_bag; }
}
