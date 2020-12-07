using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeginStage2UiController : MonoBehaviour
{
    [SerializeField]
    Text explainText = null;

    List<string> text = new List<string>();
    int textIndex = 0;

    void Start()
    {
        text.Add("축하드립니다. 당신은 성공적으로 GW의 실험실로 침투하였습니다.");
        text.Add("GW의 실험실에서는 인체 실험이 진행되고 있다는 소문이 전해져왔습니다...");
        text.Add("확실하게 실험을 파악 후 실험문서를 가져오시기 바랍니다.");        
        text.Add("그럼 행운을 빕니다...");
        explainText.text = text[textIndex];
    }

    public void NextButton()
    {
        textIndex++;
        if (textIndex == text.Count)
            SceneManager.LoadScene("Stage 2");
        else explainText.text = text[textIndex];
    }
}
