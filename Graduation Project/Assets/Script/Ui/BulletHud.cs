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
    Image weaponImage = null;
    [SerializeField]
    Sprite akImage = null;
    [SerializeField]
    Sprite sniperImage = null;

    public void UpdateText(int loadedBulletNum, int remainBulletNum)
    {
        loadedBullet.text = loadedBulletNum.ToString();
        remainBullet.text = remainBulletNum.ToString();
    }

    public void ChangeWeapon(string name)
    {
        switch(name)
        {
            case "AK":
                weaponImage.sprite = akImage;
                break;
            case "Sniper":
                weaponImage.sprite = sniperImage;
                break;
        }        
    }
}
