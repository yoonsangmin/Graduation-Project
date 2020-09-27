using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_Weapon : Weapon
{
    //반동
    float recoil_action_force;
    float recoil_action_zoom_force;

    //사격 속도    
    float fire_cooltime;
    float cur_fire_cooltime;

    //재장전
    float reload_time;
    bool is_reload = false;

    //총알 관련 변수
    int max_magazine;
    int cur_magazine;
    int max_bullet_count;
    int cur_bullet_count;

    //줌모드
    bool is_zoom_mode = false;
    Vector3 weapon_origin_pos;
    [SerializeField]
    Vector3 zoom_origin_pos = new Vector3(0.2f, 0.05f, -0.1f);

    void Start()
    {
        weapon_origin_pos = transform.localPosition;
    }

    void Update()
    {
        GunFireCooltimeCalc();
    }

    public void SetWeaponStat(string name, float damage, float range, float accuracy, float fire_cooltime, float reload_time, float recoil_action_force, float recoil_action_zoom_force, int max_magazine, int max_bullet_count)
    {
        weapon_name = name;
        this.damage = damage;
        this.range = range;
        this.accuracy = accuracy;
        this.fire_cooltime = fire_cooltime;
        this.reload_time = reload_time;
        this.recoil_action_force = recoil_action_force;
        this.recoil_action_zoom_force = recoil_action_zoom_force;
        this.max_magazine = max_magazine;
        this.cur_magazine = this.max_magazine;
        this.max_bullet_count = max_bullet_count;
        this.cur_bullet_count = this.max_bullet_count;
    }

    //사격
    public void Fire()
    {
        if (cur_fire_cooltime > 0 || is_reload == true) return;

        if (cur_magazine > 0)
        {
            cur_magazine--;
            cur_fire_cooltime = fire_cooltime;

            Flash.Play();

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
        if (is_reload == true || cur_magazine >= max_magazine) return;

        StopZoom();
        StartCoroutine(ReloadCoroutine());
    }

    //재장전 코루틴
    IEnumerator ReloadCoroutine()
    {
        if (cur_bullet_count > 0)
        {
            is_reload = true;
            //SetAttackAni("Reload");
            //총알이있을때 장전
            cur_bullet_count += cur_magazine;

            yield return new WaitForSeconds(reload_time);

            if (cur_bullet_count >= max_magazine)
            {
                cur_magazine = max_magazine;
                cur_bullet_count -= max_magazine;
            }
            //현재 가지고 있는 총알의 개수가 탄창을 가득 채울수 없다면
            else
            {
                cur_magazine = cur_bullet_count;
                cur_bullet_count = 0;
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
        //StartAttackAni("Zoom");

        if (is_zoom_mode == true)
        {
            StopAllCoroutines();
            StartCoroutine(ZoomActivateCoroutine());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(ZoomDeactivateCoroutine());
        }
    }

    //줌모드 취소
    public void StopZoom()
    {
        if (is_zoom_mode == true)
            Zoom();
    }

    //줌모드 활성화
    IEnumerator ZoomActivateCoroutine()
    {
        while (transform.localPosition != zoom_origin_pos)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, zoom_origin_pos, 0.2f);
            yield return null;
        }
    }

    //줌모드 비활성화
    IEnumerator ZoomDeactivateCoroutine()
    {
        while (transform.localPosition != weapon_origin_pos)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, weapon_origin_pos, 0.2f);
            yield return null;
        }
    }

    //반동
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

}
