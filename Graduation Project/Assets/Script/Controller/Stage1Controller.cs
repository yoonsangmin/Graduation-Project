using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage1Controller : MonoBehaviour
{
    private static Stage1Controller stage1Controller;
    public static Stage1Controller instance
    {
        get
        {
            if (stage1Controller == null)
                stage1Controller = FindObjectOfType<Stage1Controller>();
            return stage1Controller;
        }
    }

    enum StageStep { Start, Step1, Step2, Step3, Step4, Step5, End }

    [SerializeField] private EnemyController enemyController = null;
    private Dictionary<int, List<Enemy>> enemys = new Dictionary<int, List<Enemy>>();

    [SerializeField] private GameObject show = null;
    [SerializeField] private Text progressText = null;
    [SerializeField] private Text remainEnemyText = null;
    [SerializeField] private Text goalText = null;

    private StageStep curProgressStep = StageStep.Start;
    private StageStep maxProgressStep = StageStep.Step5;

    private int enemySummonIndex = -1;
    public int _enemySummonIndex { get { return enemySummonIndex; } }
    private int dieEnemys = 0;
    private int allEnemys = 0;

    [SerializeField] private MiniMap miniMap = null;

    [SerializeField] private Generator generator = null;

    [SerializeField] private GameObject nextShow = null;
    [SerializeField] private Text nextShowGoalText = null;
    [SerializeField] private Text nextShowProgressText = null;
    [SerializeField] private Text nextShowProgressTitleText = null;
    [SerializeField] private Image nextShowBackground = null;
    
    private string enemyKillText = "맵에 있는 적을 모두 처치하시오!";
    private string generatorText = "발전기를 가동시켜 문을 여시오!";
    private string endStepText = "맵에 있는 적을 모두 처치 후 열린 문으로 이동하시오!";

    void Start()
    {
        enemys = enemyController.GetEnemys();
        goalText.text = enemyKillText;
        GoNextStep();
    }

    private void CheckEnemyDie()
    {
        int dieEnemysValue = 0;
        for (int i = 0; i < enemys[enemySummonIndex].Count; i++)
        {
            if (enemys[enemySummonIndex][i].GetComponent<Enemy>()._isDead == true)
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
                GameController.instance.Stage1Clear = true;
                SceneManager.LoadScene("Begin Stage 2");
                break;
        }
    }

    private IEnumerator NextShowUi()
    {
        show.SetActive(false);
        nextShow.SetActive(true);
        nextShowProgressText.text = (int)curProgressStep + "/" + (int)maxProgressStep;
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

    private void SummonEnemy()
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
    
    public bool CanGoToNextStep() { return dieEnemys == allEnemys; }
}
