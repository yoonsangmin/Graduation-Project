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
    GameObject show = null;
    [SerializeField]
    Text progressText = null;
    [SerializeField]
    Text remainEnemyText = null;
    [SerializeField]
    Text goalText = null;

    StageStep curProgressStep = StageStep.Start;
    StageStep maxProgressStep = StageStep.Step5;

    int enemySummonIndex = -1;
    int dieEnemys = 0;
    int allEnemys = 0;

    [SerializeField]
    MiniMap miniMap = null;

    [SerializeField]
    Generator generator = null;

    [SerializeField]
    GameObject nextShow = null;
    [SerializeField]
    Text nextShowGoalText = null;
    [SerializeField]
    Text nextShowProgressText = null;
    [SerializeField]
    Text nextShowProgressTitleText = null;
    [SerializeField]
    Image nextShowBackground = null;

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
                StartCoroutine(NextShowUi());
                break;
            case StageStep.Step2:
                SummonEnemy();
                StartCoroutine(NextShowUi());
                break;
            case StageStep.Step3:
                SummonEnemy();
                StartCoroutine(NextShowUi());
                break;
            case StageStep.Step4:
                generator.GeneratorStart();
                goalText.text = generatorText;
                remainEnemyText.enabled = false;
                StartCoroutine(NextShowUi());
                break;
            case StageStep.Step5:
                SummonEnemy();
                goalText.text = endStepText;
                remainEnemyText.enabled = true;
                StartCoroutine(NextShowUi());
                break;
            case StageStep.End:
                SceneManager.LoadScene("Begin Stage 2");
                break;
        }
    }

    IEnumerator NextShowUi()
    {
        show.SetActive(false);
        nextShow.SetActive(true);
        nextShowProgressText.text = progressText.text;
        nextShowGoalText.text = goalText.text;
        yield return new WaitForSeconds(1.0f);
        
        Color[] alpha = new Color[4];
        alpha[0] = nextShowGoalText.color;
        alpha[1] = nextShowProgressText.color;
        alpha[2] = nextShowProgressTitleText.color;
        alpha[3] = nextShowBackground.color;

        float alphaValue = 1.0f;
        while (alphaValue > 0)
        {
            for (int i = 0; i < alpha.Length; i++)
                alpha[i].a = alphaValue;

            nextShowGoalText.color = alpha[0];
            nextShowProgressText.color = alpha[1];
            nextShowProgressTitleText.color = alpha[2];
            nextShowBackground.color = alpha[3];

            alphaValue -= Time.deltaTime;
            yield return null;
        }

        nextShow.SetActive(false);
        for (int i = 0; i < alpha.Length; i++)
            alpha[i].a = 1.0f;
        nextShowGoalText.color = alpha[0];
        nextShowProgressText.color = alpha[1];
        nextShowProgressTitleText.color = alpha[2];
        nextShowBackground.color = alpha[3];

        show.SetActive(true);
    }

    void SummonEnemy()
    {
        enemySummonIndex++;
        for (int i = 0; i < enemys[enemySummonIndex].Count; i++)
        {
            enemys[enemySummonIndex][i].gameObject.SetActive(true);
            enemys[enemySummonIndex][i].SummonParticlePlay();
        }

        allEnemys = enemys[enemySummonIndex].Count;
        miniMap.SetEnemysCount(allEnemys);
    }

    public int GetCurEnemySummonInex() { return enemySummonIndex; }
    public bool CanGoToNextStep() { return dieEnemys == allEnemys; }
}
