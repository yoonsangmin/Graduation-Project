using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : CharacterBase
{
    bool isRevivaling = false;

    void Start()
    {
        SetCharacterStat(100.0f, 0.0f);
    }

    void Update()
    {
        HpBarLookAtCamera();
    }

    public bool IsDead() { return (curLife <= 0) && isRevivaling == false; }
    public void Dead() { isRevivaling = true; }
    public void Revival()
    {
        HpReset();
        isRevivaling = false;
    }
}
