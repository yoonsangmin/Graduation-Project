using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using BBUnity.Actions;

[Action("MyBasic/SelectPattern")]

public class BB_SelectPattern : BasePrimitiveAction
{
    [InParam("Boss Enemy")]
    public BossEnemy boss;

    [InParam("target")]
    public GameObject target;

    public override void OnStart()
    {
        boss.PatternSelected();
        if (boss.curPattern == BossAttackPattern.REST)
        {
            boss.curPattern = BossAttackPattern.DASH;
            //if(Vector3.Distance(boss.transform.position, target.transform.position) <= 7.0f)
            //    output = BossAttackPattern.SPIT;
            //else
            //    output = (BossAttackPattern)Random.Range(2, System.Enum.GetValues(typeof(BossAttackPattern)).Length);        
        }
        else
        {
            boss.curPattern = BossAttackPattern.REST;
        }
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.COMPLETED;
    }
}
