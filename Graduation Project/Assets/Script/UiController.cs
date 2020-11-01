using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    //참조하기 위한 오브젝트
    [SerializeField]
    PlayerWeaponController weaponController;
    [SerializeField]
    Player player;

    //Ui 관련
    [SerializeField]
    BulletHud bulletHud;
    [SerializeField]
    HPBar hpBar;

    void Start()
    {
        
    }

    void Update()
    {
        WeaponUi();
    }
    
    void WeaponUi()
    {
        bulletHud.UpdateText(weaponController.GetCurMagazine(), weaponController.GetCurBullet());
    }
}
