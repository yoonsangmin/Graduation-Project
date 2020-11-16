using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedWeapon : RangedWeapon
{    
    //사격
    public void Fire(Vector3 target)
    {
        if (curFireCooltime > 0 || isReload == true || (curBulletInBag <= 0 && curBulletInMagazine <= 0)) return;

        if (curBulletInMagazine > 0)
        {
            curBulletInMagazine--;
            curFireCooltime = fireCooltime;

            flash.Play();
            Vector3 temp = new Vector3(target.x, target.y + 0.9f, target.z);            
            Bullets.Fire(temp-gunEntry.transform.position);
          
            //StopAllCoroutines();
            //StartCoroutine(RecoilActionCoroutine());
        }
        else
        {
            Reload();
        }
    }
}
