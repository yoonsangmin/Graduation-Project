using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletHud : MonoBehaviour
{
    [SerializeField]
    Text Loaded_Bullet;
    [SerializeField]
    Text Remain_Bullet;
    [SerializeField]
    Image Weapon_Image;

    public void UpdateText(int loaded_bullet, int remain_bullet)
    {
        Loaded_Bullet.text = loaded_bullet.ToString();
        Remain_Bullet.text = remain_bullet.ToString();
    }

    public void ChangeWeapon()
    {

    }
}
