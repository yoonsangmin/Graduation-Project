using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> barrels = new List<GameObject>();

    float recreateTime = 5.0f;

    void Update()
    {
        foreach (GameObject barrel in barrels)
            if (barrel.GetComponent<Barrel>().IsExplosion() == true)
                StartCoroutine(RecreateCoroutine(barrel));
    }

    IEnumerator RecreateCoroutine(GameObject barrel)
    {
        yield return new WaitForSeconds(recreateTime);

        barrel.GetComponent<Barrel>().ReCreate();
    }
}
