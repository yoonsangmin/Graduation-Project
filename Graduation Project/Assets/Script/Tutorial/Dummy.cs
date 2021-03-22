using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : CharacterBase
{
    private bool isRevivaling = false;

    void Update()
    {
        HpBarLookAtCamera();
    }

    public bool NeedRevivaling() { return (curLife <= 0) && isRevivaling == false; }
    public void StartRevival() { isRevivaling = true; }
    public void Revival()
    {
        HpReset();
        isRevivaling = false;
    }
}
