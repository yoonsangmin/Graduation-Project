using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage1Quest : MonoBehaviour
{
    enum StageStep { Start, Step1, Step2, Step3, Step4, Step5, End }

    [SerializeField]
    EnemyController enemyController = null;
    Dictionary<int, List<Enemy>> enemys = new Dictionary<int, List<Enemy>>();

    [SerializeField]
    Text progressText = null;

    StageStep curProgressStep = StageStep.Start;
    StageStep maxProgressStep = StageStep.Step5;

    int enemySummonIndex = -1;

    [SerializeField]
    Text remainEnemyText = null;

    int dieEnemys = 0;
    int allEnemys = 0;

    [SerializeField]
    MiniMap miniMap = null;

    [SerializeField]
    Generator generator = null;

    [SerializeField]
    Text goalText = null;

    string enemyKillText = "맵에 있는 적을 모두 처치하시오!";
    string generatorText = "발전기를 가동시켜 문을 여시오!";
    string endStepText = "맵에 있는 적을 모두 처치 후 열린 문으로 이동하시오!";

    void Start()
    {
        enemys = enemyController.GetEnemys();
        goalText.text = enemyKillText;
        GoNextStep();
    }

    void CheckEnemyDie()
    {
        int dieEnemysValue = 0;
        for (int i = 0; i < enemys[enemySummonIndex].Count; i++)
        {
            if (enemys[enemySummonIndex][i].GetComponent<Enemy>().IsDead() == true)
                dieEnemysValue++;
        }

        dieEnemys = dieEnemysValue;
        if (curProgressStep == StageStep.Step3 && dieEnemys == allEnemys)
            GoNextStep();
    }

    public void UpdateQuestView()
    {
        progressText.text = (int)curProgressStep + "/" + (int)maxProgressStep;

        CheckEnemyDie();
        remainEnemyText.text = dieEnemys + "/" + allEnemys;
    }

    public void GoNextStep()
    {
        curProgressStep++;

        switch (curProgressStep)
        {
            case StageStep.Step1:
                SummonEnemy();
                break;
            case StageStep.Step2:
                SummonEnemy();
                break;
            case StageStep.Step3:
                SummonEnemy();
                break;
            case StageStep.Step4:
                generator.GeneratorStart();
                goalText.text = generatorText;
                remainEnemyText.enabled = false;
                break;
            case StageStep.Step5:
                SummonEnemy();
                goalText.text = endStepText;
                remainEnemyText.enabled = true;
                break;
            case StageStep.End:
                SceneManager.LoadScene("Begin Stage 2");
                break;
        }
    }

    void SummonEnemy()
    {
        enemySummonIndex++;
        for (int i = 0; i < enemys[enemySummonIndex].Count; i++)
            enemys[enemySummonIndex][i].gameObject.SetActive(true);

        allEnemys = enemys[enemySummonIndex].Count;
        miniMap.SetEnemysCount(allEnemys);        
    }

    public int GetCurEnemySummonInex() { return enemySummonIndex; }
    public bool CanGoToNextStep() { return dieEnemys == allEnemys; }
}
