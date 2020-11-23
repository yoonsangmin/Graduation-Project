using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStepArea : MonoBehaviour
{
    [SerializeField]
    Stage1Quest quest = null;

    private void OnTriggerEnter(Collider other)
    {
        if(quest.CanGoToNextStep() == true && other.tag == "Player")
        {
            if (gameObject.name == quest.GetCurProgressStep().ToString())
                quest.GoNextStep();
        }
    }
}
