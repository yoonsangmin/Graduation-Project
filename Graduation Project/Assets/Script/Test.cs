using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    GameObject dd;

    void Update()
    {
        transform.LookAt(dd.transform);
    }
}
