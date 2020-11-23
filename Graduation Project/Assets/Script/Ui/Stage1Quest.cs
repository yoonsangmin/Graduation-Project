using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1Quest : MonoBehaviour
{
    [SerializeField]
    EnemyController enemyController = null;
    Dictionary<int, List<Enemy>> enemys = new Dictionary<int, List<Enemy>>();

    [SerializeField]
    Text progressText = null;

    int curProgressStep = 0;
    int maxProgressStep = 5;

    [SerializeField]
    Text remainEnemyText = null;

    int dieEnemys = 0;
    int allEnemys = 0;

    [SerializeField]
    MiniMap miniMap = null;

    void Start()
    {
        enemys = enemyController.GetEnemys();
        GoNextStep();
    }

    void CheckEnemyDie()
    {
        int dieEnemysValue = 0;
        for (int i = 0; i < enemys[curProgressStep - 1].Count; i++)
        {
            if (enemys[curProgressStep - 1][i].GetComponent<Enemy>().IsDead() == true)
                dieEnemysValue++;
        }

        dieEnemys = dieEnemysValue;
    }

    public void UpdateQuestView()
    {
        progressText.text = curProgressStep + "/" + maxProgressStep;

        CheckEnemyDie();
        remainEnemyText.text = dieEnemys + "/" + allEnemys;
    }

    public void GoNextStep()
    {
        curProgressStep++;
        for (int i = 0; i < enemys[curProgressStep - 1].Count; i++)
            enemys[curProgressStep - 1][i].gameObject.SetActive(true);

        allEnemys = enemys[curProgressStep - 1].Count;
        miniMap.SetEnemysCount(allEnemys);
    }

    public int GetCurProgressStep() { return curProgressStep; }
    public bool CanGoToNextStep() { return dieEnemys == allEnemys; }
}
