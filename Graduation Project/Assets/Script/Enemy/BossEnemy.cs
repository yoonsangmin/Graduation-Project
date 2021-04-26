using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossAttackPattern { REST, SPIT, DASH, JUMP };

public class BossEnemy : Enemy
{
    private bool isPatternEnd = true;
    public bool _isPatternEnd { get { return isPatternEnd; } }

    override protected void Dead()
    {       
    }

    override protected void HpBarSetting()
    {
    }
}
