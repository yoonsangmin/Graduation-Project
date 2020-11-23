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
    EnemyController enemyController = null;
    Dictionary<int, List<Enemy>> enemys = new Dictionary<int, List<Enemy>>();

    //Ui 관련
    [SerializeField]
    BulletHud bulletHud = null;
    [SerializeField]
    MiniMap miniMap = null;
    [SerializeField]
    Stage1Quest quest = null;

    void Start()
    {
        enemys = enemyController.GetEnemys();
        miniMap.SetEnemysCount(enemys[quest.GetCurProgressStep() - 1].Count);
    }

    void Update()
    {
        WeaponUi();
        miniMap.MiniMapUpdate(player.transform, enemys[quest.GetCurProgressStep() - 1]);
        quest.UpdateQuestView();
    }

    void WeaponUi()
    {
        bulletHud.UpdateText(weaponController.GetCurMagazine(), weaponController.GetCurBullet());
    }
}
