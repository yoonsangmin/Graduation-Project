using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField]
    ObjectText objectText = null;

    [SerializeField]
    GameObject door = null;
    [SerializeField]
    Camera cam = null;
    [SerializeField]
    Stage1Quest goNext = null;

    bool generatorStart = false;
    bool operateDone = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player" && operateDone == false && generatorStart == true) return;
        objectText.ColObject();
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Player" && operateDone == false && generatorStart == true) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            cam.gameObject.SetActive(true);
            StartCoroutine(OpenDoorCouroutine());
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag != "Player" && operateDone == false && generatorStart == true) return;
        objectText.UnColObject();
    }

    IEnumerator OpenDoorCouroutine()
    {
        yield return new WaitForSeconds(1.0f);

        Vector3 doorPos = door.transform.localPosition;

        while (door.transform.localPosition.y < 5.0f)
        {
            doorPos.y += Time.deltaTime;
            door.transform.localPosition = doorPos;
            yield return null;
        }

        objectText.gameObject.SetActive(false);
        cam.gameObject.SetActive(false);
        operateDone = true;
        goNext.GoNextStep();
    }

    public bool OperateDone() { return operateDone; }
    public void GeneratorStart()
    {
        generatorStart = true;
        objectText.UnColObject();
    }
}
