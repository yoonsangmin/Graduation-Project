using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using BBUnity.Actions;

[Action("MyBasic/ClearValue")]

public class BB_ClearValue : BasePrimitiveAction
{
    [OutParam("outputValue")]
    [Help("output variable")]
    public GameObject output;

    public override void OnStart()
    {
        output = null;
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.COMPLETED;
    }
}
