using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletHud : MonoBehaviour
{
    [SerializeField]
    Text loadedBullet = null;
    [SerializeField]
    Text remainBullet = null;
    [SerializeField]
    Image weaponImage;

    public void UpdateText(int loadedBulletNum, int remainBulletNum)
    {
        loadedBullet.text = loadedBulletNum.ToString();
        remainBullet.text = remainBulletNum.ToString();
    }

    public void ChangeWeapon()
    {

    }
}
