using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUi : MonoBehaviour
{
    void Update()
    {
        //버튼눌리는 효과
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Begin Stage 1");
    }

    public void TraniningButton()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void OptionButton()
    {
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
