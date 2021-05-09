using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dummy : CharacterBase
{
    private bool isRevivaling = false;

    private void Update()
    {
        hpBar.transform.LookAt(MainCamera.instance.transform);
    }

    public bool NeedRevivaling() { return (curLife <= 0) && isRevivaling == false; }
    public void StartRevival() { isRevivaling = true; }
    public void Revival()
    {
        HpReset();
        isRevivaling = false;
    }
}
