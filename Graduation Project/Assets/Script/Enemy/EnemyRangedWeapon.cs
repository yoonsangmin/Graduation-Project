using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedWeapon : RangedWeapon
{    
    //사격
    public void Fire()
    {
        if (curFireCooltime > 0 || isReload == true || (curBulletInBag <= 0 && curBulletInMagazine <= 0)) return;

        if (curBulletInMagazine > 0)
        {
            curBulletInMagazine--;
            curFireCooltime = fireCooltime;

            flash.Play();

            Bullets.Fire(this.gameObject);
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

        StartCoroutine(ReloadCoroutine());
    }
}
