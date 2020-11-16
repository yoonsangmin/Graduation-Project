using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> dummys = new List<GameObject>();

    float revivalTime = 5.0f;

    void Update()
    {
        foreach (GameObject dummy in dummys)
            if (dummy.GetComponent<Dummy>().IsDead())
                StartCoroutine(RevivalCoroutine(dummy));
    }

    IEnumerator RevivalCoroutine(GameObject dummy)
    {
        dummy.SetActive(false);
        dummy.GetComponent<Dummy>().Dead();
        yield return new WaitForSeconds(revivalTime);

        dummy.SetActive(true);
        dummy.GetComponent<Dummy>().Revival();
    }
}
