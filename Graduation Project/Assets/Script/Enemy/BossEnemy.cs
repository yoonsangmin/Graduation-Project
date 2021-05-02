using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossAttackPattern { NONE, REST, SPIT, DASH, JUMP };

public class BossEnemy : Enemy
{
    public BossAttackPattern curPattern  = BossAttackPattern.NONE;

    private float dashDamage = 50.0f;
    public float _dashDamage { get { return dashDamage; } }

    private float jumpAttackDamage = 70.0f;
    public float _jumpAttackDamage { get { return jumpAttackDamage; } }

    private bool isPatternEnd = true;
    public bool _isPatternEnd { get { return isPatternEnd; } }

    private bool canPatternSelect = true;
    public bool _canPatternSelect { get { return canPatternSelect; } }

    [SerializeField] private GameObject dashAttackCol = null;
    [SerializeField] private GameObject jumpAttackCol = null;
    [SerializeField] private GameObject acidAttackCol = null;

    public void PatternSelected() { canPatternSelect = false; }
    public void PatternStart() { isPatternEnd = false; }
    public void PatternEnd()
    {
        canPatternSelect = true;
        isPatternEnd = true;
    }

    //대쉬 어택
    public void DashAttackStart()
    {
        ani.SetBool("IsDashAttackOn", true);
        enemyAi.speed = stat._walkSpeed * 10.0f;
        dashAttackCol.SetActive(true);
    }

    public void DashAttackEnd()
    {
        ani.SetBool("IsDashAttackOn", false);
        enemyAi.speed = stat._walkSpeed;
        PatternEnd();
        dashAttackCol.SetActive(false);
    }

    //점프 어택
    public void JumpAttackStart()
    {
        ani.SetBool("IsJumpAttackOn", true);
        jumpAttackCol.SetActive(true);
    }

    public void JumpAttackEnd()
    {
        ani.SetBool("IsJumpAttackOn", false);
        PatternEnd();
        jumpAttackCol.SetActive(false);
    }

    public Vector3 GetJumpVelocity(Vector3 curPos, Vector3 targetPos, float initialAngle)
    {
        //중력 벡터의 길이
        float gravity = Physics.gravity.magnitude;
        float angle = initialAngle * Mathf.Deg2Rad;

        //평면 기준 Vector3
        Vector3 planarTarget = new Vector3(targetPos.x, 0.0f, targetPos.z);
        Vector3 planarCurPosition = new Vector3(curPos.x, 0.0f, curPos.z);

        float dist = Vector3.Distance(planarTarget, planarCurPosition);
        float yOffset = curPos.y - targetPos.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(dist, 2)) / (dist * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarCurPosition) * (targetPos.x > curPos.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        return finalVelocity;
    }

    public Vector3 aa(Vector3 curPos, Vector3 targetPos, float initialAngle)
    {
        //중력 벡터의 길이
        float gravity = Physics.gravity.magnitude;
        float angle = initialAngle * Mathf.Deg2Rad;

        //평면 기준 Vector3
        Vector3 planarTarget = new Vector3(targetPos.x, 0.0f, targetPos.z);
        Vector3 planarCurPosition = new Vector3(curPos.x, 0.0f, curPos.z);

        float dist = Vector3.Distance(planarTarget, planarCurPosition);

        float initialVelocity = Mathf.Sqrt(dist * gravity);

        return new Vector3(initialVelocity, initialVelocity, 0.0f);
    }

    //산성 침
    public void AcidAttackStart()
    {
        acidAttackCol.SetActive(true);
    }

    public void AcidAttackEnd()
    {
        PatternEnd();
        acidAttackCol.SetActive(false);
    }

    override protected void Dead()
    {
    }

    override protected void HpBarSetting()
    {
    }
}
