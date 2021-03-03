using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedWeapon : RangedWeapon
{
    [SerializeField]
    MainCamera mainCamera = null;

    CrossHair crossHair;

    //스나이퍼 무기 최종 나오면 바꿔야됨
    //줌모드
    [SerializeField]
    SkinnedMeshRenderer hand = null;
    [SerializeField]
    GameObject weaponShape = null;
    bool isZoomMode = false;

    //사격
    public void Fire()
    {
        if (curFireCooltime > 0 || isReload == true || (curBulletInBag <= 0 && curBulletInMagazine <= 0)) return;

        if (curBulletInMagazine > 0)
        {
            curBulletInMagazine--;
            
            curFireCooltime = fireCooltime;

            if (isZoomMode == false)
                flash.Play();

            Bullets.Fire(mainCamera.gameObject);

            mainCamera.DoRecoilAction(recoilActionForce);
        }
        else
        {
            Reload();
        }
    }

    //재장전
    public override void Reload()
    {
        base.Reload();        

        StopZoom();
        StartCoroutine(ReloadCoroutine());        
    }

    //줌 모드
    public void Zoom()
    {
        if (isReload == true) return;

        isZoomMode = !isZoomMode;

        if (isZoomMode == true)
        {
            mainCamera.StartCameraZoom();
            crossHair.ActiveZoomMode();
        }
        else
        {
            mainCamera.StopCameraZoom();
            crossHair.DeactiveZoomMode();
        }

        hand.enabled = !isZoomMode;
        weaponShape.SetActive(!isZoomMode);
    }

    //줌모드 취소
    public void StopZoom()
    {
        if (isZoomMode == true)
            Zoom();
    }

    //Bullet HUD를 위한 public 함수
    public int GetCurMagazine() { return curBulletInMagazine; }
    public int GetCurBullet() { return curBulletInBag; }
    public string GetName() { return weaponName; }

    public void SetCrossHair(CrossHair crossHair) { this.crossHair = crossHair; }

    public void DoWeaponChange()
    {
        if (isReload == true)
        {
            StopAllCoroutines();
            isReload = false;
        }
        
        if(isZoomMode == true)
        {
            StopZoom();
        }
    }

    public float GetReloadTime() { return reloadTime; }   
}
