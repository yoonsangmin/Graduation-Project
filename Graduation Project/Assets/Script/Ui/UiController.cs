using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    //참조하기 위한 오브젝트
    [SerializeField]
    PlayerWeaponController weaponController = null;
    [SerializeField]
    Player player = null;
    [SerializeField]
    Dictionary<int, List<Enemy>> enemys = new Dictionary<int, List<Enemy>>();

    //Ui 관련
    [SerializeField]
    BulletHud bulletHud = null;
    [SerializeField]
    MiniMap miniMap = null;

    int curProgress = 0;

    void Start()
    {
        SetEnemys();
        miniMap.SetEnemysCount(enemys[curProgress].Count);
    }

    void Update()
    {
        WeaponUi();
        miniMap.MiniMapUpdate(player.transform, enemys[curProgress]);
    }

    void WeaponUi()
    {
        bulletHud.UpdateText(weaponController.GetCurMagazine(), weaponController.GetCurBullet());
    }

    void SetEnemys()
    {
        List<Enemy> addEnemys = new List<Enemy>();
        for (int i = 0; i < GameObject.Find("Enemys").transform.childCount; i++)
        {
            GameObject enemyObj = GameObject.Find("Enemys").transform.GetChild(i).gameObject;
            if (enemyObj.tag == "Enemy")
                addEnemys.Add(enemyObj.GetComponent<Enemy>());
        }

        enemys.Add(0, addEnemys);
    }
}
