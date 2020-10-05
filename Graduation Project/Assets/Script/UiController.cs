using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    //참조하기 위한 오브젝트
    [SerializeField]
    WeaponController Weapon_Controller;
    [SerializeField]
    Player Player;

    //Ui 관련
    [SerializeField]
    BulletHud Bullet_Hud;
    [SerializeField]
    CrossHair Cross_Hair;
    [SerializeField]
    HPBar Hp_Bar;

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
        Bullet_Hud.UpdateText(Weapon_Controller.GetCurMagazine(), Weapon_Controller.GetCurBullet());
    }

    void PlayerUi()
    {
        Cross_Hair.SetAnimation(Player);        
    }

}
