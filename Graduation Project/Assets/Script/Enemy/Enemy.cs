using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum Pattern
{
    FixPos,
    Detect
}

enum State
{
    Detect,
    Avoid,
    Attack,
    Die
}

public class Enemy : CharacterBase
{
    NavMeshAgent enemyAi = null;

    [SerializeField]
    Pattern whatEnemyPattern = 0;

    State curState = State.Detect;

    //정찰위치
    [SerializeField]
    Transform[] patrolPoints = null;
    int patrolCount = 0;

    //Fix enemy 정찰
    float rotMax = 60;
    float curRot = 0;
    float rotVal = 0.5f;

    Transform target = null;

    //피하기
    Vector3 avoidDir;

    //시야각
    float viewAngle = 130.0f;
    float viewDistance = 30.0f;

    [SerializeField]
    LayerMask targetMask = 0;

    [SerializeField]
    EnemyWeaponController weapon = null;

    float RangeAttackDistance = 30.0f;
    float MeleeAttackDistance = 2.0f;
    float MoveToTargetDistance = 50.0f;
    float targetToDistance = 0.0f;

    void Start()
    {
        SetCharacterStat(100, 2.0f);
        enemyAi = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        enemyAi.speed = walkSpeed;
        enemyAi.isStopped = false;
    }

    void Update()
    {
        HpBarLookAtCamera();

        FsmMain();
    }

    //Fsm State
    void FsmMain()
    {
        if (isDead == true) ChangeState(State.Die);
        if (target != null)
            targetToDistance = Vector3.Distance(target.position, transform.position);

        switch (whatEnemyPattern)
        {
            case Pattern.Detect:
                switch (curState)
                {
                    case State.Detect:
                        if (IsInvoking("MoveToDetectionPoint") == false) Invoke("MoveToDetectionPoint", 1.0f);
                        WalkAnimatorSetting();
                        break;
                    case State.Attack:
                        DetectAttackToTarget();
                        break;
                    case State.Avoid:
                        transform.LookAt(target);
                        if (IsInvoking("SetAvoidDir") == false) Invoke("SetAvoidDir", 4.0f);
                        else Avoid();
                        break;
                    case State.Die:
                        break;
                }
                break;

            case Pattern.FixPos:
                switch (curState)
                {
                    case State.Detect:
                        FixDetectToRotate();
                        break;
                    case State.Attack:
                        FixAttackToTarget();
                        break;
                    case State.Avoid:
                        transform.LookAt(target);
                        if (IsInvoking("SetAvoidDir") == false) Invoke("SetAvoidDir", 4.0f);
                        else Avoid();
                        break;
                    case State.Die:
                        break;
                }
                break;
        }
    }

    void ChangeState(State change)
    {
        //FSM State Out
        switch (curState)
        {
            case State.Detect:
                break;
            case State.Attack:
                ani.SetBool("IsFire", false);
                break;
            case State.Avoid:
                CancelInvoke("SetAvoidDir");
                break;
            case State.Die:
                break;
        }

        //FSM State In
        switch (change)
        {
            case State.Detect:
                RemoveTarget();
                break;
            case State.Attack:
                break;
            case State.Avoid:
                SetAvoidDir();
                break;
            case State.Die:
                ani.SetTrigger("Dead");
                col.enabled = false;
                enemyAi.isStopped = true;
                hpBar.gameObject.SetActive(false);
                Invoke("DieToVanish", 5.0f);
                break;
        }

        curState = change;
    }

    void WalkAnimatorSetting()
    {
        if (enemyAi.velocity != Vector3.zero)
            ani.SetBool("Walking", true);
        else
            ani.SetBool("Walking", false);
    }

    void MoveToDetectionPoint()
    {
        View();

        if (enemyAi.velocity == Vector3.zero && enemyAi.pathPending == false)
        {
            SetDestination(patrolPoints[patrolCount++].position);

            if (patrolCount >= patrolPoints.Length)
                patrolCount = 0;
        }
    }

    void DetectAttackToTarget()
    {
        //플레이어와 근접하게 이동
        if (targetToDistance <= MoveToTargetDistance && targetToDistance > RangeAttackDistance - 5.0f)
        {
            SetDestination(target.position);
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.up, (target.position - transform.position).normalized, out hit, RangeAttackDistance))
            {
                if (hit.transform.tag == "Player")
                {
                    enemyAi.isStopped = true;

                    //장거리 공격
                    if (targetToDistance <= RangeAttackDistance && targetToDistance > MeleeAttackDistance)
                        Attack();
                    //근접 공격
                    else if (targetToDistance <= MeleeAttackDistance)
                        Attack();
                }
                else
                {
                    SetDestination(target.position);
                }
            }
        }
    }

    void FixDetectToRotate()
    {
        View();

        if (curRot >= rotMax || curRot <= -rotMax) rotVal = -rotVal;
        curRot += rotVal;

        enemyAi.transform.rotation = enemyAi.transform.rotation * Quaternion.Euler(0.0f, rotVal, 0.0f);
    }

    void FixAttackToTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.up, (target.position - transform.position).normalized, out hit, RangeAttackDistance * 2))
        {
            if (hit.transform.tag == "Player")
            {
                enemyAi.isStopped = true;

                //장거리 공격
                if (targetToDistance <= RangeAttackDistance * 2)
                    Attack();
            }
            else
            {
                SetDestination(target.position);
            }
        }
    }

    void Attack()
    {
        transform.LookAt(target);
        weapon.LookAtTarget(target);
        weapon.Fire();
        ani.SetBool("IsFire", true);

        if (weapon.CanFire() == false) ChangeState(State.Avoid);
        if (targetToDistance > MoveToTargetDistance) ChangeState(State.Detect);
    }

    void SetAvoidDir()
    {
        int moveDir = Random.Range(1, 3);
        avoidDir = Vector3.zero;
        switch (moveDir)
        {
            case 1:
                avoidDir = enemyAi.transform.right;
                break;
            case 2:
                avoidDir = -enemyAi.transform.right;
                break;
        }
    }

    void Avoid()
    {
        if (weapon.CanFire() == true) ChangeState(State.Attack);
        if (targetToDistance > MoveToTargetDistance) ChangeState(State.Detect);

       SetDestination(enemyAi.transform.position + avoidDir);
        WalkAnimatorSetting();
    }

    //시야각 관련 함수
    void View()
    {
        Vector3 leftBoundary = BoundaryAngle(-viewAngle * 0.5f);
        Vector3 rightBoundary = BoundaryAngle(viewAngle * 0.5f);

        Collider[] target = Physics.OverlapSphere(transform.position, viewDistance * 2, targetMask);

        if (target.Length < 1) return;

        Transform findTarget = target[0].transform;

        Vector3 direction = (findTarget.position - transform.position).normalized;
        float angle = Vector3.Angle(direction, transform.forward);

        if (angle < viewAngle * 0.5f || haveDamaged == true)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position + transform.up, direction, out hit, viewDistance, targetMask))
                if (hit.transform.tag == "Player")
                {
                    SetTarget(hit.transform);
                    ChangeState(State.Attack);
                }
        }
    }

    Vector3 BoundaryAngle(float angle)
    {
        angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0.0f, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    //타겟 설정 변경
    void SetTarget(Transform target) { this.target = target; }
    void RemoveTarget() { this.target = null; }

    //이동 설정
    void SetDestination(Vector3 target)
    {
        enemyAi.isStopped = false;
        enemyAi.SetDestination(target);
    }

    void DieToVanish() { gameObject.SetActive(false); }
}
