using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectText : MonoBehaviour
{
    GameObject mainCamera;

    GameObject colUi = null;
    [SerializeField]
    GameObject unColUi = null;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").gameObject;
        colUi = GameObject.Find("Ui").transform.Find("Object Text").transform.Find("Col").gameObject;
        colUi.SetActive(false);
        unColUi.SetActive(false);
    }

    void Update()
    {
        transform.LookAt(mainCamera.transform.position);        
    }

    public void ColObject() { colUi.SetActive(true); unColUi.SetActive(false); }
    public void UnColObject() { colUi.SetActive(false); unColUi.SetActive(true); }
}
