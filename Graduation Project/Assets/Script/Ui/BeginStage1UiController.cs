using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeginStage1UiController : MonoBehaviour
{
    [SerializeField]
    Text explainText = null;

    List<string> text = new List<string>();
    int textIndex = 0;

    void Start()
    {
        text.Add("세계의 평화를 위해 활동하는 PW 집단..");
        text.Add("PW는 세계적으로 유명한 코드네임 GW의 실험이 위험하다고 판단하였습니다.");
        text.Add("그리하여 PW는 당신을 용병으로 고용하였습니다.");
        text.Add("당신의 임무는 GW의 실험실로 침투하여 GW의 실험이 무엇인지 알아오는 것입니다.");
        text.Add("그럼 행운을 빕니다...");
        explainText.text = text[textIndex];
    }

    public void NextButton()
    {
        textIndex++;
        if (textIndex == text.Count)
            SceneManager.LoadScene("Stage 1");
        else explainText.text = text[textIndex];
    }
}
