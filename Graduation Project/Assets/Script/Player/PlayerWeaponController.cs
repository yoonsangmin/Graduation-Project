using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : WeaponController
{
    PlayerRangedWeapon curRangedWeapon;

    AudioSource audioSource;
    float audioSourceOriginPitch;
    [SerializeField]
    AudioClip reload = null;
    [SerializeField]
    AudioClip shoot = null;
    [SerializeField]
    AudioClip draw = null;

    [SerializeField]
    PlayerRangedWeapon AK = null;
    [SerializeField]
    PlayerRangedWeapon Sniper = null;

    bool doWeaponChange = false;

    void Awake()
    {
        AK.SetWeaponStat("AK", 30.0f, 60.0f, //name, damage, range
                                150.0f, 0.002f, 0.2f, //speed, accuracy, fireCooltime
                                3.0f, 2.0f, //reloadTime, 반동
                                30, 500); //탄창 최대 개수, 총알 최대 개수

        Sniper.SetWeaponStat("Sniper", 120.0f, 120.0f, //name, damage, range
                                150.0f, 0.002f, 1.0f, //speed, accuracy, fireCooltime
                                3.0f, 10.0f, //reloadTime, 반동
                                2, 50); //탄창 최대 개수, 총알 최대 개수

        curRangedWeapon = AK;    
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSourceOriginPitch = audioSource.pitch;
        AudioController.instance.AddAudioSource(audioSource);
    }

    public void Fire() { if (doWeaponChange == true) return; curRangedWeapon.Fire(); PlayShootAudio(); }
    public void Reload() { if (doWeaponChange == true) return; curRangedWeapon.Reload(); PlayReloadAudio(); }
    public bool CanFire() { return doWeaponChange == false && curRangedWeapon.CanFire(); }
    public bool IsReload() { return curRangedWeapon.IsReload(); }

    public void WeaponChange()
    {
        doWeaponChange = true;
        curRangedWeapon.DoWeaponChange();
        if (audioSource.isPlaying == true || IsInvoking("ReturnAudioOriginPitch") == true)
        {
            audioSource.Stop();
            CancelInvoke("ReturnAudioOriginPitch");
            ReturnAudioOriginPitch();
        }
        PlayDrawAudio();

        if (curRangedWeapon == AK)
        {            
            curRangedWeapon.GetComponent<SkinnedMeshRenderer>().enabled = false;            
            curRangedWeapon = Sniper;
            curRangedWeapon.gameObject.SetActive(true);
        }
        else
        {
            curRangedWeapon.gameObject.SetActive(false);
            curRangedWeapon = AK;
            curRangedWeapon.GetComponent<SkinnedMeshRenderer>().enabled = true;
        }

        UiController.instance.ChangeWeaponImage(curRangedWeapon.GetName());
    }

    public void Zoom()
    {
        if (curRangedWeapon != Sniper || doWeaponChange == true) return;
        curRangedWeapon.Zoom();
    }
    public void StopZoom()
    {
        if (curRangedWeapon != Sniper) return;
        curRangedWeapon.StopZoom();
    }

    //Bullet HUD를 위한 public 함수
    public int GetCurMagazine() { return curRangedWeapon.GetCurMagazine(); }
    public int GetCurBullet() { return curRangedWeapon.GetCurBullet(); }

    public void SetCrossHair(CrossHair crossHair) { Sniper.SetCrossHair(crossHair); }

    public void FinishWeaponChange() { doWeaponChange = false; }

    void PlayReloadAudio()
    {
        audioSource.pitch = reload.length / curRangedWeapon.GetReloadTime();
        audioSource.PlayOneShot(reload);
        Invoke("ReturnAudioOriginPitch", curRangedWeapon.GetReloadTime());
    }
    void ReturnAudioOriginPitch() { audioSource.pitch = audioSourceOriginPitch; }
    void PlayShootAudio() { audioSource.PlayOneShot(shoot); }
    void PlayDrawAudio() { audioSource.PlayOneShot(draw); }
}
