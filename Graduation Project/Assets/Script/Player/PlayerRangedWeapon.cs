using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedWeapon : RangedWeapon
{
    [SerializeField]
    MainCamera mainCamera = null;  

    //줌모드
    [SerializeField]
    GameObject arm;
    [SerializeField]
    SkinnedMeshRenderer hand;
    bool isZoomMode = false;
    Vector3 weaponOriginPos = new Vector3(0.011f, -0.016f, 0.08f);
    Vector3 zoomOriginPos = new Vector3(-0.06f, 0.01f, -0.1f);

    void Start()
    {
        arm.transform.localPosition = weaponOriginPos;
    }

    //사격
    public void Fire()
    {
        if (curFireCooltime > 0 || isReload == true || (curBulletInBag <= 0 && curBulletInMagazine <= 0)) return;

        if (curBulletInMagazine > 0)
        {
            curBulletInMagazine--;
            curFireCooltime = fireCooltime;

            flash.Play();
            Bullets.Fire(FirePos());

            mainCamera.DoRecoilAction(recoilActionForce);

            //StopAllCoroutines();
            //StartCoroutine(RecoilActionCoroutine());
        }
        else
        {
            Reload();
        }
    }

    Vector3 FirePos()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, range))
            return hitInfo.point - gunEntry.transform.position;
        else
            return mainCamera.transform.position + mainCamera.transform.forward * range - gunEntry.transform.position;
    }

    //줌 모드
    public void Zoom(CrossHair crossHair)
    {
        if (isReload == true) return;

        isZoomMode = !isZoomMode;

        StopAllCoroutines();

        StartCoroutine(ZoomCoroutine(crossHair));
    }

    //줌모드 취소
    public void StopZoom(CrossHair crossHair)
    {
        if (isZoomMode == true)
            Zoom(crossHair);
    }

    //줌모드 코루틴
    IEnumerator ZoomCoroutine(CrossHair crossHair)
    {
        Vector3 goalPos;

        if (isZoomMode == true) goalPos = zoomOriginPos;
        else goalPos = weaponOriginPos;

        while (Vector3.Distance(arm.transform.localPosition, goalPos) > 0.005f)
            arm.transform.localPosition = Vector3.Lerp(arm.transform.localPosition, goalPos, 0.005f);

        arm.transform.localPosition = goalPos;

        if (goalPos == zoomOriginPos)
        {
            mainCamera.StartCameraZoom();
            crossHair.ActiveZoomMode();
            hand.enabled = false;
            GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
        else
        {
            mainCamera.StopCameraZoom();
            crossHair.DeactiveZoomMode();
            hand.enabled = true;
            GetComponent<SkinnedMeshRenderer>().enabled = true;
        }

        yield return null;
    }

    //Bullet HUD를 위한 public 함수
    public int GetCurMagazine() { return curBulletInMagazine; }
    public int GetCurBullet() { return curBulletInBag; }
}
