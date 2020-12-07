using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CriticalText : MonoBehaviour
{
    Text thisText;

    float endYPos = -21.0f;

    void Awake()
    {
        thisText = GetComponent<Text>();
    }

    public void HitCritical(Vector3 startPos) { StartCoroutine(ShowCouroutine(startPos)); }

    IEnumerator ShowCouroutine(Vector3 startPos)
    {        
        transform.localPosition = startPos;

        Vector3 textPos = transform.localPosition;
        Color alphaValue = thisText.color;

        while (transform.localPosition.y < endYPos)
        {
            textPos.y += 0.3f;
            transform.localPosition = textPos;

            alphaValue.a = (endYPos - transform.localPosition.y) / (endYPos - startPos.y);
            thisText.color = alphaValue;

            yield return null;
        }

        gameObject.SetActive(false);
    }
}
