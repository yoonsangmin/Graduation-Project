using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    GameObject enemysParent = null;
    Dictionary<int, List<Enemy>> enemys = new Dictionary<int, List<Enemy>>();

    void Awake()
    {
        SetEnemys();
    }

    void SetEnemys()
    {
        for (int i = 0; i < enemysParent.transform.childCount; i++)
        {
            if (enemysParent.transform.GetChild(i).gameObject.name != (i + 1).ToString()) continue;

            List<Enemy> addEnemys = new List<Enemy>();

            for (int j = 0; j < enemysParent.transform.GetChild(i).childCount; j++)
            {
                GameObject enemyObj = enemysParent.transform.GetChild(i).GetChild(j).gameObject;
                if (enemyObj.tag == "Enemy")
                {
                    addEnemys.Add(enemyObj.GetComponent<Enemy>());
                    enemyObj.SetActive(false);
                }
            }
            enemys.Add(i, addEnemys);
        }
    }

    public Dictionary<int,List<Enemy>> GetEnemys() { return enemys; }
}
