using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using BBUnity.Actions;

[Action("MyBasic/SelectPattern")]

public class BB_SelectPattern : BasePrimitiveAction
{
    [OutParam("output")]
    public BossAttackPattern output;

    public override void OnStart()
    {
        output = (BossAttackPattern)Random.Range(0, System.Enum.GetValues(typeof(BossAttackPattern)).Length);
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.COMPLETED;
    }
}
