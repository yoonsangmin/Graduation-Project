using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    private static UiController uiController;
    public static UiController instance
    {
        get
        {
            if (uiController == null)
                uiController = FindObjectOfType<UiController>();
            return uiController;
        }
    }

    private delegate void ControllGame();
    ControllGame StopGame;
    ControllGame PlayGame;

    //참조하기 위한 오브젝트      
    private Dictionary<int, List<Enemy>> enemys = new Dictionary<int, List<Enemy>>();

    //Ui 관련
    [SerializeField] private BulletHud bulletHud = null;
    [SerializeField] private MiniMap miniMap = null;
    [SerializeField] private CriticalTexts criticalText = null;
    [SerializeField] public Option option = null;

    void Start()
    {
        StopGame += Player.instance.StopPlayer;
        StopGame += MainCamera.instance.StopCamera;

        PlayGame += Player.instance.PlayPlayer;
        PlayGame += MainCamera.instance.PlayCamera;

        //enemys = EnemyController.instance.GetEnemys();
        //miniMap.SetEnemysCount(enemys[Stage1Controller.instance._enemySummonIndex].Count);
    }

    void Update()
    {
        WeaponUi();
        // miniMap.MiniMapUpdate(Player.instance.transform, enemys[Stage1Controller.instance._enemySummonIndex]);
        //Stage1Controller.instance.UpdateQuestView();
        OpenOptionWindow();
    }

    private void WeaponUi()
    {
        bulletHud.UpdateText(Player.instance._weaponController._curRangedWeapon._curBulletInMagazine, Player.instance._weaponController._curRangedWeapon._curBulletInBag);
    }

    private void OpenOptionWindow()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            option.gameObject.SetActive(!option.gameObject.activeSelf);
            if (option.gameObject.activeSelf == true)
                StopGame();
            else
                PlayGame();
        }
    }

    public void HitCritical() { criticalText.HitCritical(); }
    public void ChangeWeaponImage(string name) { bulletHud.ChangeWeapon(name); }
}
