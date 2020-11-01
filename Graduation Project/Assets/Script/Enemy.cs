using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : CharacterBase
{
    NavMeshAgent enemyAi;

    //정찰위치
    [SerializeField]
    Transform[] patrolPoints;
    int patrolCount = 0;

    Transform target;

    //시야각
    float viewAngle = 130.0f;
    float viewDistance = 20.0f;

    [SerializeField]
    LayerMask targetMask;

    [SerializeField]
    EnemyWeaponController weapon;

    float RangeAttackDistance = 30.0f;
    float MeleeAttackDistance = 2.0f;
    float MoveToTargetDistance = 50.0f;

    void Start()
    {
        SetCharacterStat(100, 3.0f);
        enemyAi = GetComponent<NavMeshAgent>();
        enemyAi.speed = walkSpeed;
    }

    void Update()
    {
        View();

        if (target != null)
            AttackToTarget();
        else
            MoveToDetectionPoint();
    }

    void MoveToDetectionPoint()
    {
        Debug.Log("dd");
        if (enemyAi.velocity == Vector3.zero && enemyAi.pathPending == false)
        {
            enemyAi.stoppingDistance = 0.0f;

            enemyAi.SetDestination(patrolPoints[patrolCount++].position);

            if (patrolCount >= patrolPoints.Length)
                patrolCount = 0;
        }
    }

    void AttackToTarget()
    {
        enemyAi.stoppingDistance = RangeAttackDistance - 5.0f;

        float targetToDistance = Vector3.Distance(target.position, transform.position);

        //플레이어가 벗어나면 다시 순찰 위치로
        if (targetToDistance > MoveToTargetDistance) RemoveTarget();
        else
        {
            //플레이어와 근접하게 이동
            if (targetToDistance <= MoveToTargetDistance && targetToDistance > enemyAi.stoppingDistance)
            {
                Debug.Log("이동");
                enemyAi.SetDestination(target.position);
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + transform.up, (target.position - transform.position).normalized, out hit, RangeAttackDistance, targetMask))
                {
                    if (hit.transform.tag == "Player")
                    {
                        //장거리 공격
                        if (targetToDistance <= RangeAttackDistance && targetToDistance > MeleeAttackDistance)
                        {
                            Debug.Log("장거리");
                            transform.LookAt(target);
                            Attack();
                        }
                        //근접 공격
                        else if (targetToDistance <= MeleeAttackDistance)
                        {
                            Debug.Log("근접");
                            transform.LookAt(target);
                            Attack();
                        }
                    }
                }
                else
                    enemyAi.SetDestination(target.position);
            }
        }
    }

    void Attack()
    {
        weapon.LookAtTarget(target);
        weapon.Fire(false);
    }

    //시야각 관련 함수
    void View()
    {
        Vector3 leftBoundary = BoundaryAngle(-viewAngle * 0.5f);
        Vector3 rightBoundary = BoundaryAngle(viewAngle * 0.5f);

        Collider[] target = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        for (int i = 0; i < target.Length; i++)
        {
            Transform findTarget = target[i].transform;

            if (findTarget.tag == "Player")
            {
                Vector3 direction = (findTarget.position - transform.position).normalized;
                float angle = Vector3.Angle(direction, transform.forward);

                if (angle < viewAngle * 0.5f || haveDamaged == true)
                {
                    RaycastHit hit;

                    if (Physics.Raycast(transform.position + transform.up, direction, out hit, viewDistance, targetMask))
                        if (hit.transform.tag == "Player")
                            SetTarget(hit.transform);
                }
            }
        }
    }

    Vector3 BoundaryAngle(float angle)
    {
        angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0.0f, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    void SetTarget(Transform target)
    {
        this.target = target;
    }

    void RemoveTarget()
    {
        this.target = null;
    }
}
