using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    //참조하기 위한 오브젝트
    [SerializeField]
    WeaponController weaponController;
    [SerializeField]
    Player player;

    //Ui 관련
    [SerializeField]
    BulletHud bulletHud;
    [SerializeField]
    CrossHair crossHair;
    [SerializeField]
    HPBar hpBar;

    void Start()
    {
        
    }

    void Update()
    {
        WeaponUi();
        PlayerUi();
    }
    
    void WeaponUi()
    {
        bulletHud.UpdateText(weaponController.GetCurMagazine(), weaponController.GetCurBullet());
    }

    void PlayerUi()
    {
        crossHair.SetAnimation(player);        
    }

}
