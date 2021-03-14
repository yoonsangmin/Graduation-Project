using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    [SerializeField] private List<GameObject> barrels = new List<GameObject>();

    private float recreateTime = 5.0f;

    void Update()
    {
        foreach (GameObject barrel in barrels)
            if (barrel.GetComponent<Barrel>()._isExplosion == true)
                StartCoroutine(RecreateCoroutine(barrel));
    }

    private IEnumerator RecreateCoroutine(GameObject barrel)
    {
        yield return new WaitForSeconds(recreateTime);

        barrel.GetComponent<Barrel>().ReCreate();
    }
}
