using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectText : MonoBehaviour
{
    Camera mainCamera;

    [SerializeField]
    float yPosDistance = 0.0f;

    [SerializeField]
    GameObject colUi = null;
    [SerializeField]
    GameObject unColUi = null;
    [SerializeField]
    GameObject rootObject = null;

    Vector3 originPos = new Vector3(205.0f, 0.0f, 0.0f);

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        colUi.SetActive(false);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (colUi.activeSelf == true || rootObject.GetComponent<CheckObjectVisible>().isVisible == false ) return;

        Vector3 screenPos = mainCamera.WorldToScreenPoint(rootObject.transform.position + new Vector3(0.0f, yPosDistance, 0.0f));
        transform.position = new Vector3(screenPos.x, screenPos.y, 0.0f);
    }

    public void ColObject() { transform.localPosition = originPos; colUi.SetActive(true); unColUi.SetActive(false); }
    public void UnColObject() { colUi.SetActive(false); unColUi.SetActive(true); }
}
